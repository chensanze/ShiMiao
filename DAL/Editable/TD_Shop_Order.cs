using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;
using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System.Data.Common;

namespace ShiMiao.DAL
{
    public partial class TD_Shop_Order
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Shop_Order model, string tranID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Shop_Order(");
            strSql.Append("OrderID,PayNo,OrgID,OriPrice,RealPrice,Status,MemberID,NickName,HeaderImage,Mobile,OrderType,PayType,IsPay,IsMemberDeleted,OrderTime,PayTime,CompleteTime,Message,ExtraPrice");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@PayNo,@OrgID,@OriPrice,@RealPrice,@Status,@MemberID,@NickName,@HeaderImage,@Mobile,@OrderType,@PayType,@IsPay,@IsMemberDeleted,@OrderTime,@PayTime,@CompleteTime,@Message,@ExtraPrice");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@PayNo", model.PayNo),
                        new MySqlParameter("@OrgID", model.OrgID),
                        new MySqlParameter("@OriPrice", model.OriPrice),
                        new MySqlParameter("@RealPrice", model.RealPrice),
                        new MySqlParameter("@Status", model.Status),
                        new MySqlParameter("@MemberID", model.MemberID),
                        new MySqlParameter("@NickName", model.NickName),
                        new MySqlParameter("@HeaderImage", model.HeaderImage),
                        new MySqlParameter("@Mobile", model.Mobile),
                        new MySqlParameter("@OrderType", model.OrderType),
                        new MySqlParameter("@PayType", model.PayType),
                        new MySqlParameter("@IsPay", model.IsPay),
                        new MySqlParameter("@IsMemberDeleted", model.IsMemberDeleted),
                        new MySqlParameter("@OrderTime", model.OrderTime),
                        new MySqlParameter("@PayTime", model.PayTime),
                        new MySqlParameter("@CompleteTime", model.CompleteTime),
                        new MySqlParameter("@Message", model.Message),
                        new MySqlParameter("@ExtraPrice",model.ExtraPrice)

            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
        }

        public IList<Model.TD_Shop_Order> GetUnPayOrder()
        {
            string sql = "SELECT OrderID FROM TD_Shop_Order WHERE IsPay='0' and OrderTime<@Time";
            MySqlParameter[] parameters = {
                new MySqlParameter("@Time", DateTime.Now.AddHours(-1))
            };
            IList<ShiMiao.Model.TD_Shop_Order> list = new List<ShiMiao.Model.TD_Shop_Order>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(sql, parameters))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Shop_Order model = new ShiMiao.Model.TD_Shop_Order();

                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    list.Add(model);
                }
                return list;
            }
        }

        public IList<decimal> GetCount(string where)
        {
            IList<decimal> list = new List<decimal>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(1),SUM(orders.realprice) FROM TD_Shop_Order as orders inner join TD_Shop_OrderGoods as goods on orders.OrderID=goods.OrderID");
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0}", where);
            }
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(sql.ToString()))
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
            return list;
        }

        public IList<Model.OrderDetail> GetListByPage(string where, string orderField, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT orders.NickName,orders.OrderTime,orders.RealPrice,orderGoods.Amount,orderGoods.Title");
            strSql.Append(" FROM TD_Shop_Order as orders");
            strSql.Append(" inner join TD_Shop_OrderGoods as orderGoods on orders.OrderID=orderGoods.OrderID");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            if (!string.IsNullOrEmpty(orderField))
            {
                strSql.AppendFormat(" order by {0}", orderField);
            }
            strSql.AppendFormat(" limit {0}, {1}", startIndex, pageSize);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            IList<ShiMiao.Model.OrderDetail> list = new List<ShiMiao.Model.OrderDetail>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.OrderDetail model = new ShiMiao.Model.OrderDetail();

                    if (dr[0] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.OrderTime = dr.GetDateTime(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.RealPrice = dr.GetDecimal(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Amount = dr.GetInt32(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.Title = dr.GetString(4);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }

        public int PayOrder(Model.TD_Shop_Order model, string tranID)
        {
            string sql = "UPDATE TD_Shop_Order SET PayTime=@PayTime,IsPay=@IsPay WHERE OrderID=@OrderID";
            MySqlParameter[] parameters = {
                new MySqlParameter("@PayTime", model.PayTime),
                new MySqlParameter("@IsPay", model.IsPay),
                new MySqlParameter("@OrderID", model.OrderID)
            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, sql, parameters);
        }

        public Model.TD_Shop_Order GetModel(string OrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID,PayNo,OrgID,OriPrice,RealPrice,Status,MemberID,NickName,HeaderImage,Mobile,OrderType,PayType,IsPay,IsMemberDeleted,OrderTime,PayTime,CompleteTime ,Message,ExtraPrice");
            strSql.Append("  from TD_Shop_Order ");
            strSql.Append(" where OrderID=@OrderID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@OrderID", OrderID)

                        };

            ShiMiao.Model.TD_Shop_Order model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    model = new ShiMiao.Model.TD_Shop_Order();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.PayNo = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OriPrice = dr.GetDecimal(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.RealPrice = dr.GetDecimal(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.Status = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.Mobile = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.OrderType = dr.GetInt32(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(11);
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(12);
                    }
                    if (dr[13] != DBNull.Value)
                    {
                        model.IsMemberDeleted = dr.GetString(13);
                    }
                    if (dr[14] != DBNull.Value)
                    {
                        model.OrderTime = dr.GetDateTime(14);
                    }
                    if (dr[15] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(15);
                    }
                    if (dr[16] != DBNull.Value)
                    {
                        model.CompleteTime = dr.GetDateTime(16);
                    }
                    if (dr[17] != DBNull.Value)
                    {
                        model.Message = dr.GetString(17);
                    }
                    if (dr[18] != DBNull.Value)
                    {
                        model.ExtraPrice = dr.GetDecimal(18);
                    }
                }
                return model;
            }
        }
    }
}
