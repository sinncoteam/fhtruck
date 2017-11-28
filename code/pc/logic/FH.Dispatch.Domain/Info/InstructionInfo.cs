using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Client;

namespace FH.Dispatch.Domain.Info
{
    /// <summary>
    /// 指令集信息
    /// </summary>
    public class InstructionInfo
    {
        public InstructionInfo()
        {
            //RoutePath = new Dictionary<string, TurnWay>();
            RouteWay = new List<TurnWayInfo>();
            PathList = new List<string>();
        }

        public List<string> PathList { get; set; }

        /// <summary>
        /// 调度状态
        /// </summary>
        public InstructionStatus StatusEnum { get; set; }

        public string TruckCode { get; set; }
        /// <summary>
        /// 启动标识，01启动，02停止
        /// </summary>
        public string StartCode { get; set; }
        /// <summary>
        /// 出库入库，01出库，02入库
        /// </summary>
        public string InOut { get; set; }
        /// <summary>
        /// 取货点编码
        /// </summary>
        public string InPackCode { get; set; }
        /// <summary>
        /// 取货点层数
        /// </summary>
        public string InPackLayer { get; set; }
        /// <summary>
        /// 卸货点编码
        /// </summary>
        public string OutPackCode { get; set; }
        /// <summary>
        /// 卸货点层数
        /// </summary>
        public string OutPackLayer { get; set; }
        /// <summary>
        /// 托盘编码
        /// </summary>
        public string TuopanCode { get; set; }
        /// <summary>
        /// 仓位编码
        /// </summary>
        public string RackLayerCode { get; set; }
        ///// <summary>
        ///// RF运行路径集合
        ///// </summary>
        //public Dictionary<string, TurnWay> RoutePath { get; set; }

        public List<TurnWayInfo> RouteWay { get; set; }


        /// <summary>
        /// 生成指令
        /// </summary>
        /// <param name="rackList">仓位列表，用于相邻仓位的屏蔽</param>
        /// <returns></returns>
        public string ResetInstruction(IList<RackInfo> rackList = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.TruckCode).Append(this.StartCode).Append(this.InOut);
            sb.Append(this.InPackCode).Append(this.InPackLayer).Append(this.OutPackCode).Append(this.OutPackLayer);
            int i = 0;
            string lastRoute = "";
            foreach (var item in this.RouteWay)
            {
                if (i > 0)
                {
                    string way = getTurnwayMap(item.Way);
                    sb.Append(item.RouteCode).Append(way);
                    lastRoute = item.RouteCode;
                }
                i++;
            }            
            CRCHelper crcMethod = new CRCHelper();
            string crc = crcMethod.CRCResult_ModBus(sb.ToString());
            sb.Append(crc);
            return sb.ToString();
        }

        public void AddItem(string route, TurnWay way)
        {
            this.RouteWay.Add(new TurnWayInfo() { RouteCode = route, Way = way });
        }

        private string getTurnwayMap(TurnWay way)
        {
            switch (way)
            {
                case TurnWay.Stop: return "05";
                case TurnWay.Next: return "01";
                case TurnWay.Right: return "02";
                case TurnWay.Back: return "03";
                case TurnWay.Left: return "04";
            }
            return "05";
        }
        /// <summary>
        /// 根据方向获取转向
        /// </summary>
        /// <param name="fromWay"></param>
        /// <param name="toWay"></param>
        /// <returns></returns>
        public TurnWay getWay(DirectionWay fromWay, DirectionWay toWay)
        {
            if (fromWay == DirectionWay.None)
            {
                return TurnWay.Next;
            }
            if (fromWay == DirectionWay.Easet)
            {
                switch (toWay)
                {
                    case DirectionWay.Easet: return TurnWay.Next;
                    case DirectionWay.North: return TurnWay.Left;
                    case DirectionWay.South: return TurnWay.Right;
                    case DirectionWay.West: return TurnWay.Back;
                }
            }
            else if (fromWay == DirectionWay.South)
            {
                switch (toWay)
                {
                    case DirectionWay.Easet: return TurnWay.Left;
                    case DirectionWay.North: return TurnWay.Back;
                    case DirectionWay.South: return TurnWay.Next;
                    case DirectionWay.West: return TurnWay.Right;
                }
            }
            else if (fromWay == DirectionWay.North)
            {
                switch (toWay)
                {
                    case DirectionWay.Easet: return TurnWay.Right;
                    case DirectionWay.North: return TurnWay.Next;
                    case DirectionWay.South: return TurnWay.Back;
                    case DirectionWay.West: return TurnWay.Left;
                }
            }
            else if (fromWay == DirectionWay.West)
            {
                switch (toWay)
                {
                    case DirectionWay.Easet: return TurnWay.Back;
                    case DirectionWay.North: return TurnWay.Right;
                    case DirectionWay.South: return TurnWay.Left;
                    case DirectionWay.West: return TurnWay.Next;
                }
            }
            
            return TurnWay.Stop;
        }

