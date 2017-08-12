using ShiMiao.Common;
using Stone.WeiXin;
using System;
using System.Web.Mvc;

namespace ShiMiao.WebCommon
{
    public class ShiMiaoWXValidationAttribute : WXValidationAttribute
    {
        private static readonly BLL.TD_WeiXin_Member memberBLL = new BLL.TD_WeiXin_Member();

        public override void LoadContext(ActionExecutingContext filterContext)
        {
            string enOrgID = filterContext.HttpContext.Request["oid"];
            if (string.IsNullOrEmpty(enOrgID))
            {
                throw new Exception("未指定OID");
            }
            filterContext.Controller.ViewBag.EnOrgID = enOrgID;
        }
        public override object GetMember()
        {
            return MemberData.GetMember();
        }

        public override void AddMember(ThirdPortUserInfo userInfo, ActionExecutingContext filterContext)
        {
            Model.TD_WeiXin_Member member = memberBLL.GetModelByOpenID(userInfo.openid);
            if (member == null)
            {
                member = new Model.TD_WeiXin_Member();
                member.MemberID = Guid.NewGuid().ToString();
                member.NickName = userInfo.nickname;
                member.Sex = (short)userInfo.sex;
                member.OpenID = userInfo.openid;
                member.Country = userInfo.country;
                member.Province = userInfo.province;
                member.City = userInfo.province;
                member.HeaderImage = userInfo.headimgurl;
                member.CreateTime = DateTime.Now;
                member.OrgID = int.Parse(DESEncrypt.Decrypt(filterContext.Controller.ViewBag.EnOrgID));
                memberBLL.Add(member);
            }
            MemberData.SignModel(member);
        }
    }
}
