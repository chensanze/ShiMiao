using MySql.Data.MySqlClient;
using ShiMiao.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiMiao.Model;
using System.Data.Common;

namespace ShiMiao.DAL
{
    public class TD_WeiXin_Member
    {
        public int Add(Model.TD_WeiXin_Member model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TD_WeiXin_Member(");
            strSql.Append("MemberID,NickName,HeaderImage,OpenID,CreateTime,IsFocused,FocusTime,UnFocusTime,Sex,Country,Province,City,OrgID");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@NickName,@HeaderImage,@OpenID,@CreateTime,@IsFocused,@FocusTime,@UnFocusTime,@Sex,@Country,@Province,@City,@OrgID");
            strSql.Append(") ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@MemberID", model.MemberID),
                        new MySqlParameter("@NickName", model.NickName),
                        new MySqlParameter("@HeaderImage", model.HeaderImage),
                        new MySqlParameter("@OpenID", model.OpenID),
                        new MySqlParameter("@CreateTime", model.CreateTime),
                        new MySqlParameter("@IsFocused", model.IsFocused),
                        new MySqlParameter("@FocusTime", model.FocusTime),
                        new MySqlParameter("@UnFocusTime", model.UnFocusTime),
                        new MySqlParameter("@Sex", model.Sex),
                        new MySqlParameter("@Country", model.Country),
                        new MySqlParameter("@Province", model.Province),
                        new MySqlParameter("@City", model.City),
                        new MySqlParameter("@OrgID", model.OrgID)
            };
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        public IList<Model.TD_WeiXin_Member> GetListByMemberIDs(IList<string> list)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TEMPORARY TABLE TEMP(id varchar(36));");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO TEMP(id) VALUES(@Parameter{0});", i);
                sql.AppendLine();
            }
            sql.Append(" SELECT t1.MemberID,t1.NickName,t1.HeaderImage,t1.OpenID,t1.CreateTime,t1.IsFocused,t1.FocusTime,t1.UnFocusTime,t1.Sex,t1.Country,t1.Province,t1.City");
            sql.Append(" from TD_WeiXin_Member t1 INNER JOIN TEMP t2 ON t1.MemberID=t2.id;");
            sql.Append(" drop table TEMP;");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            IList<ShiMiao.Model.TD_WeiXin_Member> modelList = new List<ShiMiao.Model.TD_WeiXin_Member>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(sql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_WeiXin_Member model = new ShiMiao.Model.TD_WeiXin_Member();
                    if (dr[0] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.IsFocused = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.FocusTime = dr.GetDateTime(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.UnFocusTime = dr.GetDateTime(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.Sex = dr.GetInt16(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.Country = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Province = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.City = dr.GetString(11);
                    }
                    modelList.Add(model);
                }
                parameters.Clear();
                return modelList;
            }
        }

        public Model.TD_WeiXin_Member GetModel(string memberID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MemberID,NickName,HeaderImage,OpenID,CreateTime,IsFocused,FocusTime,UnFocusTime,Sex,Country,Province,City");
            strSql.Append("  from TD_WeiXin_Member ");
            strSql.Append(" where MemberID=@MemberID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@MemberID", memberID)

                        };

            ShiMiao.Model.TD_WeiXin_Member model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    model = new ShiMiao.Model.TD_WeiXin_Member();
                    if (dr[0] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.IsFocused = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.FocusTime = dr.GetDateTime(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.UnFocusTime = dr.GetDateTime(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.Sex = dr.GetInt16(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.Country = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Province = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.City = dr.GetString(11);
                    }
                }
                return model;
            }
        }

        public int Update(Model.TD_WeiXin_Member model)
        {
            StringBuilder strSql = new StringBuilder();
            List<string> fields = new List<string>();
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            strSql.Append("update TD_WeiXin_Member set ");

            if (model.FocusTime.HasValue)
            {
                fields.Add("FocusTime=@FocusTime");
                parameters.Add(new MySqlParameter("@FocusTime", model.FocusTime));
            }
            if (!string.IsNullOrEmpty(model.IsFocused))
            {
                fields.Add("IsFocused=@IsFocused");
                parameters.Add(new MySqlParameter("@IsFocused", model.IsFocused));
            }
            if (model.UnFocusTime.HasValue)
            {
                fields.Add("UnFocusTime=@UnFocusTime");
                parameters.Add(new MySqlParameter("@UnFocusTime", model.UnFocusTime));
            }
            strSql.Append(string.Join(",", fields));
            strSql.Append(" where MemberID=@MemberID  ");
            parameters.Add(new MySqlParameter("@MemberID", model.MemberID));
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters.ToArray());
        }

        public Model.TD_WeiXin_Member GetModelByOpenID(string openID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MemberID,NickName,HeaderImage,OpenID,CreateTime,IsFocused,FocusTime,UnFocusTime,Sex,Country,Province,City");
            strSql.Append("  from TD_WeiXin_Member ");
            strSql.Append(" where OpenID=@OpenID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@OpenID", openID)

                        };

            ShiMiao.Model.TD_WeiXin_Member model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    model = new ShiMiao.Model.TD_WeiXin_Member();
                    if (dr[0] != DBNull.Value)
                    {
                        model.MemberID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.NickName = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.HeaderImage = dr.GetString(2);
                    }
                    if (dr[3] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(3);
                    }
                    if (dr[4] != DBNull.Value)
                    {
                        model.CreateTime = dr.GetDateTime(4);
                    }
                    if (dr[5] != DBNull.Value)
                    {
                        model.IsFocused = dr.GetString(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.FocusTime = dr.GetDateTime(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.UnFocusTime = dr.GetDateTime(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.Sex = dr.GetInt16(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.Country = dr.GetString(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Province = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.City = dr.GetString(11);
                    }
                }
                return model;
            }
        }
    }
}