        /// <summary>
        /// 根据转向获取当前朝向
        /// </summary>
        /// <param name="fromWay"></param>
        /// <param name="turnWay"></param>
        /// <returns></returns>
        public DirectionWay getDirection(DirectionWay fromWay, TurnWay turnWay)
        {
            if (turnWay == TurnWay.Next)
            {
                return fromWay;
            }
            if (fromWay == DirectionWay.Easet)
            {
                switch (turnWay)
                {
                    case TurnWay.Left: return DirectionWay.North;
                    case TurnWay.Right: return DirectionWay.South;
                    case TurnWay.Back: return DirectionWay.West;
                }
            }
            else if (fromWay == DirectionWay.South)
            {
                switch (turnWay)
                {
                    case TurnWay.Left: return DirectionWay.Easet;
                    case TurnWay.Right: return DirectionWay.West;
                    case TurnWay.Back: return DirectionWay.North;
                }
            }
            else if (fromWay == DirectionWay.West)
            {
                switch (turnWay)
                {
                    case TurnWay.Left: return DirectionWay.South;
                    case TurnWay.Right: return DirectionWay.North;
                    case TurnWay.Back: return DirectionWay.Easet;
                }
            }
            else if (fromWay == DirectionWay.North)
            {
                switch (turnWay)
                {
                    case TurnWay.Left: return DirectionWay.West;
                    case TurnWay.Right: return DirectionWay.Easet;
                    case TurnWay.Back: return DirectionWay.South;
                }
            }
            return DirectionWay.None;
        }

        public string getIOType(InOutType type)
        {
            if (type == InOutType.In) { return "01"; }
            return "02";
        }

        public string getStartType(StartType type)
        {
            int i = Convert.ToInt32(type);
            return i.ToString("x2");
        }
    }

    public enum TurnWay
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 5,
        /// <summary>
        /// 前方
        /// </summary>
        Next = 1,
        /// <summary>
        /// 右
        /// </summary>
        Right = 2,
        /// <summary>
        /// 后
        /// </summary>
        Back = 3,
        /// <summary>
        /// 左
        /// </summary>
        Left = 4
    }

    public enum DirectionWay
    {
        None = 0,
        North = 1,
        Easet = 2,
        South = 3,
        West = 4
    }

    public class TurnWayInfo
    {
        public string RouteCode { get; set; }
        public TurnWay Way { get; set; }
    }

    /// <summary>
    /// 出入库枚举
    /// </summary>
    public enum InOutType
    {
        /// <summary>
        /// 出库
        /// </summary>
        In = 1,
        /// <summary>
        /// 入库
        /// </summary>
        Out = 2
    }

    /// <summary>
    /// 启动标识
    /// </summary>
    public enum StartType
    {
        Start = 1,
        Stop = 2,
        ReStart = 3
    }

    public enum InstructionStatus
    {
        OK = 0,
        /// <summary>
        /// 没有可用叉车
        /// </summary>
        NoTruck = 1,
        /// <summary>
        /// 没有可用停车位
        /// </summary>
        NoStopPos = 2
    }
}
