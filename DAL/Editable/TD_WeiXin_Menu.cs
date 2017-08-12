using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.DAL
{
    public class TD_WeiXin_Menu
    {
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public IList<Model.TD_WeiXin_Menu> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MenuID, OrgID, OrgName, MenuName, MenuType, MenuValue, ParentID, OrderIndex  ");
            strSql.Append(" FROM TD_WeiXin_Menu ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.AppendFormat(" order by {0}", orderBy);
            }
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            IList<Model.TD_WeiXin_Menu> list = new List<Model.TD_WeiXin_Menu>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    Model.TD_WeiXin_Menu model = new Model.TD_WeiXin_Menu();
                    if (dr[0] != DBNull.Value)
                    {
                        model.MenuID = dr.GetInt32(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OrgName = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.MenuName = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.MenuType = dr.GetInt32(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.MenuValue = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.ParentID = dr.GetInt32(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.OrderIndex = dr.GetInt32(7);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }
    }
}
