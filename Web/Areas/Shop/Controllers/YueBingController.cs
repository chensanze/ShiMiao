using ShiMiao.Common;
using ShiMiao.WebCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Areas.Shop.Controllers
{
    [ShiMiaoWXValidation]
    public class YueBingController : MiniWebBaseController
    {
        private static readonly BLL.TD_Shop_Order orderBLL = new BLL.TD_Shop_Order();
        private static readonly BLL.TD_Shop_Goods goodsBLL = new BLL.TD_Shop_Goods();
        private static readonly BLL.bl_Shop_Order_Consignee orderConsignee = new BLL.bl_Shop_Order_Consignee();
        
        // GET: Shop/YueBing
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public JsonResult getOrderList()
        {
            return null;
        }
        [HttpPost]
        public JsonResult SaveOrder(Models.m_YueBing model)
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            Model.TD_Shop_Goods goods = goodsBLL.GetModel(model.goodsID);
            if (goods.Balance.Value < model.Amount)
            {
                return GetErrorResult("数量不足");
            }
            ShiMiao.Model.TD_Shop_Order_Consignee consignee = new Model.TD_Shop_Order_Consignee();
            consignee.Address = model.Adress;
            consignee.Name = model.username;
            consignee.Phone = model.Phone;
            consignee.Identification = model.Identification;

            var member = MemberData.GetMember();
            Model.TD_Shop_Order order = new Model.TD_Shop_Order();
            order.PayType = Constants.PayType.WeiXin;
            order.MemberID = member.MemberID;
            order.HeaderImage = member.HeaderImage;
            order.NickName = member.NickName;
            order.OrderType = (int)Constants.DonationType.Shop1;
            order.OrgID = orgID;
            //月饼常规价格 + 快递费
            order.OriPrice = goods.Price * model.Amount + model.ExtraPrice * model.Amount;
            if (model.Amount>=5)
            {//优惠价格 + 快递费
                order.RealPrice = 88 * model.Amount + model.ExtraPrice * model.Amount;
            }
            else
            {
                order.RealPrice = order.OriPrice;
            }
            order.Status = (int)Constants.OrderStatus.WaitPay;

            IList<Model.TD_Shop_OrderGoods> orderGoodsList = new List<Model.TD_Shop_OrderGoods>();
            Model.TD_Shop_OrderGoods orderGoods = new Model.TD_Shop_OrderGoods();
            orderGoods.GoodsID = goods.GoodsID;
            orderGoods.Title = goods.Title;
            orderGoods.Amount = model.Amount;
            orderGoods.OrgID = orgID;
            orderGoods.OriPrice = goods.Price;
            orderGoods.RealPrice = goods.Price;
            orderGoodsList.Add(orderGoods);
            int result = orderBLL.Save(order, orderGoodsList);
            if (result > 0)
            {
                string url = string.Empty;
                if (order.PayType == Constants.PayType.WeiXin)
                {
                    url = "/WeiXinPay/PayForShop?oid=" + ViewBag.EnOrgID + "&orderid=" + order.OrderID + "&url=" + HttpUtility.UrlEncode("/Shop/Goods2/Index?oid=" + ViewBag.EnOrgID);
                }
                return GetSucceedResult(new
                {
                    url = url
                }, "");
            }
            else
            {
                return GetErrorResult("保存失败，请稍候再试");
            }
        }
    }
}