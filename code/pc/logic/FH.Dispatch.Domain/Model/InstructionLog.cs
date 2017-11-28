/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：InstructionLog.cs
// 文件功能描述： 领域层实体定义(Model)
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:43 
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
    /// InstructionLog   领域层实体定义(Model)
    /// </summary>
    public class InstructionLog
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 指令
        /// </summary>
        public virtual string Instruction { get; set; }

        /// <summary>
        /// 叉车编码
        /// </summary>
        public virtual string Truckcode { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public virtual int OperatorId { get; set; }


    }
}