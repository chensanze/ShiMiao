using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;
using ShiMiao.DBUtility;
using ShiMiao.Common;

namespace ShiMiao.BLL
{
    public partial class TD_Shop_Order
    {
        private static readonly DAL.TD_Shop_Order dal = new DAL.TD_Shop_Order();
        private static readonly DAL.TD_Shop_Goods goodsBLL = new DAL.TD_Shop_Goods();
        private static readonly DAL.TD_Shop_OrderGoods orderGoodsBLL = new DAL.TD_Shop_OrderGoods();

        public int Save(Model.TD_Shop_Order order, IList<Model.TD_Shop_OrderGoods> ordderGoodsList)
        {
            string tranID = MySqlHelperUtil.BeginTran();
            try
            {
                string orderID = GetOrderID();
                order.OrderID = orderID;
                order.PayNo = orderID;
                order.IsMemberDeleted = "0";
                order.IsPay = "0";
                order.OrderTime = DateTime.Now;
                int result = dal.Add(order, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                foreach (var orderGoods in ordderGoodsList)
                {
                    orderGoods.OrderID = orderID;
                    orderGoods.OrderTime = order.OrderTime;
                    orderGoods.OrderGoodsID = Guid.NewGuid().ToString();
                    result = orderGoodsBLL.Add(orderGoods, tranID);
                    if (result == 0)
                    {
                        MySqlHelperUtil.RollbackTran(tranID);
                        return 0;
                    }
                    result = goodsBLL.Frozen(orderGoods.GoodsID, orderGoods.Amount.Value, tranID);
                    if (result == 0)
                    {
                        MySqlHelperUtil.RollbackTran(tranID);
                        return 0;
                    }
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
        public int Save(Model.TD_Shop_Order order,Model.TD_Shop_Order_Consignee consignee, IList<Model.TD_Shop_OrderGoods> ordderGoodsList)
        {
            string tranID = MySqlHelperUtil.BeginTran();
            try
            {
                string orderID = GetOrderID();
                order.OrderID = orderID;
                order.PayNo = orderID;
                order.IsMemberDeleted = "0";
                order.IsPay = "0";
                order.OrderTime = DateTime.Now;
                int result = dal.Add(order, tranID);
                if (result == 0)
                {
                    MySqlHelperUtil.RollbackTran(tranID);
                    return 0;
                }
                foreach (var orderGoods in ordderGoodsList)
                {
                    orderGoods.OrderID = orderID;
                    orderGoods.OrderTime = order.OrderTime;
                    orderGoods.OrderGoodsID = Guid.NewGuid().ToString();
                    result = orderGoodsBLL.Add(orderGoods, tranID);
                    if (result == 0)
                    {
                        MySqlHelperUtil.RollbackTran(tranID);
                        return 0;
                    }
                    result = goodsBLL.Frozen(orderGoods.GoodsID, orderGoods.Amount.Value, tranID);
                    if (result == 0)
                    {
                        MySqlHelperUtil.RollbackTran(tranID);
                        return 0;
                    }
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

        public int PayOrder(Model.TD_Shop_Order model, string tranID)
        {
            return dal.PayOrder(model, tranID);
        }

        private string GetOrderID()
        {
            DateTime now = DateTime.Now;
            return Constants.PayCode.Shop + now.ToString("yyMMddHHmmssffffff");
        }

        public Model.TD_Shop_Order GetModel(string orderid)
        {
            return dal.GetModel(orderid);
        }

        public IList<decimal> GetCount(string where)
        {
            return dal.GetCount(where);
        }

        public IList<Model.OrderDetail> GetListByPage(string where, string orderField, int startIndex, int pageSize)
        {
            return dal.GetListByPage(where, orderField, startIndex, pageSize);
        }
    }
}
