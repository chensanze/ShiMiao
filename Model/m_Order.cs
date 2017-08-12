using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class m_Order
    {
        /// <summary>
        /// 本商户订单号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 平台交易流水号
        /// </summary>
        public string TransID { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string OrderFee { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// 订单状态 1.待支付 2.已支付 3.申请退款 4.已退款
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime Createtime { get; set; }
        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
    }
}
