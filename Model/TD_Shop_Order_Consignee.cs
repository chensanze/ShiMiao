using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class TD_Shop_Order_Consignee
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 收件人联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 收获地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string Identification { get; set; } = null;

    }
}
