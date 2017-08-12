using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json;
using System;

namespace ShiMiao.BLL.Alipay
{
    public class m_Alipay_Server_Token
    {
        /// <summary>
        /// 授权访问令牌
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 用户的userId 支付宝用户的唯一userId
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 用户的open_id（已废弃，请勿使用）
        /// </summary>
        public string alipay_user_id { get; set; }
        /// <summary>
        /// 令牌有效期 交换令牌的有效期，单位秒
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 刷新令牌有效期 刷新令牌有效期，单位秒
        /// </summary>
        public int re_expires_in { get; set; }
        /// <summary>
        /// 刷新令牌 通过该令牌可以刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
    }
    public class Alipay_Token
    {
        public static m_Alipay_Server_Token askTokenByCodeFromServer(string app_id, string auth_code)
        {
            try
            {
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", app_id, "\\RSA\\merchant_private_key_2048.txt", "json", "1.0", "RSA2", "\\RSA\\alipay_public_key_sha256.txt", "GBK", true);
                AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest();
                request.GrantType = "authorization_code";
                request.Code = auth_code;//"4b203fe6c11548bcabd8da5bb087a83b";
                                         //request.RefreshToken = "201208134b203fe6c11548bcabd8da5bb087a83b";
                AlipaySystemOauthTokenResponse response = client.Execute(request);
                var token = JsonConvert.DeserializeObject<m_Alipay_Server_Token>(response.Body);
                if (null == token) return null;
                else return token;
            }
            catch (Exception ex)
            {
                //记录日志
            }
            return null;
        }
        public static m_Alipay_Server_Token askTokenByOldFromServer(string app_id, string refresh_token)
        {
            try
            {
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", app_id, "\\RSA\\merchant_private_key_2048.txt", "json", "1.0", "RSA2", "\\RSA\\alipay_public_key_sha256.txt", "GBK", true);
                AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest();
                request.GrantType = "refresh_token";
                //request.Code = "4b203fe6c11548bcabd8da5bb087a83b";
                request.RefreshToken = refresh_token;//"201208134b203fe6c11548bcabd8da5bb087a83b";
                AlipaySystemOauthTokenResponse response = client.Execute(request);
                var token = JsonConvert.DeserializeObject<m_Alipay_Server_Token>(response.Body);
                if (null == token) return null;
                else return token;
            }
            catch (Exception ex)
            {
                //记录日志
            }
            return null;
        }


    }
}
