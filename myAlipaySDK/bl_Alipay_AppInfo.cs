using ShiMiao.DAL.Alipay;
using ShiMiao.Model.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShiMiao.BLL.Alipay
{
    public class bl_Alipay_AppInfo
    {
        private string _orgid = string.Empty;
        private readonly dl_Alipay_AppInfo dl_AppInfo;
        public m_Alipay_AppInfo appInfo
        {
            get {
                var o = HttpRuntime.Cache.Get(this.GetType().AssemblyQualifiedName + "/{_orgid}") as m_Alipay_AppInfo;
                if (null == o)
                {
                    o = dl_AppInfo.getModel();
                    if (null == o) throw new Exception("未找到/{_orgid}对应的信息");
                    HttpRuntime.Cache.Insert(this.GetType().AssemblyQualifiedName + "/{_orgid}", o);
                }
                return o;
            }
        }
        public bl_Alipay_AppInfo(string orgid)
        {
            this._orgid = orgid;
            dl_AppInfo = new dl_Alipay_AppInfo("/{_orgid}");
        }
        
    }
}
