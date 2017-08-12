using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAlipaySDK
{
    public class m_Alipay_SubButton
    {
        public string name { get; set; }
        public string action_type { get; set; }
        public string action_param { get; set; }
        public string icon { get; set; }
    }
    public class m_Alipay_Button: m_Alipay_SubButton
    {
        public List<m_Alipay_SubButton> sub_button { get; set; } = new List<m_Alipay_SubButton>();
    }
    public class m_Alipay_Menu
    {
        public List<m_Alipay_Button> Button { get; set; }=new List<m_Alipay_Button>();
        public string type { get; set; } = "icon";
    }
    public class alipay_menu_create_response
    {
        public string code { get; set; } = "1000";
        public string msg { get; set; } = "Success";
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string menu_key { get; set; } = "default";
    }
    public class Alipay_Menu
    {
        public static alipay_menu_create_response CreateMenu(string app_id,m_Alipay_Menu menu)
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", app_id, "\\RSA\\merchant_private_key_2048.txt", "json", "1.0", "RSA2", "\\RSA\\alipay_public_key_sha256.txt", "GBK", true);
            AlipayOpenPublicMenuCreateRequest request = new AlipayOpenPublicMenuCreateRequest();
            //request.BizContent = "{" +
            //"      \"button\":[{" +
            //"        \"name\":\"最新优惠\"," +
            //"\"action_type\":\"link\"," +
            //"\"action_param\":\"http://m.alipay.com\"," +
            //"\"icon\":\"http://xxxx/test.jpg\"," +
            //"          \"sub_button\":[{" +
            //"            \"name\":\"流量查询\"," +
            //"\"action_type\":\"link\"," +
            //"\"action_param\":\"http://m.alipay.com\"," +
            //"\"icon\":\"http://example.com/test.jpg\"" +
            //"            }]" +
            //"        }]," +
            //"\"type\":\"icon\"" +
            //"  }";
            request.BizContent = JsonConvert.SerializeObject(menu);
            AlipayOpenPublicMenuCreateResponse response = client.Execute(request);
            var rsp = JsonConvert.DeserializeObject<alipay_menu_create_response>(response.Body);
            if (null == rsp) return null;
            else return rsp;
        }
    }
}
