/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：TruckLog.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// TruckLog   领域层实体定义(Model)
    /// </summary>
    public class TruckLog
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 叉车编码
        /// </summary>
        public virtual string Truckcode { get; set; }

        /// <summary>
        /// 路径点ID
        /// </summary>
        public virtual string Routecode { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public virtual string RunStatus { get; set; }

        /// <summary>
        /// 全部指令
        /// </summary>
        public virtual string FullCode { get; set; }
    }
}