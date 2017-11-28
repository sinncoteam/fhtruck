/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：RouteMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:29 
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
    /// RouteMap  领域层实体映射Map
    /// </summary>
    public class RouteMap : DMClassMap<Route>
    {
        public RouteMap()
        {
            Table("t_d_route");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.HouseId, "house_id");
            Map(a => a.Rfcode, "rfcode");
            Map(a => a.Routecode, "routecode");
            Map(a => a.Isstart, "isstart");
            Map(a => a.Routewaycount, "routewaycount");
            Map(a => a.Nextroutecode, "nextroutecode");
            Map(a => a.Nextroutevalue, "nextroutevalue");
            Map(a => a.Leftroutecode, "leftroutecode");
            Map(a => a.Leftroutevalue, "leftroutevalue");
            Map(a => a.Rightroutecode, "rightroutecode");
            Map(a => a.Rightroutevalue, "rightroutevalue");
            Map(a => a.Backroutecode, "backroutecode");
            Map(a => a.Backroutevalue, "backroutevalue");
            Map(a => a.X, "x");
            Map(a => a.Y, "y");
            Map(a => a.Createtime, "createtime");
            Map(a => a.CreateoperatorId, "createoperator_id");
            Map(a => a.Updatetime, "updatetime");
            Map(a => a.UpdateoperatorId, "updateoperator_id");
            Map(a => a.IsValid, "isvalid");
        }
    }
}