using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FH.Systems.Domain.Info;

namespace FH.Cms.Models
{
    public class OperatorEditDto
    {
        public OperatorInfo OperInfo { get; set; }
    }

    public class OperatorRoleEditDto
    {
        public RoleInfo RlInfo { get; set; }
    }
}