/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：Route.cs
// 文件功能描述： 领域层实体定义(Model)
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:29 
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
    /// Route   领域层实体定义(Model)
    /// </summary>
    public class Route
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        public virtual int HouseId { get; set; }

        /// <summary>
        /// RF卡编码
        /// </summary>
        public virtual string Rfcode { get; set; }

        /// <summary>
        /// 当前路点编码
        /// </summary>
        public virtual string Routecode { get; set; }

        /// <summary>
        /// 是否起点，0不是，1是
        /// </summary>
        public virtual int Isstart { get; set; }

        /// <summary>
        /// 是否岔路，默认1，大于1则为岔路数
        /// </summary>
        public virtual int Routewaycount { get; set; }

        /// <summary>
        /// 下一路径点ID
        /// </summary>
        public virtual string Nextroutecode { get; set; }

        /// <summary>
        /// 下一路径点权值，单位（毫米）
        /// </summary>
        public virtual int Nextroutevalue { get; set; }

        /// <summary>
        /// 左路径点ID
        /// </summary>
        public virtual string Leftroutecode { get; set; }

        /// <summary>
        /// 左路径点权值
        /// </summary>
        public virtual int Leftroutevalue { get; set; }

        /// <summary>
        /// 右路径点ID
        /// </summary>
        public virtual string Rightroutecode { get; set; }

        /// <summary>
        /// 右路径点权值
        /// </summary>
        public virtual int Rightroutevalue { get; set; }

        /// <summary>
        /// 后路径点ID
        /// </summary>
        public virtual string Backroutecode { get; set; }

        /// <summary>
        /// 后路径点权值
        /// </summary>
        public virtual int Backroutevalue { get; set; }

        public virtual int X { get; set; }

        public virtual int Y { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual int CreateoperatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime Updatetime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual int UpdateoperatorId { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int IsValid { get; set; }


    }
}