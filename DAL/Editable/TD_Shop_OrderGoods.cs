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
    public partial class TD_Shop_OrderGoods
    {
        public int Add(ShiMiao.Model.TD_Shop_OrderGoods model, string tranID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Shop_OrderGoods(");
            strSql.Append("OrderGoodsID,OrderID,GoodsID,Title,Amount,OrgID,OriPrice,RealPrice,OrderTime");
            strSql.Append(") values (");
            strSql.Append("@OrderGoodsID,@OrderID,@GoodsID,@Title,@Amount,@OrgID,@OriPrice,@RealPrice,@OrderTime");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderGoodsID", model.OrderGoodsID),
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@GoodsID", model.GoodsID),
                        new MySqlParameter("@Title", model.Title),
                        new MySqlParameter("@Amount", model.Amount),
                        new MySqlParameter("@OrgID", model.OrgID),
                        new MySqlParameter("@OriPrice", model.OriPrice),
                        new MySqlParameter("@RealPrice", model.RealPrice),
                        new MySqlParameter("@OrderTime", model.OrderTime)

            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
        }

        public IList<Model.TD_Shop_OrderGoods> GetListByOrderID(string orderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT OrderGoodsID,OrderID,GoodsID,Title,Amount,OrgID,OriPrice,RealPrice,OrderTime FROM TD_Shop_OrderGoods");
            strSql.Append(" where OrderID=@OrderID");
            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", orderID)
            };
            IList<ShiMiao.Model.TD_Shop_OrderGoods> list = new List<ShiMiao.Model.TD_Shop_OrderGoods>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Shop_OrderGoods model = new ShiMiao.Model.TD_Shop_OrderGoods();

                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderGoodsID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.GoodsID = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Title = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.Amount = dr.GetInt32(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.OrgID = dr.GetInt32(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.OriPrice = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.RealPrice = dr.GetDecimal(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.OrderTime = dr.GetDateTime(8);
                    }
                    list.Add(model);
                }
                return list;
            }
        }
    }
}
