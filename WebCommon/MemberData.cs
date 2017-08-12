using System;
using System.Web;
using System.Web.Security;

namespace ShiMiao.WebCommon
{
    public class MemberData
    {
        private static readonly BLL.TD_WeiXin_Member memberBLL = new BLL.TD_WeiXin_Member();
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string MemberID
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.User.Identity.Name;
                }
            }
        }

        public static void AddMember(Model.TD_WeiXin_Member member)
        {
            if (HttpRuntime.Cache[member.MemberID] == null)
            {
                HttpRuntime.Cache.Insert(member.MemberID, member);
            }
        }

        public static void RemoveMember()
        {
            if (!string.IsNullOrEmpty(MemberID))
            {
                HttpRuntime.Cache.Remove(MemberID.ToString());
            }
        }

        public static Model.TD_WeiXin_Member GetMember()
        {
            Model.TD_WeiXin_Member model = null;
            if (HttpContext.Current.Request.Url.Host == "localhost")
            {
                model = new Model.TD_WeiXin_Member();
                model.NickName = "林子";
                model.MemberID = "2cc0f491-cad9-4e4a-9eea-da9bfb682afb";
                model.HeaderImage = "http://wx.qlogo.cn/mmopen/UmpmmQW4reL1FicWwRjSZzMWQeDibic8814LFYCxRiaAPiaQgofqFPnmb3icibf8a0qXxmwT8nFCAIN609DlWKEfkE6yrxgTpln2azt/0";
                model.OpenID = "oRlgyxDK7cNDn9CSI4Sfo_SY6AUM";
                return model;
            }
            if (!string.IsNullOrEmpty(MemberID))
            {
                model = HttpRuntime.Cache[MemberID] as Model.TD_WeiXin_Member;
                if (model == null)
                {
                    model = memberBLL.GetModel(MemberID);
                    if (model != null)
                    {
                        SignModel(model);
                    }
                }
            }
            return model;
        }

        public static void RefreshMember()
        {
            Model.TD_WeiXin_Member model = memberBLL.GetModel(MemberID);
            if (model != null)
            {
                ReSignModel(model);
            }
        }

        public static void SignModel(Model.TD_WeiXin_Member model)
        {
            AddMember(model);

            SignIn(model);
        }

        public static void ReSignModel(Model.TD_WeiXin_Member model)
        {
            if (HttpRuntime.Cache[model.MemberID] != null)
            {
                HttpRuntime.Cache[model.MemberID] = model;
            }
            else
            {
                HttpRuntime.Cache.Insert(model.MemberID, model);
            }
        }

        private static void SignIn(Model.TD_WeiXin_Member model)
        {
            FormsAuthenticationTicket tk = new FormsAuthenticationTicket(1,
                    model.MemberID,
                    DateTime.Now,
                    DateTime.MaxValue,
                    true,
                    "",
                    System.Web.Security.FormsAuthentication.FormsCookiePath
                    );

            string key = System.Web.Security.FormsAuthentication.Encrypt(tk);

            HttpCookie ck = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, key);
            //同一个域名下面的多个站点是否能共享Cookie
            //ck.Domain = System.Web.Security.FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(ck);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }

        public static void Logout()
        {
            RemoveMember();
            SignOut();
        }
    }
}
