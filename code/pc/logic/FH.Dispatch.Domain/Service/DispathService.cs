using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Domain.Model;
using ViData;
using FH.Common.Utils.Dijkstra;

namespace FH.Dispatch.Domain.Service
{
    public class DispathService : Repository<DispathInfo, Dispath>
    {
        public DispathService(int houseId)
        {
            this.HouseId = houseId;
        }
        /// <summary>
        /// 获取调度指令
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<DispathInfo> GetTopItems(int top)
        {
            string sql = "select top "+ top +" * frm t_d_dispatch where status = 0  order by id";
            return DataHelper.Fill<DispathInfo>(sql);
        }

        public int HouseId { get; set; }
        RackService x_rkService = new RackService();
        RouteService x_rtService = new RouteService();
        TruckService x_tkService = new TruckService();
        private IList<RackInfo> rackList;
        private IList<RouteInfo> routelist;
        private IList<CenterRouteInfo> centerlist;
        private IList<List<TurnWayInfo>> waylist;
        private Dictionary<string, List<DNode>> nodeDict;
        /// <summary>
        /// 当前车辆动态
        /// </summary>
        private Dictionary<string, string> dynamicRoute;
        void initData()
        {
            waylist = new List<List<TurnWayInfo>>();
            rackList = x_rkService.Get(a => a.Isvalid == 1 && a.HouseId == HouseId);
            routelist = x_rtService.Get(a => a.IsValid == 1 && a.HouseId == HouseId);
            centerlist = new List<CenterRouteInfo>();
            nodeDict = SetToNodes();
            foreach (var item in routelist)
            {
                int i = 0;
                List<string> last = new List<string>();

                foreach (var subitem in routelist)
                {
                    if (subitem.Nextroutecode == item.Routecode)
                    {
                        last.Add(subitem.Routecode);
                        i++;
                    }
                }
                if (i >= 2)
                {
                    string next2 = "";
                    if (!string.IsNullOrEmpty(item.Leftroutecode))
                    {
                        next2 = item.Leftroutecode;
                    }
                    else
                    {
                        next2 = item.Rightroutecode;
                    }
                    CenterRouteInfo rinfo = new CenterRouteInfo()
                    {
                        Center = item.Routecode,
                         Last1 = last[0],
                          Last2 = last[1],
                           Next1 = item.Nextroutecode,
                            Next2 = next2
                    };
                    centerlist.Add(rinfo);
                }
            }
        }
        /// <summary>
        /// 构造指令地图
        /// </summary>
        /// <param name="iotype">出库入库</param>
        /// <param name="rackcode1">取货仓位编码</param>
        /// <param name="rackcode2">卸货仓位编码</param>
        /// <param name="tuopancode">托盘编码</param>
        /// <returns></returns>
        public InstructionInfo SetToMap( InOutType iotype, string rackcode1, string rackcode2, string tuopancode)
        {
            initData();
            InstructionInfo insInfo = new InstructionInfo();

            RackLayerService x_rlService = new RackLayerService();
            var rlayer = x_rlService.Get(a => a.RackLayerCode == rackcode1).FirstOrDefault();
            if (rlayer != null)
            {
                var rkitem = rackList.Single(a => a.Id == rlayer.RackId);
                insInfo.InPackCode = rkitem.RouteCode;
                insInfo.InPackLayer = rlayer.RackLayerNum.ToString("x2");
            }
            var rlayer2 = x_rlService.Get(a => a.RackLayerCode == rackcode2).FirstOrDefault();
            if (rlayer2 != null)
            {
                var rkitem = rackList.Single(a => a.Id == rlayer2.RackId);
                insInfo.OutPackCode = rkitem.RouteCode;
                insInfo.OutPackLayer = rlayer2.RackLayerNum.ToString("x2");
            }

            
            TruckInfo freetruck = getLatestTruck(insInfo.InPackCode); //寻找最近空闲叉车
            if (freetruck == null)
            {
                insInfo.StatusEnum = InstructionStatus.NoTruck;
                return insInfo;
            }
            string truckcode = freetruck.Truckcode;
            string truckcoderoute = freetruck.TruckrouteCode;
            
            insInfo.StartCode = insInfo.getStartType(StartType.Start);
            insInfo.InOut = insInfo.getIOType(iotype);
            insInfo.TruckCode = truckcode;
            
            
            insInfo.TuopanCode = tuopancode;
            if (iotype == InOutType.In)
            {
                insInfo.RackLayerCode = rackcode2;
            }
            else
            {
                insInfo.RackLayerCode = rackcode1;
            }

            var packList = routelist.Where(a => a.Isstart == 2);
            TruckService x_tkServic = new TruckService();
            var tkList = x_tkServic.Get(a => a.Isvalid == 1 && a.HouseId == HouseId);

            //排除掉已占用的停车位，还需要排除掉已被规划的停车位
            foreach (var item in packList)
            {
                foreach (var titem in tkList)
                {
                    if (titem.TruckrouteCode == item.Routecode)
                    {
                        item.IsValid = 0;
                    }
                }
            }
            foreach (var item in waylist)
            {
                var code = item[item.Count - 1].RouteCode;
                foreach (var pitem in packList)
                {
                    if (pitem.Routecode == code)
                    {
                        pitem.IsValid = 0;
                    }
                }
            }
            string endroute = "";
            var end_item = packList.Where(a => a.IsValid == 1).FirstOrDefault();
            if (end_item != null)
            {
                endroute = end_item.Routecode;
            }
            else
            {
                endroute = truckcoderoute;
                end_item = routelist.Where(a => a.Routecode == endroute).First();
            }

            //开始计算路径
            
            PathMathHelper.InitRouteNodes(nodeDict);
            PathMathResult result = PathMathHelper.Start(truckcoderoute, insInfo.InPackCode);   //取货路径
            string path1 = truckcoderoute + result.Path;   //取货路径
            insInfo.PathList.Add(path1);
            PathMathHelper.InitRouteNodes(nodeDict);
            result = PathMathHelper.Start(insInfo.InPackCode, insInfo.OutPackCode);
            string path2 = result.Path;     //卸货路径
            insInfo.PathList.Add(path2);
            PathMathHelper.InitRouteNodes(nodeDict);
            result = PathMathHelper.Start(insInfo.OutPackCode, endroute);      //停车路径
            string path3 = result.Path;     //停车路径
            insInfo.PathList.Add(path3);
            var rack_item = rackList.Where(a => a.Rackcode == insInfo.InPackCode).FirstOrDefault();
            var out_rack_item = rackList.Where(a => a.Rackcode == insInfo.OutPackCode).FirstOrDefault();

            ComputePath(insInfo, path1, path2, rack_item);
            ComputePath(insInfo, path2, path3, out_rack_item);
            ComputePath(insInfo, path3);

            //如果终点是停车位，则反转停车位的转向
            if (end_item != null && end_item.Isstart == 2)  
            {
                if (insInfo.RouteWay.Count > 2)
                {
                    var wayinfo = insInfo.RouteWay.Last();
                    if (wayinfo.Way == TurnWay.Left)
                    {
                        wayinfo.Way = TurnWay.Right;
                        insInfo.AddItem(wayinfo.RouteCode, TurnWay.Back);
                    }
                    else if (wayinfo.Way == TurnWay.Right)
                    {
                        wayinfo.Way = TurnWay.Left;
                        insInfo.AddItem(wayinfo.RouteCode, TurnWay.Back);
                    }
                }
            }
            insInfo.AddItem(end_item.Routecode, TurnWay.Stop);

            waylist.Add(insInfo.RouteWay);
            return insInfo;
        }

