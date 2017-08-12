using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model.Alipay
{
    public class m_Alipay_Member
    {
        public string MemberID { set; get; }
        public int? OrgID { set; get; }
        public string NickName { set; get; }
        public string HeaderImage { set; get; }
        public string UserID { set; get; }
        public short? Sex { set; get; }
        public string Country { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
        public DateTime? CreateTime { set; get; }
        public string IsFocused { set; get; }
        public DateTime? FocusTime { set; get; }
        public DateTime? UnFocusTime { set; get; }
    }
}
