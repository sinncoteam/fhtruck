using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Dispatch.Domain.Model
{
    public class RackLayer
    {
        public int ID { get; set; }
        public int RackId { get; set; }
        public string RackLayerCode { get; set; }
        public int RackLayerNum { get; set; }
        public int RackStatus { get; set; }
        public string PackageCode { get; set; }
        public int IsValid { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateoperatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateoperatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        /// <summary>
        /// 外部仓位码
        /// </summary>
        public string OutRackLayerCode { get; set; }
    }
}
