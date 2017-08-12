using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.BLL
{
    public class TD_WeiXin_Menu
    {
        private static readonly DAL.TD_WeiXin_Menu dal = new DAL.TD_WeiXin_Menu();

        /// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<Model.TD_WeiXin_Menu> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
        {
            return dal.GetList(where, orderBy, dictParams);
        }
    }
}
