using ShiMiao.Common;
using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Areas.Shop.Controllers
{
    public class Goods1Controller : MiniWebBaseController
    {
        private static readonly BLL.TD_Shop_Order orderBLL = new BLL.TD_Shop_Order();
        private static readonly BLL.TD_Shop_Goods goodsBLL = new BLL.TD_Shop_Goods();

        [ShiMiaoWXValidation]
        public ActionResult Index()
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            StringBuilder where = new StringBuilder();
            where.AppendFormat("orders.OrgID={0} and orders.OrderType={1} and orders.IsPay='1'", orgID, (int)Constants.DonationType.Shop1);
            IList<decimal> counts = orderBLL.GetCount(where.ToString());
            ViewBag.RecordCount = counts[0];
            ViewBag.TotalMoney = counts[1];
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}广种福田", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [ShiMiaoWXValidation]
        public JsonResult GetList()
        {
            StringBuilder where = new StringBuilder();
            where.Append("statecode=0");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            string orderField = "OrderIndex";
            int recordCount = goodsBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount);
            object list = null;
            if (recordCount > 0)
            {
                int startIndex = PageSize * (PageIndex - 1);
                IList<Model.TD_Shop_Goods> donationList = goodsBLL.GetListByPage(where.ToString(), orderField, dict, startIndex, PageSize);
                list = BuildList(donationList);
            }
            else
            {
                list = new List<Model.TD_Shop_Goods>();
            }
            return GetSucceedListResult(list, recordCount);
        }

        private object BuildList(IList<Model.TD_Shop_Goods> list)
        {
            IList<object> newList = new List<object>();
            foreach (var model in list)
            {
                newList.Add(new
                {
                    GoodsID = model.GoodsID,
                    Price = model.Price,
                    Title = model.Title,
                    Image = model.Image,
                    Amount = model.Amount,
                    Balance = model.Balance,
                    Frozen = model.Frozen
                });
            }
            return newList;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ShiMiaoWXValidation]
        public JsonResult SaveOrder(string goodsID, int? amount)
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            Model.TD_Shop_Goods goods = goodsBLL.GetModel(goodsID);
            if (goods.Balance.Value < amount.Value)
            {
                return GetErrorResult("数量不足");
            }
            var member = MemberData.GetMember();
            Model.TD_Shop_Order order = new Model.TD_Shop_Order();
            order.PayType = Constants.PayType.WeiXin;
            order.MemberID = member.MemberID;
            order.HeaderImage = member.HeaderImage;
            order.NickName = member.NickName;
            order.OrderType = (int)Constants.DonationType.Shop1;
            order.OrgID = orgID;
            order.OriPrice = goods.Price * (amount ?? 1);
            order.RealPrice = order.OriPrice;
            order.Status = (int)Constants.OrderStatus.WaitPay;

            IList<Model.TD_Shop_OrderGoods> orderGoodsList = new List<Model.TD_Shop_OrderGoods>();
            Model.TD_Shop_OrderGoods orderGoods = new Model.TD_Shop_OrderGoods();
            orderGoods.GoodsID = goods.GoodsID;
            orderGoods.Title = goods.Title;
            orderGoods.Amount = amount;
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
                    url = "/WeiXinPay/PayForShop?oid=" + ViewBag.EnOrgID + "&orderid=" + order.OrderID + "&url=" + HttpUtility.UrlEncode("/Shop/Goods1/Index?oid=" + ViewBag.EnOrgID);
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

        [ShiMiaoWXValidation]
        [HttpPost]
        public JsonResult GetOrderList()
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            StringBuilder where = new StringBuilder();
            where.AppendFormat("orders.OrgID={0} and orders.OrderType={1} and orders.IsPay='1'", orgID, (int)Constants.DonationType.Shop1);
            string orderField = "orders.PayTime desc";
            IList<Model.OrderDetail> orderList = orderBLL.GetListByPage(where.ToString(), orderField, 0, 30);
            object list = BuildOrderList(orderList);
            return GetSucceedListResult(list, 30);
        }

        private object BuildOrderList(IList<Model.OrderDetail> list)
        {
            IList<object> newList = new List<object>();
            foreach (var model in list)
            {
                newList.Add(new
                {
                    OrderTime = model.OrderTime.Value.ToString("yyyy-MM-dd"),
                    NickName = DisableName(model.NickName),
                    Title = string.Format("{0}{1}份", model.Title, model.Amount),
                    RealPrice = model.RealPrice
                });
            }
            return newList;
        }

        private string DisableName(string name)
        {
            if (name.Length >= 1)
            {
                return name.Substring(0, 1) + "**";
            }
            return name;
        }
    }
}