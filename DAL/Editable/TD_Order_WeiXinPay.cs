using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ShiMiao.DAL
{
    public partial class TD_Order_WeiXinPay
    {
        public bool OrderIsPay(string orderID, int status)
        {
            string sql = "SELECT 1 FROM TD_Order_WeiXinPay WHERE OrderID=@OrderID and Status=@Status";
            MySqlParameter[] parameters = {
                new MySqlParameter("@OrderID", orderID),
                new MySqlParameter("@Status", status)
            };
            object result = MySqlHelperUtil.ExecuteScalar(sql, parameters);
            return result != null;
        }

        public int Sync(Model.TD_Order_WeiXinPay model, string tranID)
        {
            string sql = "UPDATE TD_Order_WeiXinPay SET Status=@Status,OrderFee=@OrderFee,CashFee=@CashFee,CallBackTime=@CallBackTime WHERE OrderID=@OrderID and NonceStr=@NonceStr and Status=0";
            MySqlParameter[] parameters = {
                new MySqlParameter("@Status", model.Status),
                new MySqlParameter("@OrderFee", model.OrderFee),
                new MySqlParameter("@CashFee", model.CashFee),
                new MySqlParameter("@CallBackTime", model.CallBackTime),
                new MySqlParameter("@OrderID", model.OrderID),
                new MySqlParameter("@NonceStr", model.NonceStr)
            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, sql, parameters);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID, NonceStr, Timestamp, OrgID, WeiXinOrderID, OrderFee, CashFee, Package, Status, PayTime, CallBackTime  ");
            strSql.Append(" FROM TD_Order_WeiXinPay");
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
            IList<ShiMiao.Model.TD_Order_WeiXinPay> list = new List<ShiMiao.Model.TD_Order_WeiXinPay>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Order_WeiXinPay model = new ShiMiao.Model.TD_Order_WeiXinPay();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.NonceStr = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.Timestamp = dr.GetInt32(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.WeiXinOrderID = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.OrderFee = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.CashFee = dr.GetInt32(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.Package = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.Status = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.CallBackTime = dr.GetDateTime(10);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT OrderID, NonceStr, Timestamp, OrgID, WeiXinOrderID, OrderFee, CashFee, Package, Status, PayTime, CallBackTime  FROM TD_Order_WeiXinPay");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
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
            IList<ShiMiao.Model.TD_Order_WeiXinPay> list = new List<ShiMiao.Model.TD_Order_WeiXinPay>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Order_WeiXinPay model = new ShiMiao.Model.TD_Order_WeiXinPay();

                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.NonceStr = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.Timestamp = dr.GetInt32(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.WeiXinOrderID = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.OrderFee = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.CashFee = dr.GetInt32(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.Package = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.Status = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.CallBackTime = dr.GetDateTime(10);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }
    }
}
