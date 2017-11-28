using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Dispatch.Domain.Info
{
    /// <summary>
    /// 十字点阵组合
    /// </summary>
    public class CenterRouteInfo
    {
        /// <summary>
        /// 中心点
        /// </summary>
        public string Center { get; set; }
        /// <summary>
        /// 上一节点1
        /// </summary>
        public string Last1 { get; set; }
        /// <summary>
        /// 上一节点2
        /// </summary>
        public string Last2 { get; set; }
        /// <summary>
        /// 下一节点1
        /// </summary>
        public string Next1 { get; set; }
        /// <summary>
        /// 下一节点2
        /// </summary>
        public string Next2 { get; set; }
    }
}
