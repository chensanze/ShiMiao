using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.WebCommon.Alipay
{
    public class AlipayValidationAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //请求支付宝页面时获取身份信息
            //https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id=APPID&scope=SCOPE&redirect_uri=ENCODED_URL
            var enOrgID = (filterContext.Controller.ValueProvider.GetValue("oid") == null)
                    ? string.Empty
                    : filterContext.Controller.ValueProvider.GetValue("oid").AttemptedValue;
            if (string.IsNullOrEmpty(enOrgID))
            {
                throw new Exception("未指定OID");
            }
            
            
            var auth_code = (filterContext.Controller.ValueProvider.GetValue("auth_code") == null)
                ? string.Empty
                : filterContext.Controller.ValueProvider.GetValue("auth_code").AttemptedValue;
            if (string.IsNullOrEmpty(auth_code))
            {
                //需要获取到授权码
                filterContext.Controller.ViewBag.EnOrgID = enOrgID;
                var appInfo = new BLL.Alipay.bl_Alipay_AppInfo(enOrgID).appInfo;
                var request = (HttpWebRequest)WebRequest.Create("https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id=" + appInfo.app_id + "&scope=auth_user&redirect_uri=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
                return;
            }
            BLL.Alipay.bl_Alipay_Token token = new BLL.Alipay.bl_Alipay_Token(enOrgID,auth_code);
            

            /*
            var user_id = (filterContext.Controller.ValueProvider.GetValue("user_id") == null)
                    ? string.Empty
                    : filterContext.Controller.ValueProvider.GetValue("user_id").AttemptedValue;

            var auth_code = (filterContext.Controller.ValueProvider.GetValue("auth_code") == null)
                ? string.Empty
                : filterContext.Controller.ValueProvider.GetValue("auth_code").AttemptedValue;
            if(string.IsNullOrEmpty(auth_code) && string.IsNullOrEmpty(user_id))
            {
                //需要获取到授权码
                filterContext.Controller.ViewBag.EnOrgID = enOrgID;
                var appInfo = new BLL.Alipay.bl_Alipay_AppInfo(enOrgID).appInfo;
                var request = (HttpWebRequest)WebRequest.Create("https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id=" + appInfo.app_id + "&scope=auth_user&redirect_uri=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
                return;
            }
            else if (string.IsNullOrEmpty(user_id) && !string.IsNullOrEmpty(auth_code))
            {
                //获取用户ID

            }
            */




        }
    }
}
