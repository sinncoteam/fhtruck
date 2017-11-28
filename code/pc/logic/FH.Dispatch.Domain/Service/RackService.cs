/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。  
//
// 文件名：RackService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2016/12/8 10:17:40 
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
    /// RackService  领域层服务实现
    /// </summary>
    public class RackService : Repository<RackInfo, Rack>
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public IList<RackInfo> GetByPage(PagingInfo pi)
        {
            pi.TableName = " t_d_rack r";
            pi.Fileds = " *";
            pi.SortFields = " r.rackcode desc ";
            return this.GetPaging(pi);
        }

        /// <summary>
        /// 更新某一字段
        /// </summary>
        /// <param name="column"></param>
        /// <param name="newValue"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateRow(string column, object newValue, int id)
        {
            string sql = "update t_d_rack set " + column + " = @newvalue where id = " + id;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("newvalue", newValue);
            int i = DataHelper.ExcuteNonQuery2(sql, dict);
            return i;
        }

        /// <summary>
        /// 设为有效
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="isvalid"></param>
        /// <returns></returns>
        public int UpdateToValid(IList<int> idList, int isvalid)
        {
            string s = string.Join(",", idList);
            string sql = "update t_d_rack set isvalid = " + isvalid + " where id in (" + s + ")";
            return DataHelper.ExcuteNonQuery(sql);
        }

        /// <summary>
        /// 查询仓库下的仓位
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public IList<RackInfo> GetListByHouse(int houseId)
        {
            string sql = "select rk.* from t_d_rack rk where rk.isvalid = 1 and  rk.house_id = " + houseId;
            return DataHelper.Fill<RackInfo>(sql);
        }
    }
}
