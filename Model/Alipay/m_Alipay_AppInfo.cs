using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model.Alipay
{
    public class m_Alipay_AppInfo
    {
        public string app_id { get; set; }
        public string orgid { get; set; }
        /// <summary>
        /// 区分类型 是支付宝还是微信
        /// </summary>
        public int type { get; set; }
    }
}
