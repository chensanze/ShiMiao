using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class TD_Shop_Order
    {
        public string OrderID { set; get; } 
        public string PayNo { set; get; }
        public int? OrgID { set; get; }
        public decimal? OriPrice { set; get; }
        public decimal? RealPrice { set; get; }
        public int? Status { set; get; }
        public string MemberID { set; get; }
        public string NickName { set; get; }
        public string HeaderImage { set; get; }
        public string Mobile { set; get; }
        public int? OrderType { set; get; }
        public int? PayType { set; get; }
        public string IsPay { set; get; }
        public string IsMemberDeleted { set; get; }
        public DateTime? OrderTime { set; get; }
        public DateTime? PayTime { set; get; }
        public DateTime? CompleteTime { set; get; }
        public string Message { get; set; }
        /// <summary>
        /// 额外的费用 如 快递费
        /// </summary>
        public decimal? ExtraPrice { get; set; }
    }
}
