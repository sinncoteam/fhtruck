/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：TruckMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:36 
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
    /// TruckMap  领域层实体映射Map
    /// </summary>
    public class TruckMap : DMClassMap<Truck>
    {
        public TruckMap()
        {
            Table("t_d_truck");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.HouseId, "house_id");
            Map(a => a.Truckcode, "truckcode");
            Map(a => a.Truckname, "truckname");
            Map(a => a.Truckstatus, "truckstatus");
            Map(a => a.TruckrouteCode, "truckroutecode");
            Map(a => a.OperatorId, "operator_id");
            Map(a => a.Isvalid, "isvalid");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Updatetime, "updatetime");
            Map(a => a.CreateoperatorId, "createoperator_id");
            Map(a => a.UpdateoperatorId, "updateoperator_id");
            Map(a => a.OutTruckCode, "out_truckcode");
        }
    }
}