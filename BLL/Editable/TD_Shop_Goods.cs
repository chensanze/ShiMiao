using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;
using ShiMiao.Common;

namespace ShiMiao.BLL
{
    public class TD_Shop_Goods
    {
        private static readonly DAL.TD_Shop_Goods dal = new DAL.TD_Shop_Goods();
        public int GetRecordCount(string where, IDictionary<string, object> dict)
        {
            return dal.GetRecordCount(where, dict);
        }

        public int ClearFrozenGoods()
        {
            DateTime time = DateTime.Now.AddMinutes(-30);
            int orderStatus = (int)Constants.OrderStatus.Cancel;
            return dal.ClearFrozenGoods(time, orderStatus);
        }

        public IList<ShiMiao.Model.TD_Shop_Goods> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            return dal.GetListByPage(where, orderby, dictParams, startIndex, pageSize);
        }

        public Model.TD_Shop_Goods GetModel(string goodsID)
        {
            return dal.GetModel(goodsID);
        }

        public int PayOrder(string orderID, IList<Model.TD_Shop_OrderGoods> orderGoodsList, string tranID)
        {
            foreach (Model.TD_Shop_OrderGoods orderGoods in orderGoodsList)
            {
                int result = dal.PayOrder(orderGoods.GoodsID, orderGoods.Amount.Value, tranID);
                if (result == 0)
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}
