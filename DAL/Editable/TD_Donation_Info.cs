using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ShiMiao.DBUtility;
using MySql.Data.MySqlClient;

namespace ShiMiao.DAL  
{
	    //TD_Donation_Info
	public partial class TD_Donation_Info
    {

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Donation_Info> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DonationID, Title, ImageURL, Content, ReadCount, IsImage, IsDeleted, CreateTime, CreatorID, CreatorName, UpdateTime, UpdaterID, UpdaterName, PublishOrgID, PublishOrgName  ");
            strSql.Append(" FROM TD_Donation_Info");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.AppendFormat(" order by {0}", orderBy);
            }
            strSql.AppendFormat(" limit {0} ", top);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dictParams != null)
            {
                foreach (var pair in dictParams)
                {
                    MySqlParameter parameter = new MySqlParameter("@" + pair.Key, pair.Value);
                    parameters.Add(parameter);
                }
            }
            IList<ShiMiao.Model.TD_Donation_Info> list = new List<ShiMiao.Model.TD_Donation_Info>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Info model = new ShiMiao.Model.TD_Donation_Info();
                    if (dr[0] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.Title = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.ImageURL = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Content = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.ReadCount = dr.GetInt32(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.IsImage = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.IsDeleted = dr.GetString(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.CreatorID = dr.GetDecimal(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.CreatorName = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.UpdateTime = dr.GetDateTime(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.UpdaterID = dr.GetDecimal(11);
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        model.UpdaterName = dr.GetString(12);
                    }
                    if (dr[13] != DBNull.Value)
                    {
                        model.PublishOrgID = dr.GetDecimal(13);
                    }
                    if (dr[14] != DBNull.Value)
                    {
                        model.PublishOrgName = dr.GetString(14);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        public IList<ShiMiao.Model.TD_Donation_Info> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DonationID, Title, ImageURL, Content, ReadCount, IsImage, IsDeleted, CreateTime, CreatorID, CreatorName, UpdateTime, UpdaterID, UpdaterName, PublishOrgID, PublishOrgName  FROM TD_Donation_Info");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
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
            IList<ShiMiao.Model.TD_Donation_Info> list = new List<ShiMiao.Model.TD_Donation_Info>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Info model = new ShiMiao.Model.TD_Donation_Info();

                    if (dr[0] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.Title = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.ImageURL = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.Content = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.ReadCount = dr.GetInt32(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.IsImage = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.IsDeleted = dr.GetString(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.CreatorID = dr.GetDecimal(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.CreatorName = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.UpdateTime = dr.GetDateTime(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.UpdaterID = dr.GetDecimal(11);
                    }
                    if (dr[12] != DBNull.Value)
                    {
                        model.UpdaterName = dr.GetString(12);
                    }
                    if (dr[13] != DBNull.Value)
                    {
                        model.PublishOrgID = dr.GetDecimal(13);
                    }
                    if (dr[14] != DBNull.Value)
                    {
                        model.PublishOrgName = dr.GetString(14);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
        }
    }
}