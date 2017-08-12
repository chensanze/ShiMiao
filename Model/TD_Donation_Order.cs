using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ShiMiao.Model{
         //TD_Donation_Order
        public partial class TD_Donation_Order : BaseModel
    {
                
          /// <summary>
        /// OrderID
        /// </summary>
        public string OrderID{set;get;}
        public int? OrgID { set; get; }
        public string PayNo { set; get; }
        /// <summary>
        /// DonationID
        /// </summary>
        public string DonationID{set;get;}
        public string Mobile { set; get; }
        public string Name { set; get; }
        public string MemberID { set; get; }
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID{set;get;}
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName{set;get;}
        /// <summary>
        /// HeaderImage
        /// </summary>
        public string HeaderImage{set;get;}
        /// <summary>
        /// OrderTime
        /// </summary>
        public DateTime? OrderTime{set;get;}
        /// <summary>
        /// Fee
        /// </summary>
        public decimal? Fee{set;get;}
        /// <summary>
        /// IsPay
        /// </summary>
        public string IsPay{set;get;}
        /// <summary>
        /// PayType
        /// </summary>
        public int? PayType{set;get;}
        /// <summary>
        /// PayTime
        /// </summary>
        public DateTime? PayTime{set;get;}
        /// <summary>
        /// 捐款类型
        /// </summary>
        public int? DonationType { set; get; }
        public string Message { set; get; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public string IsAnonymous { set; get; }

    }
}