        /// <summary>
        /// 寻找最近的空闲叉车
        /// </summary>
        /// <param name="routecode"></param>
        /// <returns></returns>
        private TruckInfo getLatestTruck(string routecode)
        {
            var trucklist = x_tkService.Get(a => a.Isvalid == 1 && a.Truckstatus == 0 && a.HouseId == HouseId);
            int count = trucklist.Count;
            if (count > 0)
            {
                if (count == 1)
                {
                    return trucklist[0];
                }
                int lastMin = 0;
                int i = 0, j = 0;
                foreach (var item in trucklist) //寻找最近的空闲叉车
                {
                    PathMathHelper.InitRouteNodes(nodeDict);
                    PathMathResult result = PathMathHelper.Start(item.TruckrouteCode, routecode);   //取货路径
                    if (result.Status)
                    {
                        
                        if(result.PathLong < lastMin)
                        {                            
                            j = i;
                        }
                        if (lastMin > result.PathLong)
                        {
                            lastMin = result.PathLong;
                        }
                        else if (lastMin == 0)
                        {
                            lastMin = result.PathLong;
                        }
                    }
                    i++;
                }
                return trucklist[j];
            }
            return null;
        }

        /// <summary>
        /// 初始化转向
        /// </summary>
        DirectionWay direction = DirectionWay.None;
        string lastRoute = "";
        /// <summary>
        /// 计算路径以及转向逻辑
        /// </summary>
        /// <param name="insInfo"></param>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="rackItem"></param>
        private void ComputePath(InstructionInfo insInfo, string path1, string path2 = null, RackInfo rackItem = null)
        {
            string[] path1Arr = path1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int path1Length = path1Arr.Length;
            for (int i = 0; i < path1Length; i++)
            {
                string path = path1Arr[i];
                if (i == 0 && !string.IsNullOrEmpty(lastRoute)) //如果取/卸货仓位是紧邻的仓位，则指令需要跳过下一位置
                {
                    var rackInfo = rackList.Where(a => a.RouteCode == path).FirstOrDefault();
                    if (rackInfo != null)
                    {
                        var lastInfo = rackList.Where(a => a.RouteCode == lastRoute).FirstOrDefault();
                        if (lastInfo != null)
                        {
                            continue;
                        }
                    }
                }
                lastRoute = path;
                var item = routelist.Where(a => a.Routecode == path1Arr[i]).FirstOrDefault();
                if (i + 1 < path1Length)    //还有下一节点
                {
                    string next = path1Arr[i + 1];
                    var nextItem = routelist.Where(a => a.Routecode == next).FirstOrDefault();
                    int xsub = item.X - nextItem.X;
                    int ysub = item.Y - nextItem.Y;
                    if (xsub == 0)
                    {
                        if (ysub > 0)   //向南
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.South));
                            direction = DirectionWay.South;

                        }
                        else  //向北
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.North));

