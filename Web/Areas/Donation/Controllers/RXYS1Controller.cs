using ShiMiao.Common;
using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Areas.Donation.Controllers
{
    public class RXYS1Controller : MiniWebBaseController
    {
        private static readonly BLL.TD_Donation_Order orderBLL = new BLL.TD_Donation_Order();

        [ShiMiaoWXValidation]
        public ActionResult Index()
        {
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}日行一善", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [ShiMiaoWXValidation]
        public ActionResult List()
        {
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}日行一善", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [ShiMiaoWXValidation]
        public ActionResult Detail(string m)
        {
            decimal money = 0;
            if (m == "r")
            {
                Random random = new Random();
                int r1 = random.Next(1, 100);
                if (r1 % 10 == 7
                    || r1 % 10 == 8
                    || r1 % 10 == 9)
                {
                    money = r1 + (decimal)0.88;
                }
                else
                {
                    int r2 = random.Next(1, 100);
                   if (r2 > 50)
                    {
                        money = r1 + (decimal)0.68;
                    }
                   else
                    {
                        money = r1 + (decimal)0.66;
                    }
                }
            }
            else
            {
                money = decimal.Parse(m);
            }
            ViewBag.Money = money;
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}日行一善", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ShiMiaoWXValidation]
        public JsonResult SaveOrder(Model.TD_Donation_Order order)
        {
            order.PayType = Constants.PayType.WeiXin;
            var member = MemberData.GetMember();
            order.MemberID = member.MemberID;
            order.OpenID = member.OpenID;
            order.HeaderImage = member.HeaderImage;
            order.NickName = member.NickName;
            order.DonationType = (int)Constants.DonationType.RXYS1;
            order.DonationID = Constants.DonationID.RXYS1;
            order.OrgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            int result = orderBLL.Save(order);
            if (result > 0)
            {
                string url = string.Empty;
                if (order.PayType == Constants.PayType.WeiXin)
                {
                    url = "/WeiXinPay/PayForDonation?oid=" + ViewBag.EnOrgID + "&orderid=" + order.OrderID + "&url=" + HttpUtility.UrlEncode("/Donation/RXYS1/Index?oid=" + ViewBag.EnOrgID);
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