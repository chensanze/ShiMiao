using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace ShiMiao.Model
{
         //图片明细表
        public partial class T_Image_Detail
    {
                
          /// <summary>
        /// 广告明细ID
        /// </summary>
        public decimal DetailID{set;get;}
        /// <summary>
        /// 机构ID
        /// </summary>
        public int? OrgID{set;get;}
        /// <summary>
        /// 广告主ID
        /// </summary>
        public decimal? OwnerID{set;get;}
        /// <summary>
        /// 广告主名称
        /// </summary>
        public string OwnerName{set;get;}
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageURL{set;get;}
        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetURL{set;get;}
        /// <summary>
        /// 广告名称
        /// </summary>
        public string Name{set;get;}
        /// <summary>
        /// 状态
        /// </summary>
        public string IsEnabled{set;get;}
        /// <summary>
        /// 计价方式
        /// </summary>
        public int? PriceType{set;get;}
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? UnitPrice{set;get;}
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Quantity{set;get;}
        /// <summary>
        /// 总价
        /// </summary>
        public decimal? TotalAmount{set;get;}
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime{set;get;}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark{set;get;}
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreateTime{set;get;}
        /// <summary>
        /// 添加人ID
        /// </summary>
        public decimal? CreatorID{set;get;}
        /// <summary>
        /// 添加人姓名
        /// </summary>
        public string CreatorName{set;get;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime{set;get;}
        /// <summary>
        /// 修改人ID
        /// </summary>
        public decimal? UpdaterID{set;get;}
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdaterName{set;get;}
                
        
   
    }
}

