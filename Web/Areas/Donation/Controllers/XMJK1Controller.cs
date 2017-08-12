using ShiMiao.Common;
using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Areas.Donation.Controllers
{
    public class XMJK1Controller : MiniWebBaseController
    {
        private static readonly BLL.TD_Donation_Info donationBLL = new BLL.TD_Donation_Info();
        private static readonly BLL.TD_Donation_Order orderBLL = new BLL.TD_Donation_Order();
        private static readonly BLL.TD_Donation_LeaveMsg leaveMsgBLL = new BLL.TD_Donation_LeaveMsg();

        [ShiMiaoWXValidation]
        public ActionResult Index()
        {
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}项目捐款", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View();
        }

        [HttpPost]
        [ShiMiaoWXValidation]
        public JsonResult GetList(string type)
        {
            StringBuilder where = new StringBuilder();
            where.Append("IsImage='0' and IsDeleted='0'");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            string orderField = "UpdateTime desc";
            int recordCount = donationBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount);
            object list = null;
            if (recordCount > 0)
            {
                int startIndex = PageSize * (PageIndex - 1);
                IList<Model.TD_Donation_Info> donationList = donationBLL.GetListByPage(where.ToString(), orderField, dict, startIndex, PageSize);
                list = BuildList(donationList);
            }
            else
            {
                list = new List<Model.TD_Donation_Info>();
            }
            return GetSucceedListResult(list, recordCount);
        }

        private object BuildList(IList<Model.TD_Donation_Info> list)
        {
            IList<object> newList = new List<object>();
            foreach (var model in list)
            {
                newList.Add(new
                {
                    id = model.DonationID,
                    ImageURL = model.ImageURL,
                    Title = model.Title
                });
            }
            return newList;
        }

        [HttpPost]
        [ShiMiaoWXValidation]
        public JsonResult GetTopAD()
        {
            int top = 5;
            StringBuilder where = new StringBuilder();
            where.Append("IsImage='1' and IsDeleted='0'");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            string orderField = "UpdateTime desc";
            int recordCount = donationBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount);
            object list = null;
            if (recordCount > 0)
            {
                int startIndex = PageSize * (PageIndex - 1);
                int endIndex = PageSize * PageIndex;
                IList<Model.TD_Donation_Info> donationList = donationBLL.GetTopList(top, where.ToString(), orderField, dict);
                list = BuildADList(donationList);
            }
            else
            {
                list = new List<Model.TD_Donation_Info>();
            }
            return GetSucceedListResult(list, recordCount);
        }

        private object BuildADList(IList<Model.TD_Donation_Info> list)
        {
            var newList = from model in list
                          select new
                          {
                              ImageURL = model.ImageURL,
                              TargetURL = string.Format("/Donation/XMJK1/Detail?oid={0}&id={1}", ViewBag.EnOrgID, model.DonationID),
                              Title = model.Title
                          };
            return newList;
        }

        [ShiMiaoWXValidation]
        public ActionResult Detail(string id)
        {
            Model.TD_Donation_Info model = donationBLL.GetModel(id);
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}项目捐款", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View(model);
        }

        [HttpPost]
        [ShiMiaoWXValidation]
        public JsonResult GetDetailImage(string id)
        {
            Model.TD_Donation_Info model = donationBLL.GetModel(id);
            List<object> list = new List<object>();
            if (model != null)
            {
                list.Add(new
                {
                    ImageURL = model.ImageURL,
                    Title = model.Title
                });
            }
            return GetSucceedListResult(list, list.Count);
        }

        [ShiMiaoWXValidation]
        public ActionResult Order(string id)
        {
            Model.TD_Donation_Info model = donationBLL.GetModel(id);
            string url = Request.Url.AbsoluteUri;
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            share.Title = string.Format("{0}项目捐款", "净名寺");
            share.Link = url;
            share.Image = Request.Url.Scheme + "://" + Request.Url.Authority + "/Content/Org/JMS/logo.jpg";
            share.Content = "欢迎转发分享，功德无量";
            ViewBag.ShareInfo = share;
            return View(model);
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
            order.DonationType = (int)Constants.DonationType.XMJK1;
            order.OrgID = int.Parse(DESEncrypt.Decrypt(ViewBag.EnOrgID));
            int result = orderBLL.Save(order);
            if (result > 0)
            {
                string url = string.Empty;
                if (order.PayType == Constants.PayType.WeiXin)
                {
                    url = "/WeiXinPay/PayForDonation?oid=" + ViewBag.EnOrgID + "&orderid=" + order.OrderID + "&url=" + HttpUtility.UrlEncode("/Donation/XMJK1/Index?oid=" + ViewBag.EnOrgID);
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
        public JsonResult GetOrderList(string id)
        {
            StringBuilder where = new StringBuilder();
            where.Append("DonationID=@DonationID and IsPay='1'");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("DonationID", id);
            string orderField = "PayTime desc";
            int recordCount = orderBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount, 50);
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
            foreach (var model in list)
            {
                newList.Add(new
                {
                    NickName = DisableName(!string.IsNullOrEmpty(model.Name) ? model.Name : model.NickName),
                    Fee = model.Fee,
                    PayTime = model.PayTime.HasValue ? model.PayTime.Value.ToString("yyyy-MM-dd") : string.Empty
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

        [HttpPost]
        public JsonResult GetLeaveMessageList(string id)
        {
            StringBuilder where = new StringBuilder();
            where.Append("DonationID=@DonationID");
            IDictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("DonationID", id);
            string orderField = "CreateTime desc";
            int recordCount = leaveMsgBLL.GetRecordCount(where.ToString(), dict);
            InitPager(recordCount, 50);
            object list = null;
            if (recordCount > 0)
            {
                int startIndex = PageSize * (PageIndex - 1);
                IList<Model.TD_Donation_LeaveMsg> leaveMsgList = leaveMsgBLL.GetListByPage(where.ToString(), orderField, dict, startIndex, PageSize);
                list = BuildLeaveMsgList(leaveMsgList);
            }
            else
            {
                list = new List<Model.TD_Donation_LeaveMsg>();
            }
            return GetSucceedListResult(list, recordCount);
        }

        private object BuildLeaveMsgList(IList<Model.TD_Donation_LeaveMsg> list)
        {
            IList<object> newList = new List<object>();
            foreach (var model in list)
            {
                newList.Add(new
                {
                    id = model.LeaveMsgID,
                    HeaderImage = model.HeaderImage,
                    NickName = model.NickName,
                    Message = model.Message,
                    CreateTime = model.CreateTime.Value.ToString("yyyy-MM-dd HH:mm")
                });
            }
            return newList;
        }

        [ShiMiaoWXValidation]
        [HttpPost]
        public JsonResult SaveLeaveMessage(Model.TD_Donation_LeaveMsg model)
        {
            if (string.IsNullOrEmpty(model.Message))
            {
                return GetErrorResult("请输入留言内容");
            }
            if (model.Message.Length > 2000)
            {
                return GetErrorResult("内容不能超过2000字");
            }
            model.LeaveMsgID = Guid.NewGuid().ToString();
            model.CreateTime = DateTime.Now;
            var member = MemberData.GetMember();
            if (member != null)
            {
                model.HeaderImage = member.HeaderImage;
                model.MemberID = member.MemberID;
                model.NickName = member.NickName;
            }
            int result = leaveMsgBLL.Add(model);
            if (result > 0)
            {
                return GetSucceedResult(new
                {

                }, "保存成功");
            }
            else
            {
                return GetErrorResult("保存失败");
            }
        }
    }
}