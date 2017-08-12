using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiMiao.Web.Areas.Shop.Models
{
    public class m_Shop_Order
    {
        public string goodsID { get; set; }
        public int Amount { get; set; }
        public string username { get; set; }
        public string message { get; set; }
    }
}