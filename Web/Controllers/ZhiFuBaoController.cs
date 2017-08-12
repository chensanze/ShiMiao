using Aop.Api.Util;
using ShiMiao.Common;
using Stone.ZhiFuBao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ShiMiao.Web.Controllers
{
    public class ZhiFuBaoController : Controller
    {

        public ActionResult Gateway()
        {
            string oid = Request["oid"];
            if (string.IsNullOrEmpty(oid))
            {
                return Content("未配置正确的网关地址");
            }
            int orgID = int.Parse(DESEncrypt.Decrypt(oid));
            string service = Request["service"];
            if (!string.IsNullOrEmpty(service))
            {
                string signType = Request["sign_type"];
                string charset = Request["charset"];
                string bizContent = Request["biz_content"];
                string sign = Request["sign"];
                if (service == "alipay.service.check")
                {
                    if (CheckSignature(orgID, service, signType, charset, bizContent, sign))
                    {
                        return ResponseCheckMessage(true, orgID);
                    }
                    else
                    {
                        return ResponseCheckMessage(false, orgID);
                    }
                }
                else if (service == "alipay.mobile.public.message.notify")
                {
                    return ResponseNotifyMessage(orgID, bizContent);
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }

        private ActionResult ResponseCheckMessage(bool isSuccess, int orgID)
        {
            ZhiFuBaoPort port = new ZhiFuBaoPort(orgID);
            string response = port.CheckMessage(isSuccess);
            return Content(response);
        }

        private ActionResult ResponseNotifyMessage(int orgID, string bizContent)
        {
            ZhiFuBaoPort port = new ZhiFuBaoPort(orgID);
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
            string response = port.ProcessMessage();
            return Content(response);
        }

        private bool CheckSignature(int orgID, string service, string signType, string charset, string bizContent, string sign)
        {
            ZhiFuBaoPort port = new ZhiFuBaoPort(orgID);
            return port.CheckSignature(service, signType, charset, bizContent, sign);
        }
    }
}