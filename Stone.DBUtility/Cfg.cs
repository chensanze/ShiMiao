using System.Configuration;
using Stone.Common;

namespace Stone.DBUtility
{
    /// <summary>
    ///  ˝æ›≈‰÷√œÓ
    /// </summary>
    public sealed class Cfg
    {
        private string connectionString;
        private static Cfg instance = null;
        private static readonly object instanceLock = new object();
        public static Cfg Instance
        {
            get
            {
                if (instance == null || string.IsNullOrEmpty(instance.ConnectionString))
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new Cfg();
                        }
                        instance.ConnectionString = GetDefaultConntection();
                    }
                }
                return instance;
            }
        }

        private static string GetDefaultConntection()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public string ConnectionString
        {
            set { connectionString = value; }
            get { return connectionString; }
        }
    }
}
