using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FH.Common.Utils.Dijkstra
{
    public class DNode
    {
        string Name;
        public int Distance;
        public GNode CurrNode
        {
            get { return PathMathHelper.ht[Name] as GNode; }
        }
        public DNode(string name, int dist)
        {
            this.Name = name;
            Distance = dist;
        }
    }
}
