using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;

namespace ShiMiao.BLL
{
    public class TD_WeiXin_Member
    {
        private static readonly DAL.TD_WeiXin_Member dal = new DAL.TD_WeiXin_Member();
        public int Add(Model.TD_WeiXin_Member model)
        {
            return dal.Add(model);
        }

        public Model.TD_WeiXin_Member GetModelByOpenID(string openID)
        {
            return dal.GetModelByOpenID(openID);
        }

        public int Update(Model.TD_WeiXin_Member model)
        {
            return dal.Update(model);
        }

        public Model.TD_WeiXin_Member GetModel(string memberID)
        {
            return dal.GetModel(memberID);
        }

        public IList<Model.TD_WeiXin_Member> GetListByMemberIDs(IList<string> memberIDs)
        {
            return dal.GetListByMemberIDs(memberIDs);
        }
    }
}
