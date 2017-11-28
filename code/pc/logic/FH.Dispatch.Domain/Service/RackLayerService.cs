using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Domain.Model;
using ViData;

namespace FH.Dispatch.Domain.Service
{
    public class RackLayerService : Repository<RackLayerInfo, RackLayer>
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public IList<RackLayerInfo> GetByPage(PagingInfo pi)
        {
            pi.TableName = " t_d_rack_layer rl inner join t_d_rack r on rl.rack_id = r.id";
            pi.Fileds = " rl.*, r.rackcode";
            pi.SortFields = " rl.racklayercode desc ";
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
            string sql = "update t_d_rack_layer set " + column + " = @newvalue where id = " + id;
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
            string sql = "update t_d_rack_layer set isvalid = " + isvalid + " where id in (" + s + ")";
            return DataHelper.ExcuteNonQuery(sql);
        }
    }
}
