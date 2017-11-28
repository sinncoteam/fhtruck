/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：Rack.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// Rack   领域层实体定义(Model)
    /// </summary>
    public class Rack
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        public virtual int HouseId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Rackcode { get; set; }

        /// <summary>
        /// 仓位名称
        /// </summary>
        public virtual string Rackname { get; set; }

        /// <summary>
        /// 仓位层数
        /// </summary>
        public virtual int RacklayerCount { get; set; }

        /// <summary>
        /// 仓位路径RF点
        /// </summary>
        public virtual string RouteCode { get; set; }

        /// <summary>
        /// 有效状态，0无效，1有效
        /// </summary>
        public virtual int Isvalid { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual int CreateoperatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual int UpdateoperatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime Updatetime { get; set; }

        /// <summary>
        /// 外部仓位码
        /// </summary>
        public string OutRackcode { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}