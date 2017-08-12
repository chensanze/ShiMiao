using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json;
using System;

namespace ShiMiao.BLL.Alipay
{
    public class m_Alipay_server_UserInfo
    {
        /// <summary>
        /// 支付宝用户的userId 
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 用户类型（1/2）1代表公司账户2代表个人账户
        /// </summary>
        public string user_type { get; set; }
        /// <summary>
        /// 用户状态（Q/T/B/W）  Q代表快速注册用户   T代表已认证用户 B代表被冻结账户  W代表已注册，未激活的账户
        /// </summary>
        public string user_status { get; set; }
        /// <summary>
        /// 是否通过实名认证。T是通过 F是没有实名认证。 
        /// </summary>
        public string is_certified { get; set; }
        /// <summary>
        /// 省份名称 
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市名称。 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        ///  	用户昵称 
        /// </summary>
        public string nick_name { get; set; }
        /// <summary>
        /// 是否是学生 
        /// </summary>
        public string is_student_certified { get; set; }
        /// <summary>
        ///  	性别（F：女性；M：男性）。
        /// </summary>
        public string gender { get; set; }
    }
    public class bl_Alipay_User
    {
        public static m_Alipay_server_UserInfo getUserInfoFromAlipay(string app_id,string token)
        {
            try
            {
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", app_id, "\\RSA\\merchant_private_key_2048.txt", "json", "1.0", "RSA2", "\\RSA\\alipay_public_key_sha256.txt", "GBK", true);
                AlipayUserInfoShareRequest request = new AlipayUserInfoShareRequest();
                AlipayUserInfoShareResponse response = client.Execute(request, token);
                var user = JsonConvert.DeserializeObject<m_Alipay_server_UserInfo>(response.Body);
                if (null == user) return null;
                else return user;
            }
            catch(Exception ex)
            {
                //记录日志
            }
            return  null;
        }
        
    }
}
