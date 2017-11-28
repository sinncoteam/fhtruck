/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：Operator.cs
// 文件功能描述： 领域层实体定义(Model)
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:10 
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
    /// Operator   领域层实体定义(Model)
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Realname { get; set; }

        /// <summary>
        /// 性别，0不详，1男，2女
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int Isvalid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime Lastlogintime { get; set; }


    }
}