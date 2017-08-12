using ShiMiao.Common;
using ShiMiao.WebCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShiMiao.Model;
using Stone.WeiXin;

namespace ShiMiao.Web.Areas.Donation.Controllers
{
    public class GDX1Controller : MiniWebBaseController
    {
        private static readonly BLL.TD_WeiXin_Member memberBLL = new BLL.TD_WeiXin_Member();
        private static readonly BLL.TD_Donation_Order orderBLL = new BLL.TD_Donation_Order();

        [ShiMiaoWXValidation]
        public ActionResult Index()
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            var member = MemberData.GetMember();
            if (member != null)
            {
                ViewBag.NickName = member.NickName;
                ViewBag.HeaderImage = member.HeaderImage;
            }
            IDictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("OrgID", orgID);
            dict.Add("DonationID", Constants.DonationID.GDX1);
            StringBuilder where = new StringBuilder();
            where.AppendFormat("OrgID=@OrgID and DonationID=@DonationID and IsPay='1'", orgID, (int)Constants.DonationType.Shop1);
            IList<decimal> counts = orderBLL.GetCount(where.ToString(), dict);
            ViewBag.RecordCount = counts[0];
            ViewBag.TotalMoney = counts[1];

            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}在线功德箱", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [ShiMiaoWXValidation]
        [HttpPost]
        public JsonResult GetOrderList()
        {
            int orgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            StringBuilder where = new StringBuilder();
            where.Append("OrgID=@OrgID and DonationID=@DonationID and IsPay='1'");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("OrgID", orgID);
            dict.Add("DonationID", Constants.DonationID.GDX1);
            string orderField = "PayTime desc";
            int recordCount = orderBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount, 10);
            object list = null;
            if (recordCount > 0)
            {
                int startIndex = PageSize * (PageIndex - 1);
                IList<Model.TD_Donation_Order> orderList = orderBLL.GetListByPage(where.ToString(), orderField, dict, startIndex, PageSize);
                list = BuildOrderList(orderList);
            }
            else
            {
                list = new List<Model.TD_Donation_Order>();
            }
            return GetSucceedListResult(list, recordCount);
        }

        private object BuildOrderList(IList<Model.TD_Donation_Order> list)
        {
            IList<object> newList = new List<object>();
            IList<string> memberIDs = new List<string>();
            foreach (var model in list)
            {
                if (!memberIDs.Contains(model.MemberID))
                {
                    memberIDs.Add(model.MemberID);
                }
            }
            IList<Model.TD_WeiXin_Member> members = memberBLL.GetListByMemberIDs(memberIDs);
            foreach (var model in list)
            {
                var member = GetCurrentMember(model, members);
                newList.Add(new
                {
                    HeaderImage = model.HeaderImage,
                    NickName = DisableName(model.NickName),
                    Region = GetRegion(member),
                    Fee = model.Fee,
                    PayTime = model.PayTime.HasValue ? model.PayTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty,
                    Message = model.Message,
                    IsAnonymous = model.IsAnonymous
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

        private string GetRegion(Model.TD_WeiXin_Member member)
        {
            if (member == null)
            {
                return string.Empty;
            }
            string region = member.Province + " " + member.City;
            return region;
        }

        private Model.TD_WeiXin_Member GetCurrentMember(Model.TD_Donation_Order model, IList<Model.TD_WeiXin_Member> members)
        {
            foreach (var member in members)
            {
                if (member.MemberID == model.MemberID)
                {
                    return member;
                }
            }
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ShiMiaoWXValidation]
        public JsonResult SaveOrder(Model.TD_Donation_Order order)
        {
            order.OrgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            order.PayType = Constants.PayType.WeiXin;
            var member = MemberData.GetMember();
            order.MemberID = member.MemberID;
            order.OpenID = member.OpenID;
            order.HeaderImage = member.HeaderImage;
            order.NickName = member.NickName;
            order.DonationType = (int)Constants.DonationType.GDX1;
            order.DonationID = Constants.DonationID.GDX1;
            if (string.IsNullOrEmpty(order.IsAnonymous))
            {
                order.IsAnonymous = "0";
            }
            int result = orderBLL.Save(order);
            if (result > 0)
            {
                string url = string.Empty;
                if (order.PayType == Constants.PayType.WeiXin)
                {
                    url = "/WeiXinPay/PayForDonation?oid=" + ViewBag.EnOrgID + "&orderid=" + order.OrderID + "&url=" + HttpUtility.UrlEncode("/Donation/GDX1/Index?oid=" + ViewBag.EnOrgID);
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