using System;
using ShiMiao.Model;
using ShiMiao.DBUtility;
using ShiMiao.Common;

namespace ShiMiao.BLL
{
    public partial class TD_Donation_Order
    {
        public int PayOrder(Model.TD_Donation_Order model, string tranID)
        {
            return dal.PayOrder(model, tranID);
        }

        public int Save(Model.TD_Donation_Order order)
        {
            string tranID = MySqlHelperUtil.BeginTran();
            try
            {
                string orderID = GetOrderID();
                order.OrderID = orderID;
                order.PayNo = orderID;
                order.IsPay = "0";
                order.OrderTime = DateTime.Now;
                decimal result = dal.Add(order, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                MySqlHelperUtil.CommitTran(tranID);
                return 1;
            }
            catch (Exception ex)
            {
                MySqlHelperUtil.RollbackTran(tranID);
                return 0;
            }
        }

        private string GetOrderID()
        {
            DateTime now = DateTime.Now;
            return Constants.PayCode.Donation + now.ToString("yyMMddHHmmssffffff");
        }
    }
}
