﻿/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：OperatorOrg.cs
// 文件功能描述： 领域层实体定义(Model)
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:42 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Systems.Domain.Model
{
    /// <summary>
    /// OperatorOrg   领域层实体定义(Model)
    /// </summary>
    public class OperatorOrg
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public virtual int OperatorId { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public virtual string OrgCode { get; set; }


    }
}