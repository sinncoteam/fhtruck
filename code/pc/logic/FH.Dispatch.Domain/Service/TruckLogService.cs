/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。  
//
// 文件名：TruckLogService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2016/12/8 10:17:18 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViData;
using FH.Dispatch.Domain.Model;
using FH.Dispatch.Domain.Info;


namespace FH.Dispatch.Domain.Service
{
    /// <summary>
    /// TruckLogService  领域层服务实现
    /// </summary>
    public class TruckLogService : Repository<TruckLogInfo, TruckLog>
    {

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public IList<TruckLogInfo> GetByPage(PagingInfo pi)
        {
            pi.TableName = " t_l_truck_log tl";
            pi.Fileds = " *";
            pi.SortFields = " id desc ";
            return this.GetPaging(pi);
        }
    }
}
