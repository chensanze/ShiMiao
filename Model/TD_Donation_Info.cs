using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ShiMiao.Model{
         //TD_Donation_Info
        public partial class TD_Donation_Info : BaseModel
    {
                
          /// <summary>
        /// DonationID
        /// </summary>
        public string DonationID {set;get;}
        public int? OrgID { set; get; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title{set;get;}
        /// <summary>
        /// ImageURL
        /// </summary>
        public string ImageURL{set;get;}
        /// <summary>
        /// Content
        /// </summary>
        public string Content{set;get;}
        /// <summary>
        /// ReadCount
        /// </summary>
        public int? ReadCount{set;get;}
        /// <summary>
        /// IsImage
        /// </summary>
        public string IsImage{set;get;}
        /// <summary>
        /// IsDeleted
        /// </summary>
        public string IsDeleted{set;get;}
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime{set;get;}
        /// <summary>
        /// CreatorID
        /// </summary>
        public decimal? CreatorID{set;get;}
        /// <summary>
        /// CreatorName
        /// </summary>
        public string CreatorName{set;get;}
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime? UpdateTime{set;get;}
        /// <summary>
        /// UpdaterID
        /// </summary>
        public decimal? UpdaterID{set;get;}
        /// <summary>
        /// UpdaterName
        /// </summary>
        public string UpdaterName{set;get;}
        /// <summary>
        /// PublishOrgID
        /// </summary>
        public decimal? PublishOrgID{set;get;}
        /// <summary>
        /// PublishOrgName
        /// </summary>
        public string PublishOrgName{set;get;}
                
        
   
    }
}

