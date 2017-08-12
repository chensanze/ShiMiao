using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class TD_Donation_LeaveMsg
    {
        public string LeaveMsgID { set; get; }
        public int? OrgID { set; get; }
        public string DonationID { set; get; }
        public string MemberID { set; get; }
        public string NickName { set; get; }
        public string HeaderImage { set; get; }
        public string Message { set; get; }
        public DateTime? CreateTime { set; get; }
    }
}
