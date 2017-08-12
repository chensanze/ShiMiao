using MySql.Data.MySqlClient;

namespace ShiMiao.DBUtility
{
    /// <summary>
    /// SQL SERVER数据库数据操作
    /// </summary>
    public class MySqlHelperUtil : DataProvider<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlParameter>
    {
    }
}
