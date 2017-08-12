using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.DAL
{
    public class TD_Donation_LeaveMsg
    {
        public int Add(Model.TD_Donation_LeaveMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_Donation_LeaveMsg(");
            strSql.Append("LeaveMsgID,DonationID,MemberID,NickName,HeaderImage,Message,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@LeaveMsgID,@DonationID,@MemberID,@NickName,@HeaderImage,@Message,@CreateTime");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@LeaveMsgID", model.LeaveMsgID),
                        new MySqlParameter("@DonationID", model.DonationID),
                        new MySqlParameter("@MemberID", model.MemberID),
                        new MySqlParameter("@NickName", model.NickName),
                        new MySqlParameter("@HeaderImage", model.HeaderImage),
                        new MySqlParameter("@Message", model.Message),
                        new MySqlParameter("@CreateTime", model.CreateTime)

            };
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        public int GetRecordCount(string strWhere, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TD_Donation_LeaveMsg");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters.ToArray());
            parameters.Clear();
            return int.Parse(result.ToString());
        }

        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Donation_LeaveMsg> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT LeaveMsgID,DonationID,MemberID,NickName,HeaderImage,Message,CreateTime FROM TD_Donation_LeaveMsg");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.AppendFormat(" order by {0}", orderby);
            }
            strSql.AppendFormat(" limit {0}, {1}", startIndex, pageSize);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            IList<ShiMiao.Model.TD_Donation_LeaveMsg> list = new List<ShiMiao.Model.TD_Donation_LeaveMsg>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_LeaveMsg model = new ShiMiao.Model.TD_Donation_LeaveMsg();

                    if (dr[0] != DBNull.Value)
                    {
                        model.LeaveMsgID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.Message = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(6);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }
    }
}
