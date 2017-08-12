using ShiMiao.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.BLL
{
    public class bl_Shop_Order_Consignee
    {
        private readonly dl_Shop_Order_Consignee dl_Order_Consignee = new dl_Shop_Order_Consignee();
        public ShiMiao.Model.TD_Shop_Order_Consignee getConsignee(string OrderID)
        {
            return dl_Order_Consignee.getConsignee(OrderID);
        }
        public bool Exists(string OrderID)
        {
            return dl_Order_Consignee.Exists(OrderID);
        }
        public int Add(ShiMiao.Model.TD_Shop_Order_Consignee model)
        {
            return dl_Order_Consignee.Add(model);
        }
        public int Add(ShiMiao.Model.TD_Shop_Order_Consignee model, string tranID)
        {
            return dl_Order_Consignee.Add(model, tranID);
        }
    }
}
