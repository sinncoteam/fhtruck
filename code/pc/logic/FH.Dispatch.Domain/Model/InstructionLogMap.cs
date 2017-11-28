/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：InstructionLogMap.cs
// 文件功能描述： 领域层实体映射Map
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
using ViData;

namespace FH.Dispatch.Domain.Model
{
    /// <summary>
    /// InstructionLogMap  领域层实体映射Map
    /// </summary>
    public class InstructionLogMap : DMClassMap<InstructionLog>
    {
        public InstructionLogMap()
        {
            Table("t_l_instruction_log");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.Instruction, "instruction");
            Map(a => a.Truckcode, "truckcode");
            Map(a => a.Createtime, "createtime");
            Map(a => a.OperatorId, "operator_id");
        }
    }
}