using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace FH.Dispatch.Domain.Model
{
    public class DispathMap : DMClassMap<Dispath>
    {
        public DispathMap()
        {
            Table("t_d_dispatch");
            Id(a => a.ID).Identity();
            Map(a => a.DispatchCode, "dispatchcode");
            Map(a => a.CreateTime, "createtime");
            Map(a => a.Status, "status");
        }
    }
}
