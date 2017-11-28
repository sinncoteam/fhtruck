using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Utils
{
    public class AjaxMsgResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public string Code { get; set; }
        public object Source { get; set; }
    }
}
