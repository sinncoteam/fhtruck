using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FH.Common.Utils.Dijkstra
{
    public class PathMathResult
    {
        public string Path { get; set; }
        public int PathLong { get; set; }
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
    }

    public class PathMathHelper
    {
        public static Hashtable ht ;
        public static int minlon;

        public static PathMathResult Start(string start, string end)
        {

            //InitNodesSimple();
            PathMathResult result = new PathMathResult();
            GNode gnStart = ht[start] as GNode;
            GNode gnEnd = ht[end] as GNode;
            if (gnStart == null || gnEnd == null)
            {
                result.Status = false;
                result.StatusMsg = "开始节点或结尾节点不正确";
                return result;
            }
            gnStart.SetRank(null);
            gnStart.BFind = true;
            List<GNode> arrGN = new List<GNode>();
            arrGN.Add(gnStart);
            InitRank(arrGN);
            FindMin(arrGN, gnEnd);
            result.Status = true;
            result.Path = gnEnd.Path;
            result.PathLong = minlon;
            return result;
        }

        /// <summary>
        /// 初始化所有路径点
        /// </summary>
        /// <param name="dict"></param>
        public static void InitRouteNodes(Dictionary<string, List<DNode>> dict)
        {
            ht = new Hashtable();
            minlon = 0;
            foreach (var item in dict)
            {
                GNode dnTemp = new GNode(item.Key);
                foreach (var subItem in item.Value)
                {
                    dnTemp.LD.Add(subItem);
                }
                ht.Add(item.Key, dnTemp);
            }
        }
        #region example
        ///// <summary>
        ///// 初始化
        ///// </summary>
        //public static void InitNodes()
        //{
        //    ht = new Hashtable();
        //    GNode dnTemp = new GNode("A");
        //    ht.Add("A", dnTemp);
        //    dnTemp = new GNode("B");
        //    ht.Add("B", dnTemp);
        //    dnTemp = new GNode("C");
        //    ht.Add("C", dnTemp);
        //    dnTemp = new GNode("D");
        //    ht.Add("D", dnTemp);
        //    dnTemp = new GNode("E");
        //    ht.Add("E", dnTemp);
        //    dnTemp = new GNode("F");
        //    ht.Add("F", dnTemp);
        //    dnTemp = new GNode("G");
        //    ht.Add("G", dnTemp);
        //    dnTemp = new GNode("H");
        //    ht.Add("H", dnTemp);
        //    dnTemp = new GNode("I");
        //    ht.Add("I", dnTemp);
        //    dnTemp = new GNode("J");
        //    ht.Add("J", dnTemp);
        //    dnTemp = new GNode("K");
        //    ht.Add("K", dnTemp);
        //    dnTemp = new GNode("L");
        //    ht.Add("L", dnTemp);

        //    dnTemp = ht["A"] as GNode;
        //    dnTemp.LD.Add(new DNode("B", 3));
        //    dnTemp.LD.Add(new DNode("C", 5));
        //    dnTemp.LD.Add(new DNode("D", 2));

        //    dnTemp = ht["B"] as GNode;
        //    dnTemp.LD.Add(new DNode("A", 3));
        //    dnTemp.LD.Add(new DNode("C", 4));
        //    dnTemp.LD.Add(new DNode("E", 10));

        //    dnTemp = ht["C"] as GNode;
        //    dnTemp.LD.Add(new DNode("A", 5));
        //    dnTemp.LD.Add(new DNode("B", 4));
        //    dnTemp.LD.Add(new DNode("D", 2));
        //    dnTemp.LD.Add(new DNode("G", 6));
        //    dnTemp.LD.Add(new DNode("F", 1));

        //    dnTemp = ht["D"] as GNode;
        //    dnTemp.LD.Add(new DNode("A", 2));
        //    dnTemp.LD.Add(new DNode("C", 2));
        //    dnTemp.LD.Add(new DNode("H", 3));

        //    dnTemp = ht["E"] as GNode;
        //    dnTemp.LD.Add(new DNode("B", 10));
        //    dnTemp.LD.Add(new DNode("F", 4));
        //    dnTemp.LD.Add(new DNode("I", 2));

        //    dnTemp = ht["F"] as GNode;
        //    dnTemp.LD.Add(new DNode("C", 1));
        //    dnTemp.LD.Add(new DNode("E", 4));
        //    dnTemp.LD.Add(new DNode("K", 8));
        //    dnTemp.LD.Add(new DNode("L", 2));

        //    dnTemp = ht["G"] as GNode;
        //    dnTemp.LD.Add(new DNode("H", 8));
        //    dnTemp.LD.Add(new DNode("C", 6));
        //    dnTemp.LD.Add(new DNode("L", 2));

        //    dnTemp = ht["H"] as GNode;
        //    dnTemp.LD.Add(new DNode("G", 8));
        //    dnTemp.LD.Add(new DNode("D", 3));

        //    dnTemp = ht["I"] as GNode;
        //    dnTemp.LD.Add(new DNode("J", 1));
        //    dnTemp.LD.Add(new DNode("E", 2));
        //    dnTemp.LD.Add(new DNode("K", 6));

        //    dnTemp = ht["J"] as GNode;
        //    dnTemp.LD.Add(new DNode("I", 1));
        //    dnTemp.LD.Add(new DNode("K", 9));

        //    dnTemp = ht["K"] as GNode;
        //    dnTemp.LD.Add(new DNode("F", 8));
        //    dnTemp.LD.Add(new DNode("I", 6));
        //    dnTemp.LD.Add(new DNode("J", 9));
        //    dnTemp.LD.Add(new DNode("L", 5));

        //    dnTemp = ht["L"] as GNode;
        //    dnTemp.LD.Add(new DNode("G", 2));
        //    dnTemp.LD.Add(new DNode("F", 2));
        //    dnTemp.LD.Add(new DNode("K", 5));
        //}

        ///// <summary>
        ///// 初始化simple
        ///// </summary>
        //public static void InitNodesSimple()
        //{
        //    ht = new Hashtable();
        //    GNode dnTemp = new GNode("A");
        //    ht.Add("A", dnTemp);
        //    dnTemp = new GNode("B");
        //    ht.Add("B", dnTemp);
        //    dnTemp = new GNode("C");
        //    ht.Add("C", dnTemp);
        //    dnTemp = new GNode("D");
        //    ht.Add("D", dnTemp);
        //    dnTemp = new GNode("E");
        //    ht.Add("E", dnTemp);
        //    dnTemp = new GNode("F");
        //    ht.Add("F", dnTemp);
        //    dnTemp = new GNode("J");
        //    ht.Add("J", dnTemp);
        //    dnTemp = new GNode("K");
        //    ht.Add("K", dnTemp);

        //    dnTemp = ht["A"] as GNode;
        //    dnTemp.LD.Add(new DNode("B", 10)); 

        //    dnTemp = ht["B"] as GNode;
        //    dnTemp.LD.Add(new DNode("A", 10));
        //    dnTemp.LD.Add(new DNode("C", 10)); 

        //    dnTemp = ht["C"] as GNode; 
        //    dnTemp.LD.Add(new DNode("B", 4));
        //    dnTemp.LD.Add(new DNode("D", 2)); 

        //    dnTemp = ht["D"] as GNode; 
        //    dnTemp.LD.Add(new DNode("C", 2));
        //    dnTemp.LD.Add(new DNode("E", 3));

        //    dnTemp = ht["E"] as GNode;
        //    dnTemp.LD.Add(new DNode("D", 10));
        //    dnTemp.LD.Add(new DNode("F", 4));

        //    dnTemp = ht["F"] as GNode;
        //    dnTemp.LD.Add(new DNode("E", 4));
        //    dnTemp.LD.Add(new DNode("J", 8));


        //    dnTemp = ht["J"] as GNode;
        //    dnTemp.LD.Add(new DNode("F", 1));
        //    dnTemp.LD.Add(new DNode("K", 9));

        //    dnTemp = ht["K"] as GNode;
        //    dnTemp.LD.Add(new DNode("J", 8));

        //}
        #endregion
        /// <summary>
        /// 初始化点阵的Rank
        /// </summary>
        /// <param name="srcs"></param>
        public static void InitRank(List<GNode> srcs)
        {
            List<GNode> nextNode = new List<GNode>();
            foreach (GNode src in srcs)
            {
                foreach (DNode dn in src.LD)
                {
                    dn.CurrNode.SetRank(src);
                    if (dn.CurrNode.Rank == (src.Rank + 1) && !nextNode.Contains(dn.CurrNode))
                        nextNode.Add(dn.CurrNode);
                }
            }
            if (nextNode.Count > 0)
                InitRank(nextNode);
        }

        /// <summary>
        /// 寻找起始节点到目标节点的最小路径，此处采用递归查找。目标节点固定，起始节点递归。
        /// </summary>
        /// <param name="src">起始节点，为临时递归节点</param>
        /// <param name="dest">查找路径中的目标节点</param>
        /// <param name="minx">当前查找最小路径值，此值在递归中共享</param>
        /// <param name="startDist">当前节点以src节点的距离</param>
        /// <param name="srcRank">源节点src的级别</param>
        /// <param name="path">查找中经过的路径</param>
        /// <returns></returns>
        public static string FindMinx(GNode src, GNode dest, ref int minx, int startDist, int srcRank, string path)
        {
            // string[] paths=
            int sRank = src.Rank;// dest.GetRank();
            int tmpLength, tmpRank;
            string tmpPath = null;
            string goalPath = "";
            string tmpPath1, tmpPath2, tmpNodeName;
            tmpPath1 = "," + path + ",";
            tmpPath2 = "," + src.Path + ",";
            foreach (DNode dn in src.LD)
            {
                tmpPath = path;
                dn.CurrNode.SetRank(src);
                tmpRank = dn.CurrNode.Rank;
                tmpNodeName = "," + dn.CurrNode.Name + ",";
                //扩散级别大于等于目标级别并且是未走过的节点。
                // had delete tmpRank >= srcRank
                //if (tmpRank >＝ srcRank && path.IndexOf(dn.CurrNode.Name) == -1 && src.Path.IndexOf(dn.CurrNode.Name)==-1)
                if (tmpRank > srcRank && tmpPath1.IndexOf(tmpNodeName) == -1 && tmpPath2.IndexOf(tmpNodeName) == -1)
                {
                    tmpLength = dn.Distance + startDist;
                    if (dn.CurrNode.Equals(dest))
                    {
                        if (minx > tmpLength)
                        {
                            minx = tmpLength;
                            tmpPath += "," + dn.CurrNode.Name;
                            goalPath = tmpPath;
                        }
                        else if (minx == tmpLength)
                        {
                            tmpPath += "," + dn.CurrNode.Name;
                            goalPath = tmpPath;
                        }
                    }
                    else
                    {
                        if (tmpLength < minx)//路程小于最小值，查询下个子节点
                        {
                            tmpPath += "," + dn.CurrNode.Name;
                            tmpPath = FindMinx(dn.CurrNode, dest, ref minx, tmpLength, srcRank, tmpPath);
                            if (tmpPath != "")
                                goalPath = tmpPath;
                        }
                    }

                }
            }
            return goalPath;
        }

        public static bool FindMin(List<GNode> srcs, GNode dest)
        {
            string tmpPath;
            int destRank = dest.GetRank();
            int tempDestRank;
            int minLen = 0;            
            bool isFind = false;
            List<GNode> nextNodes = new List<GNode>();
            foreach (GNode src in srcs)
            {
                if (src.Equals(dest)) return isFind;
                foreach (DNode dn in src.LD)
                {
                    tempDestRank = dn.CurrNode.Rank;
                    if (tempDestRank == (src.Rank + 1))
                    {
                        if (!nextNodes.Contains(dn.CurrNode))
                        {
                            nextNodes.Add(dn.CurrNode);
                        }
                        dn.CurrNode.MinDist = src.MinDist + dn.Distance;
                        if (dn.CurrNode.Equals(dest))
                        {
                            minLen = src.MinDist + dn.Distance;
                            isFind = true;
                            //break;
                        }
                    }
                }
            }
            if (isFind)
            {
                foreach (GNode src in srcs)
                {
                    tmpPath = FindMinx(src, dest, ref minLen, src.MinDist, src.Rank, "");
                    if (tmpPath != "")
                    {
                        dest.Path = src.Path + tmpPath;
                        dest.MinDist = minLen;
                        minlon += minLen;
                    }
                }
            }
            else
            {

                foreach (GNode goal in nextNodes)
                {
                    minLen = -1;
                    foreach (GNode src in srcs)
                    {
                        if (minLen == -1) minLen = goal.MinDist;
                        tmpPath = FindMinx(src, goal, ref minLen, src.MinDist, src.Rank, "");
                        if (tmpPath != "")
                        {
                            //dn.CurrNode.BFind = true;
                            goal.Path = src.Path + tmpPath;
                            goal.MinDist = minLen;
                            
                        }
                    }
                }
                FindMin(nextNodes, dest);
            }


            return isFind;
        }
    }
}
