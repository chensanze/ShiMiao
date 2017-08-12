using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class TD_Shop_OrderGoods
    {
        public string OrderGoodsID { set; get; }
        public string OrderID { set; get; }
        public string GoodsID { set; get; }
        public string Title { set; get; }
        public int? Amount { set; get; }
        public int? OrgID { set; get; }
        public decimal? OriPrice { set; get; }
        public decimal? RealPrice { set; get; }
        public DateTime? OrderTime { set; get; }
    }
}
