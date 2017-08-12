using ShiMiao.Common;
using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ShiMiao.Web.Controllers
{
    public class WeiXinController : MiniWebBaseController
    {
        private static readonly BLL.TD_WeiXin_Member memberBLL = new BLL.TD_WeiXin_Member();
        public ActionResult Monitor()
        {
            string oid = Request["oid"];
            if (string.IsNullOrEmpty(oid))
            {
                return Content("未配置正确的回调地址");
            }
            string echoStr = Request["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string domain = Request.Url.Scheme + "://" + Request.Url.Authority;
            if (!string.IsNullOrEmpty(echoStr)
                && !string.IsNullOrEmpty(signature)
                && !string.IsNullOrEmpty(timestamp)
                && !string.IsNullOrEmpty(nonce))
            {
                if (CheckSignature(signature, timestamp, nonce))
                {
                    return Content(echoStr);
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
                return ResponseMessage();
            }
        }

        private ActionResult ResponseMessage()
        {
            WeiXinPort port = new WeiXinPort();
            port.Subscribe += (openID) => {
                DateTime now = DateTime.Now;
                Model.TD_WeiXin_Member member = memberBLL.GetModelByOpenID(openID);
                if (member == null)
                {
                    ThirdPortUserInfo userInfo = port.GetUserInfo(openID);
                    member = new Model.TD_WeiXin_Member();
                    member.MemberID = Guid.NewGuid().ToString();
                    member.NickName = userInfo.nickname;
                    member.Sex = (short)userInfo.sex;
                    member.Country = userInfo.country;
                    member.Province = userInfo.province;
                    member.City = userInfo.city;
                    member.OpenID = userInfo.openid;
                    member.HeaderImage = userInfo.headimgurl;
                    member.CreateTime = now;
                    member.IsFocused = "1";
                    member.FocusTime = now;
                    member.OrgID = int.Parse(DESEncrypt.Decrypt(Request["oid"]));
                    memberBLL.Add(member);
                }
                else
                {
                    member.IsFocused = "1";
                    member.FocusTime = now;
                    memberBLL.Update(member);
                }
            };
            port.UnSubscribe += (openID) => {
                DateTime now = DateTime.Now;
                Model.TD_WeiXin_Member member = memberBLL.GetModelByOpenID(openID);
                if (member != null)
                {
                    member.IsFocused = "0";
                    member.UnFocusTime = now;
                    memberBLL.Update(member);
                }
            };
            string response = port.ProcessMessage(Request.InputStream);
            return Content(response);
        }

        private bool CheckSignature(string signature, string timestamp, string nonce)
        {
            WeiXinPort port = new WeiXinPort();
            return port.CheckSignature(signature, timestamp, nonce);
        }

        [HttpPost]
        public JsonResult SignShared(string url)
        {
            WeiXinPort port = new WeiXinPort();
            port.InitSignature(url);
            ThirdSharedInfo share = port.GetShareInfo(url);
            JavaScriptSerializer a = new JavaScriptSerializer();
            return GetSucceedResult(new {
                ShareInfo = share
            }, null);
        }
    }
}
