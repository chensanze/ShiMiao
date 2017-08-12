using ShiMiao.Common;
using ShiMiao.DBUtility;
using System;
using ShiMiao.Model;
using System.Collections.Generic;

namespace ShiMiao.BLL
{
    public  partial class TD_Order_WeiXinPay
    {
        private static readonly BLL.TD_Shop_OrderGoods orderGoodsBLL = new BLL.TD_Shop_OrderGoods();
        private BLL.TD_Donation_Order donationOrderBLL = new BLL.TD_Donation_Order();
        private BLL.TD_Shop_Goods goodsBLL = new BLL.TD_Shop_Goods();
        private BLL.TD_Shop_Order shopOrderBLL = new BLL.TD_Shop_Order();
        public bool OrderIsPay(string orderID, int status)
        {
            return dal.OrderIsPay(orderID, status);
        }

        public int Sync(Model.TD_Order_WeiXinPay model, string orderID)
        {
            int result = 0;
            string prefix = model.OrderID.Substring(0, 2);
            switch (prefix)
            {
                case Constants.PayCode.Donation:
                    result = SyncDonation(model, orderID);
                    break;
                case Constants.PayCode.Shop:
                    result = SyncShop(model, orderID);
                    break;
            }
            return result;
        }

        private int SyncShop(Model.TD_Order_WeiXinPay model, string orderID)
        {
            Model.TD_Shop_Order order = shopOrderBLL.GetModel(orderID);
            if (order == null)
            {
                return 0;
            }
            if (order.IsPay == "1")
            {
                return 0;
            }
            DateTime now = DateTime.Now;
            order.PayTime = now;
            order.IsPay = "1";

            IList<Model.TD_Shop_OrderGoods> orderGoodsList = orderGoodsBLL.GetListByOrderID(orderID);
            string tranID = MySqlHelperUtil.BeginTran();
            try
            {
                decimal result = dal.Sync(model, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                result = goodsBLL.PayOrder(orderID, orderGoodsList, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                result = shopOrderBLL.PayOrder(order, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                MySqlHelperUtil.CommitTran(tranID);
                return 1;
            }
            catch
            {
                MySqlHelperUtil.RollbackTran(tranID);
                return 0;
            }
        }

        public int SyncDonation(Model.TD_Order_WeiXinPay model, string orderID)
        {
            Model.TD_Donation_Order order = donationOrderBLL.GetModel(orderID);
            if (order == null)
            {
                return 0;
            }
            if (order.IsPay == "1")
            {
                return 0;
            }
            DateTime now = DateTime.Now;
            order.PayTime = now;
            order.IsPay = "1";

            string tranID = MySqlHelperUtil.BeginTran();
            try
            {
                decimal result = dal.Sync(model, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                result = donationOrderBLL.PayOrder(order, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                MySqlHelperUtil.CommitTran(tranID);
                return 1;
            }
            catch
            {
                MySqlHelperUtil.RollbackTran(tranID);
                return 0;
            }
        }
    }
}
