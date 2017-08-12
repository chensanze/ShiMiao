using ShiMiao.Common;
using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Controllers
{
    public class WeiXinPayController : MiniWebBaseController
    {
        private static readonly BLL.TD_Donation_Order donationOrderBLL = new BLL.TD_Donation_Order();
        private static readonly BLL.TD_Shop_Order shopOrderBLL = new BLL.TD_Shop_Order();
        private static readonly BLL.TD_Order_WeiXinPay weiXinPayBLL = new BLL.TD_Order_WeiXinPay();

        [ShiMiaoWXValidation]
        public ActionResult PayForDonation(string orderid, string url)
        {
            var member = MemberData.GetMember();
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(orderid))
            {
                return GotoErrorResult("未找到该订单");
            }
            Model.TD_Donation_Order model = donationOrderBLL.GetModel(orderid);
            if (model == null)
            {
                return GotoErrorResult("未找到该订单");
            }
            if (model.IsPay == "0")
            {
                bool exists = weiXinPayBLL.OrderIsPay(model.OrderID, 1);
                if (exists)
                {
                    return GotoErrorResult("订单已微信支付，请联系平台");
                }
                string ip = Request.UserHostAddress;
                WeiXinPort port = new WeiXinPort();
                try
                {
                    port.InitSignature(HttpContext.Request.Url.AbsoluteUri);
                    string domain = Request.Url.Scheme + "://" + Request.Url.Authority;
                    port.InitPaySignature(model.OrderID.ToString(), model.Fee.Value, member.OpenID, domain, ip);
                    if (!string.IsNullOrEmpty(port.ErrorMessage))
                    {
                        return GotoErrorResult(port.ErrorMessage);
                    }
                    Model.TD_Order_WeiXinPay weiXinPay = new Model.TD_Order_WeiXinPay();
                    weiXinPay.OrderID = model.OrderID.ToString();
                    weiXinPay.Timestamp = port.Timestamp;
                    weiXinPay.NonceStr = port.NonceStr;
                    weiXinPay.Package = port.Package;
                    weiXinPay.Status = 0;
                    weiXinPay.PayTime = now;
                    weiXinPayBLL.Add(weiXinPay);
                    ViewBag.ThirdPortConfig = new ThirdSharedInfo()
                    {
                        AppID = port.AppID,
                        NonceStr = port.NonceStr,
                        Timestamp = port.Timestamp,
                        Package = port.Package,
                        SignType = port.SignType,
                        Signature = port.Signature,
                        PaySignature = port.PaySignature
                    };
                }
                catch
                { }
                ViewBag.BackURL = HttpUtility.UrlEncode(url);
                return View(model);
            }
            else
            {
                return GotoErrorResult("订单状态有误");
            }
        }

        [ShiMiaoWXValidation]
        public ActionResult PayForShop(string orderid, string url)
        {
            var member = MemberData.GetMember();
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(orderid))
            {
                return GotoErrorResult("未找到该订单");
            }
            Model.TD_Shop_Order model = shopOrderBLL.GetModel(orderid);
            if (model == null)
            {
                return GotoErrorResult("未找到该订单");
            }
            if (model.Status == (int)Constants.OrderStatus.Cancel)
            {
                return GotoErrorResult("订单已取消");
            }
            if (model.IsPay == "0")
            {
                bool exists = weiXinPayBLL.OrderIsPay(model.OrderID, 1);
                if (exists)
                {
                    return GotoErrorResult("订单已微信支付，请联系平台");
                }
                string ip = Request.UserHostAddress;
                WeiXinPort port = new WeiXinPort();
                try
                {
                    port.InitSignature(HttpContext.Request.Url.AbsoluteUri);
                    string domain = Request.Url.Scheme + "://" + Request.Url.Authority;
                    port.InitPaySignature(model.OrderID.ToString(), model.RealPrice.Value, member.OpenID, domain, ip);
                    if (!string.IsNullOrEmpty(port.ErrorMessage))
                    {
                        return GotoErrorResult(port.ErrorMessage);
                    }
                    Model.TD_Order_WeiXinPay weiXinPay = new Model.TD_Order_WeiXinPay();
                    weiXinPay.OrderID = model.OrderID.ToString();
                    weiXinPay.Timestamp = port.Timestamp;
                    weiXinPay.NonceStr = port.NonceStr;
                    weiXinPay.Package = port.Package;
                    weiXinPay.Status = 0;
                    weiXinPay.PayTime = now;
                    weiXinPayBLL.Add(weiXinPay);
                    ViewBag.ThirdPortConfig = new ThirdSharedInfo()
                    {
                        AppID = port.AppID,
                        NonceStr = port.NonceStr,
                        Timestamp = port.Timestamp,
                        Package = port.Package,
                        SignType = port.SignType,
                        Signature = port.Signature,
                        PaySignature = port.PaySignature
                    };
                }
                catch
                { }
                ViewBag.BackURL = HttpUtility.UrlEncode(url);
                return View(model);
            }
            else
            {
                return GotoErrorResult("订单状态有误");
            }
        }

        public ActionResult Monitor()
        {
            if (Request.InputStream.Length > 0)
            {
                string xml = string.Empty;
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    xml = reader.ReadToEnd();
                }
                WeiXinPayRequestXML request = XmlOperator.Derialize<WeiXinPayRequestXML>(xml);
                if (request.result_code.ToUpper() != "SUCCESS")
                {
                    return Content("");
                }
                int splitIndex = request.out_trade_no.IndexOf("_");
                string orderID = request.out_trade_no;
                if (splitIndex > 0)
                {
                    orderID = request.out_trade_no.Substring(0, splitIndex);
                }
                string where = string.Format("OrderID='{0}'", orderID);
                IList<Model.TD_Order_WeiXinPay> payList = weiXinPayBLL.GetList(where, "PayTime desc", null);
                int count = payList.Count((model) => { return model.Status == 1; });
                if (count > 0)
                {
                    return PayOK();
                }
                var config = WeiXinConfig.GetConfig();
                count = payList.Count((model) => {
                    if (model.OrderID == orderID
                    && model.NonceStr == request.nonce_str
                    && config.AppID == request.appid
                    && config.ShopID == request.mch_id)
                    {
                        return true;
                    }
                    return false;
                });
                if (count > 0)
                {
                    Model.TD_Order_WeiXinPay pay = new Model.TD_Order_WeiXinPay();
                    pay.OrderID = orderID;
                    pay.NonceStr = request.nonce_str;
                    pay.WeiXinOrderID = request.transaction_id;
                    pay.OrderFee = request.total_fee;
                    pay.CashFee = request.cash_fee;
                    pay.Status = 1;
                    pay.CallBackTime = DateTime.ParseExact(request.time_end, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    int result = weiXinPayBLL.Sync(pay, orderID);
                    if (result > 0)
                    {
                        return PayOK();
                    }
                    else
                    {
                        return PayFail();
                    }
                }
                else
                {
                    return PayFail();
                }
            }
            return Content("");
        }

        public ActionResult PayOK()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("return_code", "SUCCESS");
            dict.Add("return_msg", "OK");
            string xml = DictionaryToXML(dict);
            return Content(xml);
        }

        public ActionResult PayFail()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("return_code", "FAIL");
            dict.Add("return_msg", "操作失败");
            string xml = DictionaryToXML(dict);
            return Content(xml);
        }

        public string DictionaryToXML(IDictionary<string, string> dict)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<xml>");
            foreach (var pair in dict)
            {
                xml.Append("<");
                xml.Append(pair.Key);
                xml.Append(">");
                xml.Append("<![CDATA[");
                xml.Append(pair.Value);
                xml.Append("]]>");
                xml.Append("</");
                xml.Append(pair.Key);
                xml.Append(">");
            }
            xml.Append("</xml>");
            return xml.ToString();
        }

        [ShiMiaoWXValidation]
        public ActionResult OK(string url)
        {
            ViewBag.BackURL = url;
            return View();
        }
    }
}