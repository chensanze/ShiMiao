using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class OrderDetail
    {
        public string NickName { set; get; }
        public DateTime? OrderTime { set; get; }
        public decimal? RealPrice { set; get; }
        public int? Amount { set; get; }
        public string Title { set; get; }
    }
}
