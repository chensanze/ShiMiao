using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.DAL
{
    public class dl_Order
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Order_WeiXinPay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Order_WeiXinPay(");
            strSql.Append("OrderID,NonceStr,Timestamp,OrgID,WeiXinOrderID,OrderFee,CashFee,Package,Status,PayTime,CallBackTime");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@NonceStr,@Timestamp,@OrgID,@WeiXinOrderID,@OrderFee,@CashFee,@Package,@Status,@PayTime,@CallBackTime");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@NonceStr", model.NonceStr),
                        new MySqlParameter("@Timestamp", model.Timestamp),
                        new MySqlParameter("@OrgID", model.OrgID),
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID),
                        new MySqlParameter("@OrderFee", model.OrderFee),
                        new MySqlParameter("@CashFee", model.CashFee),
                        new MySqlParameter("@Package", model.Package),
                        new MySqlParameter("@Status", model.Status),
                        new MySqlParameter("@PayTime", model.PayTime),
                        new MySqlParameter("@CallBackTime", model.CallBackTime)

            };
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ShiMiao.Model.TD_Order_WeiXinPay model, string tranID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Order_WeiXinPay(");
            strSql.Append("OrderID,NonceStr,Timestamp,OrgID,WeiXinOrderID,OrderFee,CashFee,Package,Status,PayTime,CallBackTime");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@NonceStr,@Timestamp,@OrgID,@WeiXinOrderID,@OrderFee,@CashFee,@Package,@Status,@PayTime,@CallBackTime");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@NonceStr", model.NonceStr),
                        new MySqlParameter("@Timestamp", model.Timestamp),
                        new MySqlParameter("@OrgID", model.OrgID),
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID),
                        new MySqlParameter("@OrderFee", model.OrderFee),
                        new MySqlParameter("@CashFee", model.CashFee),
                        new MySqlParameter("@Package", model.Package),
                        new MySqlParameter("@Status", model.Status),
                        new MySqlParameter("@PayTime", model.PayTime),
                        new MySqlParameter("@CallBackTime", model.CallBackTime)

            };
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
        }
    }
}
