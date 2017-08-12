using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Stone.DBUtility
{
    internal class DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>
        where TConnnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TDataAdapter : DbDataAdapter, new()
        where TParameter : DbParameter, new()
    {
        #region ExecuteNonQuery
        /// <summary>
        /// 使用默认连接， 执行SQL语句，仅仅返回数据库受影响行数。
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteNonQuery(Cfg.Instance.ConnectionString, cmdText, cmdType, cmdParms);
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(string connString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            using (TConnnection conn = new TConnnection())
            {
                int val = 0;
                conn.ConnectionString = connString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(TConnnection conn, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            int val = 0;
            TCommand cmd = new TCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。(用于需要事务的情况)
        /// 所需参数：事务对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>数据库受影响行数</returns>
        public static int ExecuteNonQuery(DbTransaction trans, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            int val = 0;
            TCommand cmd = new TCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行一个sql查询语句，返回DataReader对象。使用默认连接。
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteReader(Cfg.Instance.ConnectionString, cmdType, cmdParms);
        }

        /// <summary>
        /// 执行一个sql查询命令，返回DataReader对象。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询的结果 DataReader对象</returns>
        public static DbDataReader ExecuteReader(string connString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();
            TConnnection conn = new TConnnection();
            conn.ConnectionString = connString;

            // 此处使用try/catch的原因：当出现异常时，也可以保证能关闭连接。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 执行一个sql查询命令，返回DataReader对象。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(TConnnection conn, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            DbDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(DbTransaction tran, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            PrepareCommand(cmd, tran.Connection, tran, cmdType, cmdText, cmdParms);
            DbDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行一个sql查询语句，返回查询结果的第一行第一列的值。使用默认连接
        /// 所需参数：命令文本，参数列表。
        /// </summary>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteScalar(Cfg.Instance.ConnectionString, cmdText, cmdType, cmdParms);
        }


        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值。
        /// 所需参数：连接字符串，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(string connString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            using (TConnnection conn = new TConnnection())
            {
                conn.ConnectionString = connString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值。
        /// 所需参数：连接对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(TConnnection conn, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;

        }
        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列的值, 支持事务。
        /// 所需参数：事务对象，命令类型，命令文本，参数列表。
        /// </summary>
        /// <param name="tran">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果的第一行第一列的值</returns>
        public static object ExecuteScalar(DbTransaction tran, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            PrepareCommand(cmd, tran.Connection, tran, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;

        }
        #endregion

        #region FillDataTable
        /// <summary>
        /// 执行一个SQL查询，填充DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataTable(string cmdText, CommandType cmdType, DataTable dt, params TParameter[] cmdParms)
        {
            FillDataTable(Cfg.Instance.ConnectionString, cmdText, cmdType, dt, cmdParms);
        }
        /// <summary>
        /// 执行一个SQL查询，填充DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataTable(string connString, string cmdText, CommandType cmdType, DataTable dt, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();

            using (TConnnection conn = new TConnnection())
            {
                conn.ConnectionString = connString;

                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                TDataAdapter ada = new TDataAdapter();
                ada.SelectCommand = cmd;
                ada.Fill(dt);
                ada.Dispose();
            }
        }
        /// <summary>
        /// 重载FillDataTable 添加事务参数 执行SQL查询填充DataTable
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParms"></param>
        public static void FillDataTable(DbTransaction tran, string cmdText, CommandType cmdType, DataTable dt, params TParameter[] cmdParms)
        {
            TCommand cmd = new TCommand();
            PrepareCommand(cmd, tran.Connection, tran, cmdType, cmdText, cmdParms);
            TDataAdapter ada = new TDataAdapter();
            ada.SelectCommand = cmd;
            ada.Fill(dt);
            ada.Dispose();
        }
        #endregion

        #region PrepareCommand
        /// <summary>
        /// 准备一个可以执行的Sql命令对象。
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="conn">连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">参数列表</param>
        private static void PrepareCommand(TCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, TParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                AttachParameters(cmd, cmdParms);
            }
        }

        /// <summary>
        /// 附加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandParameters"></param>
        private static void AttachParameters(TCommand command, TParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (TParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion       

        #region 参数缓存处理
        /// <summary>
        /// 存储参数对象
        /// </summary>
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 缓存一个参数数组。
        /// </summary>
        /// <param name="cacheKey">参数数组在缓存中的名称</param>
        /// <param name="cmdParms">参数数组</param>
        public static void SetCacheParameters(string cacheKey, params TParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// 从缓存中获取一个参数数组。
        /// </summary>
        /// <param name="cacheKey">参数数组在缓存中的名称</param>
        /// <returns>参数数组的副本</returns>
        public static TParameter[] GetCachedParameters(string cacheKey)
        {
            TParameter[] cachedParms = (TParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            //克隆一个参数数组的原因：此处的操作会对获取的参数进行赋值操作。
            TParameter[] clonedParms = new TParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (TParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
            
        }
        #endregion

    }
}