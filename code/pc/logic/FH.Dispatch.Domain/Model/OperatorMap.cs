/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：OperatorMap.cs
// 文件功能描述： 领域层实体映射Map
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
using ViData;

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// OperatorMap  领域层实体映射Map
    /// </summary>
    public class OperatorMap : DMClassMap<Operator>
    {
        public OperatorMap()
        {
            Table("t_d_operator");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.Username, "username");
            Map(a => a.Password, "password");
            Map(a => a.Realname, "realname");
            Map(a => a.Sex, "sex");
            Map(a => a.Phone, "phone");
            Map(a => a.Isvalid, "isvalid");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Lastlogintime, "lastlogintime");
        }
    }
}