using System.Data.SqlClient;

namespace Stone.DBUtility
{
    /// <summary>
    /// SQL SERVER数据库数据操作
    /// </summary>
    public class SqlHelper :DataProvider<SqlConnection, SqlCommand, SqlDataAdapter, SqlParameter>
    {
    }
}
