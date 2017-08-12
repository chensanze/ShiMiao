using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using ShiMiao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.DAL
{
    public class dl_Shop_Order_Consignee
    {

        public TD_Shop_Order_Consignee getConsignee(string OrderID)
        {//该订单获取收件人信息
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID, Name, Phone,Address,Identification");
            strSql.Append("  from TD_Shop_Order_Consignee ");
            strSql.Append(" where OrderID=@OrderID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@OrderID", OrderID)

                        };

            ShiMiao.Model.TD_Shop_Order_Consignee model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    model = new ShiMiao.Model.TD_Shop_Order_Consignee();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.Name = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.Phone = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Address = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.Identification = dr.GetString(4);
                    }
                }
                return model;
            }

        }
        public bool Exists(string OrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 1 from TD_Shop_Order_Consignee");
            strSql.Append(" where ");
            strSql.Append(" OrderID = @OrderID ");
            MySqlParameter[] parameters = {
                new MySqlParameter("@OrderID", OrderID)
            };
            object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters);
            return result != null;
        }
        public int Add(ShiMiao.Model.TD_Shop_Order_Consignee model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Donation_Order(");
            strSql.Append("OrderID,Name,Phone,Address,Identification");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@Name,@Phone,@Address,@Identification");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Phone", model.Phone),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Address", model.Address),
                        new MySqlParameter("@Identification", model.Identification)

            };
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ShiMiao.Model.TD_Shop_Order_Consignee model, string tranID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Donation_Order(");
            strSql.Append("OrderID,Name,Phone,Address,Identification");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@Name,@Phone,@Address,@Identification");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Phone", model.Phone),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Address", model.Address),
                        new MySqlParameter("@Identification", model.Identification)

            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
        }
    }
}
