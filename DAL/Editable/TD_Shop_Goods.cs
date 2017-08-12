using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;

namespace ShiMiao.DAL
{
    public partial class TD_Shop_Goods
    {
        public int GetRecordCount(string strWhere, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TD_Shop_Goods");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
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
            object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters.ToArray());
            parameters.Clear();
            return int.Parse(result.ToString());
        }

        public int ClearFrozenGoods(DateTime time, int orderStatus)
        {
            string sql = @"UPDATE td_shop_order as orders
                                    inner join td_shop_ordergoods as ordergoods on orders.OrderID=ordergoods.OrderID
                                    inner join td_shop_goods as goods on ordergoods.GoodsID=goods.goodsid
                                    set orders.Status=@OrderStatus, goods.balance=goods.balance+ordergoods.Amount,goods.frozen=goods.frozen-ordergoods.Amount
                                    where orders.IsPay='0' and orders.OrderTime<@time and goods.frozen>ordergoods.Amount";
            MySqlParameter[] parameters = {
                new MySqlParameter("@Time", time),
                new MySqlParameter("@OrderStatus", orderStatus)
            };
            return MySqlHelperUtil.ExecuteNonQuery(sql, parameters);
        }

        public IList<ShiMiao.Model.TD_Shop_Goods> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT GoodsID, OrgID, Title, Image, Content, Amount, Balance, Price, CreateTime, OrderIndex, StateCode,Frozen,Unit FROM TD_Shop_Goods");
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
            IList<ShiMiao.Model.TD_Shop_Goods> list = new List<ShiMiao.Model.TD_Shop_Goods>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Shop_Goods model = new ShiMiao.Model.TD_Shop_Goods();

                    if (dr[0] != DBNull.Value)
                    {
                        model.GoodsID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.Title = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Image = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.Content = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.Amount = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Balance = dr.GetInt32(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.Price = dr.GetDecimal(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.OrderIndex = dr.GetInt32(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.StateCode = dr.GetInt32(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.Frozen = dr.GetInt32(11);
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        model.Unit = dr.GetString(12);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }

        public int PayOrder(string goodsID, int amount, string tranID)
        {
            string sql = "UPDATE TD_Shop_Goods SET Frozen=Frozen-@Amount WHERE GoodsID=@GoodsID";
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@GoodsID", goodsID),
                                    new MySqlParameter("@Amount", amount)
                        };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, sql, parameters);
        }

        public int Frozen(string goodsID, int amount, string tranID)
        {
            string sql = "UPDATE TD_Shop_Goods SET Balance=Balance-@Amount,Frozen=Frozen+@Amount WHERE GoodsID=@GoodsID and Balance>@Amount";
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@GoodsID", goodsID),
                                    new MySqlParameter("@Amount", amount)
                        };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, sql, parameters);
        }

        public ShiMiao.Model.TD_Shop_Goods GetModel(string goodsID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodsID, OrgID, Title, Image, Content, Amount, Balance, Price, CreateTime, OrderIndex, StateCode,Frozen  ");
            strSql.Append("  from TD_Shop_Goods ");
            strSql.Append(" where GoodsID=@GoodsID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@GoodsID", goodsID)

                        };

            ShiMiao.Model.TD_Shop_Goods model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    model = new ShiMiao.Model.TD_Shop_Goods();
                    if (dr[0] != DBNull.Value)
                    {
                        model.GoodsID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.Title = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Image = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.Content = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.Amount = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Balance = dr.GetInt32(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.Price = dr.GetDecimal(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.OrderIndex = dr.GetInt32(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.StateCode = dr.GetInt32(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.Frozen = dr.GetInt32(11);
                    }
                }
                return model;
            }
        }
    }
}
