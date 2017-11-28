/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：RackMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:17 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// RackMap  领域层实体映射Map
    /// </summary>
    public class RackMap : DMClassMap<Rack>
    {
        public RackMap()
        {
            Table("t_d_rack");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.HouseId, "house_id");
            Map(a => a.Rackcode, "rackcode");
            Map(a => a.Rackname, "rackname");
            Map(a => a.RacklayerCount, "racklayercount");
            Map(a => a.RouteCode, "routecode");
            Map(a => a.Isvalid, "isvalid");
            Map(a => a.CreateoperatorId, "createoperator_id");
            Map(a => a.Createtime, "createtime");
            Map(a => a.UpdateoperatorId, "updateoperator_id");
            Map(a => a.Updatetime, "updatetime");
            Map(a => a.OutRackcode, "out_rackcode");
            Map(a => a.X, "x");
            Map(a => a.Y, "y");
        }
    }
}