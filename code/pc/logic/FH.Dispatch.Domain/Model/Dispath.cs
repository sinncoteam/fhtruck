using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Dispatch.Domain.Model
{
    public class Dispath
    {
        public int ID { get; set; }
        public string DispatchCode { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 0未读取，1已处理，2处理失败
        /// </summary>
        public int Status { get; set; }
    }
}
