using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.BLL
{
    public class TD_Donation_LeaveMsg
    {
        private static readonly DAL.TD_Donation_LeaveMsg dal = new DAL.TD_Donation_LeaveMsg();

        public int Add(Model.TD_Donation_LeaveMsg model)
        {
            return dal.Add(model);
        }
        public int GetRecordCount(string strWhere, IDictionary<string, object> dictParams)
        {
            return dal.GetRecordCount(strWhere, dictParams);
        }

        public IList<ShiMiao.Model.TD_Donation_LeaveMsg> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            return dal.GetListByPage(where, orderby, dictParams, startIndex, pageSize);
        }
    }
}
