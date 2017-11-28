using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Common.Utils.Dijkstra
{
    public class GNode
    {
        public string Name;
        public List<DNode> LD;
        public int Rank;
        public int MinDist = 0;
        public string Path = "";
        public bool BFind = false;
        public GNode(string name)
        {
            this.Name = name;
            LD = new List<DNode>();
            Rank = -1;
        }
        /// <summary>
        /// 设置节点级别
        /// </summary>
        /// <param name="parentNode"></param>
        public void SetRank(GNode parentNode)
        {
            if (Rank == -1)
            {
                if (parentNode != null)
                    Rank = parentNode.Rank + 1;
                else
                    Rank = 0;
            }
        }
        public int GetRank()
        {
            return Rank;
        }
    }
}
