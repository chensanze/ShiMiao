using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;

namespace ShiMiao.BLL
{
    public partial class TD_Shop_OrderGoods
    {
        private static readonly DAL.TD_Shop_OrderGoods dal = new DAL.TD_Shop_OrderGoods();
        public int Add(Model.TD_Shop_OrderGoods model, string tranID)
        {
            return dal.Add(model, tranID);
        }

        public IList<Model.TD_Shop_OrderGoods> GetListByOrderID(string orderID)
        {
            return dal.GetListByOrderID(orderID);
        }
    }
}
