using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace FH.Dispatch.Domain.Model
{
    public class RackLayerMap : DMClassMap<RackLayer>
    {
        public RackLayerMap()
        {
            Table("t_d_rack_layer");
            Id(a => a.ID).Identity();
            Map(a => a.RackId, "rack_id");
            Map(a => a.RackLayerCode, "racklayercode");
            Map(a => a.RackLayerNum, "racklayernum");
            Map(a => a.RackStatus, "rackstatus");
            Map(a => a.PackageCode, "package_code");
            Map(a => a.IsValid, "isvalid");
            Map(a => a.CreateoperatorId, "createoperator_id");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Updatetime, "updatetime");
            Map(a => a.UpdateoperatorId, "updateoperator_id");
            Map(a => a.OutRackLayerCode, "out_racklayercode");
        }
    }
}
