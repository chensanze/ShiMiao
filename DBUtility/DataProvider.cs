using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace ShiMiao.DBUtility
{
    public class DataProvider<TConnnection, TCommand, TDataAdapter, TParameter>
        where TConnnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TDataAdapter : DbDataAdapter, new()
        where TParameter : DbParameter, new()
    {
        #region 事务连接的缓存处理
        /// <summary>
        /// 事务对象缓存
        /// </summary>
        static Hashtable tranCache = new Hashtable();
        /// <summary>
        /// 连接对象缓存
        /// </summary>
        static Hashtable connCache = new Hashtable();
        /// <summary>
        /// 启动一个事务，返回可以引用事务的 事务ID
        /// </summary>
        /// <returns>事务ID</returns>
        public static string BeginTran(string connectionString)
        {
            string tranId = Guid.NewGuid().ToString();
            DbTransaction tran = OpenTransaction(connectionString);
            tranCache.Add(
                tranId,
                tran
                );
            connCache.Add(
                tranId,
                tran.Connection
                );
            return tranId;
        }
        public static string BeginTran()
        {
            return BeginTran(Cfg.Instance.ConnectionString);
        }
        static DbTransaction OpenTransaction(string connectionString)
        {
            TConnnection cn = new TConnnection();
            cn.ConnectionString = connectionString;
            cn.Open();
            return cn.BeginTransaction();
        }
        /// <summary>
        /// 提交指定 事务ID 的事务
        /// </summary>
        /// <param name="tranId"></param>
        public static void CommitTran(string tranId)
        {
            DbTransaction tran = tranCache[tranId] as DbTransaction;
            if (tran == null)
                throw new ApplicationException("指定事务不存在");
            tran.Commit();
            TConnnection conn = connCache[tranId] as TConnnection;
            if (conn != null
                && ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 回滚指定 事务ID 的事务
        /// </summary>
        /// <param name="tranId"></param>
        public static void RollbackTran(string tranId)
        {
            DbTransaction tran = tranCache[tranId] as DbTransaction;
            if (tran == null)
                throw new ApplicationException("指定事务不存在");
            tran.Rollback();
            TConnnection conn = connCache[tranId] as TConnnection;
            if (conn != null
                && ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 关闭指定 事务ID 的事务所用的连接对象。
        /// </summary>
        /// <param name="tranId"></param>
        public static void CloseConn(string tranId)
        {
            DbTransaction tran = tranCache[tranId] as DbTransaction;
            if (tran == null)
                throw new ApplicationException("指定事务不存在");
            TConnnection conn = connCache[tranId] as TConnnection;
            if (conn == null)
                throw new ApplicationException("指定连接不存在");
            conn.Close();

            tranCache.Remove(tranId);
            connCache.Remove(tranId);
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string tranID, string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteNonQuery(null, tranID, cmdText, CommandType.Text, cmdParms);
        }
        public static int ExecuteNonQuery(string connectionString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteNonQuery(connectionString, null, cmdText, cmdType, cmdParms);
        }
        public static int ExecuteNonQuery(string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteNonQuery(Cfg.Instance.ConnectionString, null, cmdText, CommandType.Text, cmdParms);
        }
        /// <summary>
        /// 执行一个sql命令，仅仅返回数据库受影响行数。
        /// </summary>
        /// <param name="tranID">事务ID</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>        
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string connectionString, string tranID, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            if (tranID == null)
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteNonQuery(
                    connectionString,
                    cmdText,
                    cmdType,
                    cmdParms
                );
            }
            else
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteNonQuery(
                    tranCache[tranID] as DbTransaction,
                    cmdText,
                    cmdType,
                    cmdParms
               );
            }
        }
        #endregion

        #region ExecuteReader
        public static DbDataReader ExecuteReader(string tranID, string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteReader(null, tranID, cmdText, CommandType.Text, cmdParms);
        }
        public static DbDataReader ExecuteReader(string connectionString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteReader(connectionString, null, cmdText, cmdType, cmdParms);
        }
        public static DbDataReader ExecuteReader(string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteReader(Cfg.Instance.ConnectionString, null, cmdText, CommandType.Text, cmdParms);
        }
        /// <summary>
        /// 执行一个sql查询命令，返回DataReader对象。
        /// </summary>
        /// <param name="tranID">事务ID</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        private static DbDataReader ExecuteReader(string connectionString, string tranID, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            if (tranID == null)
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteReader(
                    connectionString,
                    cmdText,
                    cmdType,
                    cmdParms
                );
            }
            else
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteReader(
                    tranCache[tranID] as DbTransaction,
                    cmdText,
                    cmdType,
                    cmdParms
               );
            }
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(string tranID, string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteScalar(null, tranID, cmdText, CommandType.Text, cmdParms);
        }
        public static object ExecuteScalar(string connectionString, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            return ExecuteScalar(connectionString, null, cmdText, cmdType, cmdParms);
        }
        public static object ExecuteScalar(string cmdText, params TParameter[] cmdParms)
        {
            return ExecuteScalar(Cfg.Instance.ConnectionString, null, cmdText, CommandType.Text, cmdParms);
        }
        /// <summary>
        /// 执行一个sql查询命令，返回查询结果的第一行第一列
        /// </summary>
        /// <param name="tranID">事务ID</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        private static object ExecuteScalar(string connectionString, string tranID, string cmdText, CommandType cmdType, params TParameter[] cmdParms)
        {
            if (tranID == null)
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteScalar(
                    connectionString,
                    cmdText,
                    cmdType,
                    cmdParms
                );
            }
            else
            {
                return DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.ExecuteScalar(
                    tranCache[tranID] as DbTransaction,
                    cmdText,
                    cmdType,
                    cmdParms
               );
            }
        }
        #endregion

        #region FillDataTable
        public static void FillDataTable(string tranID, string cmdText, DataTable dt, params TParameter[] cmdParms)
        {
            FillDataTable(null, tranID, cmdText, dt, CommandType.Text, cmdParms);
        }
        public static void FillDataTable(string connectionString, string cmdText, DataTable dt, CommandType cmdType, params TParameter[] cmdParms)
        {
            FillDataTable(connectionString, null, cmdText, dt, cmdType, cmdParms);
        }
        public static void FillDataTable(string cmdText, DataTable dt, params TParameter[] cmdParms)
        {
            FillDataTable(Cfg.Instance.ConnectionString, null, cmdText, dt, CommandType.Text, cmdParms);
        }
        /// <summary>
        /// 填充一个表
        /// </summary>
        /// <param name="tranID"></param>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParms"></param>
        private static void FillDataTable(string connectionString, string tranID, string cmdText, DataTable dt, CommandType cmdType, params TParameter[] cmdParms)
        {
            if (tranID == null)
            {
                DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.FillDataTable(
                    connectionString,
                    cmdText,
                    cmdType,
                    dt,
                    cmdParms
                );
            }
            else
            {
                DataHelper<TConnnection, TCommand, TDataAdapter, TParameter>.FillDataTable(
                    tranCache[tranID] as DbTransaction,
                    cmdText,
                    cmdType,
                    dt,
                    cmdParms
                );
            }
        }
        #endregion
    }
}