                            direction = DirectionWay.North;
                        }
                    }
                    else if (ysub == 0)
                    {
                        if (xsub > 0)  //向西
                        {
                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.West));

                            direction = DirectionWay.West;
                        }
                        else  //向东
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.Easet));

                            direction = DirectionWay.Easet;
                        }
                    }

                }
                else if (!string.IsNullOrEmpty(path2))  //一期默认货架在右侧，即X正，Y负
                {
                    setRouteByRack(insInfo, path2, path, item, rackItem);
                }
            }
        }

        /// <summary>
        /// 计算仓位的转向逻辑
        /// </summary>
        /// <param name="insInfo"></param>
        /// <param name="nextPathArray"></param>
        /// <param name="lastPath"></param>
        /// <param name="item"></param>
        /// <param name="rackItem"></param>
        private void setRouteByRack(InstructionInfo insInfo, string nextPathArray, string lastPath, RouteInfo item, RackInfo rackItem)
        {
            string[] path2Arr = nextPathArray.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string next = path2Arr[0];
            var nextItem = routelist.Where(a => a.Routecode == next).FirstOrDefault();
            int xsub = item.X - nextItem.X;
            int ysub = item.Y - nextItem.Y;
            int xrack = 0;
            int yrack = 0;
            if (rackItem != null)
            {
                xrack = item.X - rackItem.X;
                yrack = item.Y - rackItem.Y;
            }
            if (xsub == 0)
            {
                if (ysub > 0)   //向南
                {
                    if (lastPath == insInfo.InPackCode || lastPath == insInfo.OutPackCode)
                    {
                        if (direction == DirectionWay.South)
                        {
                            if (xrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.North)
                        {
                            if (xrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                    }
                    direction = DirectionWay.South;
                }
                else  //向北
                {
                    if (lastPath == insInfo.InPackCode || lastPath == insInfo.OutPackCode)
                    {
                        if (direction == DirectionWay.South)
                        {
                            if (xrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.North)
                        {
                            if (xrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.Easet)
                        {

                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                    }
                    direction = DirectionWay.North;
                }
            }
            else if (ysub == 0)
            {
                if (xsub > 0)  //向西
                {
                    if (lastPath == insInfo.InPackCode || lastPath == insInfo.OutPackCode)
                    {
                        if (direction == DirectionWay.South)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Left);
                        }
                        else if (direction == DirectionWay.Easet)
                        {
                            if (yrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            if (yrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Right);
                        }
                    }
                    direction = DirectionWay.West;
                }
                else  //向东
                {
                    if (lastPath == insInfo.InPackCode || lastPath == insInfo.OutPackCode)
                    {
                        if (direction == DirectionWay.South)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Right);
                        }
                        else if (direction == DirectionWay.Easet)
                        {
                            if (yrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            if (yrack > 0)  //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Left);
                        }
                    }
                    direction = DirectionWay.Easet;
                }
            }
        }

        private Dictionary<string, List<DNode>> SetToNodes()
        {
            Dictionary<string, List<DNode>> dict = new Dictionary<string, List<DNode>>();
            foreach (var item in routelist)
            {
                List<DNode> dnList = new List<DNode>();
                if (!string.IsNullOrEmpty(item.Nextroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Nextroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Nextroutecode, item.Nextroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Leftroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Leftroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Leftroutecode, item.Leftroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Rightroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Rightroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Rightroutecode, item.Rightroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Backroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Backroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Backroutecode, item.Backroutevalue);
                        dnList.Add(dn);
                    }
                }
                dict.Add(item.Routecode, dnList);
            }
            return dict;
        }

        /// <summary>
        /// 将叉车动态写入
        /// </summary>
        /// <param name="truckcode"></param>
        /// <param name="routecode"></param>
        public void AddDynamicRoute(string truckcode, string routecode)
        {
            if (dynamicRoute == null)
            {
                var list = x_tkService.Get(a => a.Isvalid == 1);
                foreach (var item in list)
                {
                    dynamicRoute.Add(item.Truckcode, item.TruckrouteCode);
                }
            }
            if (dynamicRoute.ContainsKey(truckcode))
            {
                dynamicRoute[truckcode] = routecode;
            }
            else
            {
                dynamicRoute.Add(truckcode, routecode);
            }
        }

        /// <summary>
        /// 在十字路口的节点上，查询是否有其他叉车也在相对应十字节点上
        /// </summary>
        /// <param name="routecode"></param>
        /// <returns></returns>
        public bool IsOnStop(string routecode)
        {
            var curitem = centerlist.Where(a => a.Last1 == routecode || a.Last2 == routecode).FirstOrDefault();
            if (curitem != null)
            {
                foreach (var item in dynamicRoute)
                {
                    if (item.Value == curitem.Last2 || item.Value == curitem.Last1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 查询该节点是否是离开节点，且是否可以让等待的叉车继续行走，如有则返回truckcode，否则返回null
        /// </summary>
        /// <param name="routecode"></param>
        /// <returns></returns>
        public string IsOnLeave(string routecode)
        {
            var curitem = centerlist.Where(a => a.Next2 == routecode || a.Next1 == routecode).FirstOrDefault();
            if (curitem != null)
            {
                foreach (var item in dynamicRoute)
                {
                    if (item.Value == curitem.Last1 || item.Value == curitem.Last2)
                    {
                        return item.Key;
                    }
                }
            }

            return null;
        }
    }
}
