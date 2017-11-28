/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：Truck.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// Truck   领域层实体定义(Model)
    /// </summary>
    public class Truck
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }
        public virtual int HouseId { get; set; }
        /// <summary>
        /// 叉车编号
        /// </summary>
        public virtual string Truckcode { get; set; }

        /// <summary>
        /// 叉车名称
        /// </summary>
        public virtual string Truckname { get; set; }

        /// <summary>
        /// 叉车运行状态，0可使用，1运行中
        /// </summary>
        public virtual int Truckstatus { get; set; }

        /// <summary>
        /// 叉车当前位置，路径点ID
        /// </summary>
        public virtual string TruckrouteCode { get; set; }

        /// <summary>
        /// 当前操作员
        /// </summary>
        public virtual int OperatorId { get; set; }

        /// <summary>
        /// 有效状态，0无效，1有效
        /// </summary>
        public virtual int Isvalid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime Updatetime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual int CreateoperatorId { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual int UpdateoperatorId { get; set; }

        public virtual string OutTruckCode { get; set; }
    }
}