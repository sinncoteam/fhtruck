/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：TruckLogMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:49 
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
    /// TruckLogMap  领域层实体映射Map
    /// </summary>
    public class TruckLogMap : DMClassMap<TruckLog>
    {
        public TruckLogMap()
        {
            Table("t_l_truck_log");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.Truckcode, "truckcode");
            Map(a => a.Routecode, "routecode");
            Map(a => a.Createtime, "createtime");
            Map(a => a.RunStatus, "runstatus");
            Map(a => a.FullCode, "fullcode");
        }
    }
}