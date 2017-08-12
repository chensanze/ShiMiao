using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ShiMiao.DAL
{
    public partial class TD_Donation_Order
    {
        public int PayOrder(Model.TD_Donation_Order model, string tranID)
        {
            string sql = "UPDATE TD_Donation_Order SET PayTime=@PayTime,IsPay=@IsPay WHERE OrderID=@OrderID";
            MySqlParameter[] parameters = {
                new MySqlParameter("@PayTime", model.PayTime),
                new MySqlParameter("@IsPay", model.IsPay),
                new MySqlParameter("@OrderID", model.OrderID)
            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, sql, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Donation_Order> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID, DonationID, OpenID, NickName, HeaderImage, OrderTime, Fee, IsPay, PayType, PayTime  ");
            strSql.Append(" FROM TD_Donation_Order ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.AppendFormat(" order by {0}", orderBy);
            }
            strSql.AppendFormat(" limit {0} ", top);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            IList<ShiMiao.Model.TD_Donation_Order> list = new List<ShiMiao.Model.TD_Donation_Order>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Order model = new ShiMiao.Model.TD_Donation_Order();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.OrderTime = dr.GetDateTime(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Fee = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }

        public IList<decimal> GetCount(string where, IDictionary<string, object> dictParams)
        {
            IList<decimal> list = new List<decimal>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1),sum(Fee) from TD_Donation_Order");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" where " + where);
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
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                if (dr.Read())
                {
                    if (dr[0] != DBNull.Value)
                    {
                        list.Add(dr.GetInt32(0));
                    }
                    else
                    {
                        list.Add(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        list.Add(dr.GetDecimal(1));
                    }
                    else
                    {
                        list.Add(0);
                    }
                }
            }
            parameters.Clear();
            return list;
        }

        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Donation_Order> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT OrderID, DonationID, OpenID, NickName, HeaderImage, OrderTime, Fee, IsPay, PayType, PayTime,Name,Mobile,MemberID,Message,IsAnonymous FROM TD_Donation_Order");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.AppendFormat(" order by {0}", orderby);
            }
            strSql.AppendFormat(" limit {0}, {1}", startIndex, pageSize);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            IList<ShiMiao.Model.TD_Donation_Order> list = new List<ShiMiao.Model.TD_Donation_Order>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Order model = new ShiMiao.Model.TD_Donation_Order();

                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.OrderTime = dr.GetDateTime(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Fee = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Name = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.Mobile = dr.GetString(11);
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(12);
                    }
                    if (dr[13] != DBNull.Value)
                    {
                        model.Message = dr.GetString(13);
                    }
                    if (dr[14] != DBNull.Value)
                    {
                        model.IsAnonymous = dr.GetString(14);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }
    }
}
