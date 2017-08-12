using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ShiMiao.Model{
         //微信支付订单记录
        public partial class TD_Order_WeiXinPay : BaseModel
    {
                
          /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID{set;get;}
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr{set;get;}
        /// <summary>
        /// 时间戳
        /// </summary>
        public int? Timestamp{set;get;}
        /// <summary>
        /// 机构ID
        /// </summary>
        public int? OrgID{set;get;}
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string WeiXinOrderID{set;get;}
        /// <summary>
        /// 订单金额
        /// </summary>
        public int? OrderFee{set;get;}
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int? CashFee{set;get;}
        /// <summary>
        /// 预支付交易会话标识
        /// </summary>
        public string Package{set;get;}
        /// <summary>
        /// 支付状态
        /// </summary>
        public int? Status{set;get;}
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime{set;get;}
        /// <summary>
        /// 回调时间
        /// </summary>
        public DateTime? CallBackTime{set;get;}
                
        
   
    }
}

