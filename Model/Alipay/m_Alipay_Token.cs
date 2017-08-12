using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model.Alipay
{
    
    public class m_Alipay_Token
    {
        /// <summary>
        /// 授权访问令牌
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expire { get; set; }
    }
}
