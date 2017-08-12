using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.Model
{
    public class TD_WeiXin_Menu
    {
        public int? MenuID { set; get; }
        public int? OrgID { set; get; }
        public string OrgName { set; get; }
        public string MenuName { set; get; }
        public int? MenuType { set; get; }
        public string MenuValue { set; get; }
        public int? ParentID { set; get; }
        public int? OrderIndex { set; get; }
    }
}
