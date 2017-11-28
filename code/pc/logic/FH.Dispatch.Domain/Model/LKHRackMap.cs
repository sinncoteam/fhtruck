using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace FH.Dispatch.Domain.Model
{
    public class LKHRackMap : DMClassMap<LKHRack>
    {
        public LKHRackMap()
        {
            Table("LKH_RACK");
            Id(a => a.RackCode, "RACKCODE");
            Map(a => a.RackName, "RACKNAME");
            Map(a => a.HouseId, "HOUSEID");
        }
    }
}
