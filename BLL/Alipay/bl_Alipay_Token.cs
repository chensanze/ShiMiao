using Newtonsoft.Json;
using ShiMiao.Model.Alipay;
using System;
using System.Web;

namespace ShiMiao.BLL.Alipay
{
    public class bl_Alipay_Token
    {
        private string _orgid=string.Empty;
        private m_Alipay_AppInfo _appInfo ;
        private string _Auth_Code=string.Empty;
        public bl_Alipay_Token(string orgid,string auth_code=null)
        {
            this._orgid = orgid;
            if (!string.IsNullOrEmpty(auth_code)) this._Auth_Code = auth_code;
            _appInfo = new bl_Alipay_AppInfo(_orgid).appInfo;
        }
        private m_Alipay_Token _dicTokens
        {
            get
            {
                bool isUpdate = false;
                m_Alipay_Server_Token token = new m_Alipay_Server_Token();
                string key = this.GetType().AssemblyQualifiedName + "/{_orgid}";
                var o = HttpRuntime.Cache.Get(key) as m_Alipay_Token;
                if (null == o)
                {
                    if (string.IsNullOrEmpty(this._Auth_Code))
                        throw new Exception("未获得用户授权，无法获取访问令牌！");
                    token = Alipay_Token.askTokenByCodeFromServer(_appInfo.app_id, _Auth_Code);
                    if (null== token)
                        throw new Exception("获取通行证失败！");
                    isUpdate = true;
                }
                else
                {
                    if(o.expire.AddMinutes(30) < DateTime.Now)
                    {//还有30分钟token就要过期了 更新token
                        token = Alipay_Token.askTokenByOldFromServer(_appInfo.app_id, o.access_token);
                        if (null == token)
                            throw new Exception("获取通行证失败！");
                        isUpdate = true;
                    }
                }
                if(isUpdate)
                {
                    o.access_token = token.access_token;
                    o.expire = DateTime.Now.AddSeconds(token.expires_in);
                    HttpRuntime.Cache.Insert(key, o);
                }
                return o;
            }
        }
       

    }
}
