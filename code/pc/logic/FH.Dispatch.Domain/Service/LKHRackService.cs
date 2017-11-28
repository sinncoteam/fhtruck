using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Domain.Model;
using ViData;

namespace FH.Dispatch.Domain.Service
{
    public class LKHRackService : Repository<LKHRack, LKHRack>
    {
        public LKHRackService()
        {
            this.DBString = "orc";
        }
    }
}
