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
	    //TD_Donation_Order
	public partial class TD_Donation_Order
	{
		public bool Exists(Guid OrderID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Donation_Order");
			strSql.Append(" where ");
			strSql.Append(" OrderID = @OrderID ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@OrderID", OrderID)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters);
			return result != null;
		}
		
		public bool Exists(Guid OrderID, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Donation_Order");
			strSql.Append(" where ");
			strSql.Append(" OrderID = @OrderID ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@OrderID", OrderID)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(tranID, strSql.ToString(), parameters);
			return result != null;
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Donation_Order(");
            strSql.Append("OrderID,PayNo,DonationID,OpenID,Name,Mobile,NickName,HeaderImage,OrderTime,Fee,IsPay,PayType,PayTime");
            strSql.Append(") values (");
            strSql.Append("@OrderID,@PayNo,@DonationID,@OpenID,@Name,@Mobile,@NickName,@HeaderImage,@OrderTime,@Fee,@IsPay,@PayType,@PayTime");
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID),            
                        new MySqlParameter("@DonationID", model.DonationID),
                        new MySqlParameter("@OpenID", model.OpenID),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Mobile", model.Mobile),
                        new MySqlParameter("@NickName", model.NickName),            
                        new MySqlParameter("@HeaderImage", model.HeaderImage),            
                        new MySqlParameter("@OrderTime", model.OrderTime),            
                        new MySqlParameter("@Fee", model.Fee),            
                        new MySqlParameter("@IsPay", model.IsPay),            
                        new MySqlParameter("@PayType", model.PayType),            
                        new MySqlParameter("@PayTime", model.PayTime)            
              
            };
			 return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);			
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Order model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Donation_Order(");			
            strSql.Append("OrderID,PayNo,DonationID,OpenID,Name,Mobile,NickName,HeaderImage,OrderTime,Fee,IsPay,PayType,PayTime,DonationType,MemberID,OrgID,Message,IsAnonymous");
			strSql.Append(") values (");
            strSql.Append("@OrderID,@PayNo,@DonationID,@OpenID,@Name,@Mobile,@NickName,@HeaderImage,@OrderTime,@Fee,@IsPay,@PayType,@PayTime,@DonationType,@MemberID,@OrgID,@Message,@IsAnonymous");            
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID),
                        new MySqlParameter("@PayNo", model.PayNo),
                        new MySqlParameter("@DonationID", model.DonationID),            
                        new MySqlParameter("@OpenID", model.OpenID),
                        new MySqlParameter("@Name", model.Name),
                        new MySqlParameter("@Mobile", model.Mobile),
                        new MySqlParameter("@NickName", model.NickName),            
                        new MySqlParameter("@HeaderImage", model.HeaderImage),            
                        new MySqlParameter("@OrderTime", model.OrderTime),            
                        new MySqlParameter("@Fee", model.Fee),            
                        new MySqlParameter("@IsPay", model.IsPay),            
                        new MySqlParameter("@PayType", model.PayType),            
                        new MySqlParameter("@PayTime", model.PayTime),
                        new MySqlParameter("@DonationType", model.DonationType),
                        new MySqlParameter("@MemberID", model.MemberID),
                        new MySqlParameter("@OrgID", model.OrgID),
                        new MySqlParameter("@Message", model.Message),
                        new MySqlParameter("@IsAnonymous", model.IsAnonymous)

            };
			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Order set ");
			                        
            strSql.Append(" OrderID = @OrderID , ");                                    
            strSql.Append(" DonationID = @DonationID , ");                                    
            strSql.Append(" OpenID = @OpenID , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Mobile = @Mobile , ");
            strSql.Append(" NickName = @NickName , ");                                    
            strSql.Append(" HeaderImage = @HeaderImage , ");                                    
            strSql.Append(" OrderTime = @OrderTime , ");                                    
            strSql.Append(" Fee = @Fee , ");                                    
            strSql.Append(" IsPay = @IsPay , ");                                    
            strSql.Append(" PayType = @PayType , ");                                    
            strSql.Append(" PayTime = @PayTime  ");            			
			strSql.Append(" where OrderID=@OrderID  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID) ,            
                        new MySqlParameter("@DonationID", model.DonationID) ,            
                        new MySqlParameter("@OpenID", model.OpenID) ,
                         new MySqlParameter("@Name", model.Name) ,
                        new MySqlParameter("@Mobile", model.Mobile) ,
                       new MySqlParameter("@NickName", model.NickName) ,            
                        new MySqlParameter("@HeaderImage", model.HeaderImage) ,            
                        new MySqlParameter("@OrderTime", model.OrderTime) ,            
                        new MySqlParameter("@Fee", model.Fee) ,            
                        new MySqlParameter("@IsPay", model.IsPay) ,            
                        new MySqlParameter("@PayType", model.PayType) ,            
                        new MySqlParameter("@PayTime", model.PayTime)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Order model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Order set ");
			                        
            strSql.Append(" OrderID = @OrderID , ");                                    
            strSql.Append(" DonationID = @DonationID , ");
            strSql.Append(" OpenID = @OpenID , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Mobile = @Mobile , ");
            strSql.Append(" NickName = @NickName , ");                                    
            strSql.Append(" HeaderImage = @HeaderImage , ");                                    
            strSql.Append(" OrderTime = @OrderTime , ");                                    
            strSql.Append(" Fee = @Fee , ");                                    
            strSql.Append(" IsPay = @IsPay , ");                                    
            strSql.Append(" PayType = @PayType , ");                                    
            strSql.Append(" PayTime = @PayTime  ");            			
			strSql.Append(" where OrderID=@OrderID  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID) ,            
                        new MySqlParameter("@DonationID", model.DonationID) ,
                        new MySqlParameter("@OpenID", model.OpenID) ,
                         new MySqlParameter("@Name", model.Name) ,
                        new MySqlParameter("@Mobile", model.Mobile) ,
                        new MySqlParameter("@NickName", model.NickName) ,            
                        new MySqlParameter("@HeaderImage", model.HeaderImage) ,            
                        new MySqlParameter("@OrderTime", model.OrderTime) ,            
                        new MySqlParameter("@Fee", model.Fee) ,            
                        new MySqlParameter("@IsPay", model.IsPay) ,            
                        new MySqlParameter("@PayType", model.PayType) ,            
                        new MySqlParameter("@PayTime", model.PayTime)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid OrderID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Donation_Order ");
			strSql.Append(" where OrderID=@OrderID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid OrderID, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Donation_Order ");
			strSql.Append(" where OrderID=@OrderID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(Guid OrderID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Order set IsDeleted='1'");
			strSql.Append(" where OrderID=@OrderID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(Guid OrderID, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Order set IsDeleted='1'");
			strSql.Append(" where OrderID=@OrderID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
				/// <summary>
		/// 增加阅读数
		/// </summary>
		public int AddReadCount(Guid id)
		{
			string sql = "UPDATE TD_Donation_Order SET ReadCount=ReadCount+1 WHERE OrderID=@ID";
			MySqlParameter[] parameters = {
				new MySqlParameter("@ID", id)
			};
			return MySqlHelperUtil.ExecuteNonQuery(sql, parameters);
		}
		
		/// <summary>
		/// 批量软删除一批数据
		/// </summary>
		public int DeleteLogic(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" UPDATE TD_Donation_Order SET IsDeleted='1' FROM TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            int result = MySqlHelperUtil.ExecuteNonQuery(sql.ToString(), parameters.ToArray());
            parameters.Clear();
            return result;
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int Delete(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" UPDATE TD_Donation_Order SET IsDeleted='1' FROM TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            int result = MySqlHelperUtil.ExecuteNonQuery(sql.ToString(), parameters.ToArray());
            parameters.Clear();
            return result;
		}
		
		/// <summary>
		/// 批量物理删除一批数据
		/// </summary>
		public int DeletePhysical(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" DELETE TD_Donation_Order FROM TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            int result = MySqlHelperUtil.ExecuteNonQuery(sql.ToString(), parameters.ToArray());
            parameters.Clear();
            return result;
		}
		
		/// <summary>
		/// 批量启用一批数据
		/// </summary>
		public int SetEnabled(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" UPDATE TD_Donation_Order SET IsEnabled='1' FROM TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            int result = MySqlHelperUtil.ExecuteNonQuery(sql.ToString(), parameters.ToArray());
            parameters.Clear();
            return result;
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int SetDisabled(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" UPDATE TD_Donation_Order SET IsEnabled='0' FROM TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
            int result = MySqlHelperUtil.ExecuteNonQuery(sql.ToString(), parameters.ToArray());
            parameters.Clear();
            return result;
		}
		
		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Order> GetList(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" SELECT t1.OrderID, t1.DonationID, t1.OpenID, t1.NickName, t1.HeaderImage, t1.OrderTime, t1.Fee, t1.IsPay, t1.PayType, t1.PayTime ");
            sql.Append(" from TD_Donation_Order t1 INNER JOIN #TEMP t2 ON t1.OrderID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
			IList<ShiMiao.Model.TD_Donation_Order> modelList= new List<ShiMiao.Model.TD_Donation_Order>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(sql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Order model = new ShiMiao.Model.TD_Donation_Order();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(2);
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
                        model.OrderTime = dr.GetDateTime(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Fee = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    modelList.Add(model);
                }
                parameters.Clear();
                return modelList;
            }
		}



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ShiMiao.Model.TD_Donation_Order GetModel(string OrderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID, DonationID, OpenID, NickName, HeaderImage, OrderTime, Fee, IsPay, PayType, PayTime,Name,Mobile  ");
            strSql.Append("  from TD_Donation_Order ");
            strSql.Append(" where OrderID=@OrderID ");
            MySqlParameter[] parameters = {
                                    new MySqlParameter("@OrderID", OrderID)

                        };

            ShiMiao.Model.TD_Donation_Order model = null;
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    model = new ShiMiao.Model.TD_Donation_Order();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(2);
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
                        model.OrderTime = dr.GetDateTime(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Fee = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Name = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.Mobile = dr.GetString(11);
                    }
                }
                return model;
            }
        }
		
		/// <summary>
		/// 获取记录条数
		/// </summary>
		public int GetRecordCount(string strWhere, IDictionary<string, object> dictParams)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from TD_Donation_Order");
			if(!string.IsNullOrEmpty(strWhere))
			{
				strSql.Append(" where "+strWhere);
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
		/// 获得数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Order> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderID, DonationID, OpenID, NickName, HeaderImage, OrderTime, Fee, IsPay, PayType, PayTime,Name,Mobile,PayNo  ");
			strSql.Append(" FROM TD_Donation_Order ");
			if(!string.IsNullOrEmpty(where))
			{
				strSql.AppendFormat(" where {0}", where);
			}
			if(!string.IsNullOrEmpty(orderBy))
			{
				strSql.AppendFormat(" order by {0}", orderBy);
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
			IList<ShiMiao.Model.TD_Donation_Order> list= new List<ShiMiao.Model.TD_Donation_Order>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
            {
                while (dr.Read())
                {
                    ShiMiao.Model.TD_Donation_Order model = new ShiMiao.Model.TD_Donation_Order();
                    if (dr[0] != DBNull.Value)
                    {
                        model.OrderID = dr.GetString(0);
                    }
                    if (dr[1] != DBNull.Value)
                    {
                        model.DonationID = dr.GetString(1);
                    }
                    if (dr[2] != DBNull.Value)
                    {
                        model.OpenID = dr.GetString(2);
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
                        model.OrderTime = dr.GetDateTime(5);
                    }
                    if (dr[6] != DBNull.Value)
                    {
                        model.Fee = dr.GetDecimal(6);
                    }
                    if (dr[7] != DBNull.Value)
                    {
                        model.IsPay = dr.GetString(7);
                    }
                    if (dr[8] != DBNull.Value)
                    {
                        model.PayType = dr.GetInt32(8);
                    }
                    if (dr[9] != DBNull.Value)
                    {
                        model.PayTime = dr.GetDateTime(9);
                    }
                    if (dr[10] != DBNull.Value)
                    {
                        model.Name = dr.GetString(10);
                    }
                    if (dr[11] != DBNull.Value)
                    {
                        model.Mobile = dr.GetString(11);
                    }
                    list.Add(model);
                }
                parameters.Clear();
                return list;
            }
		}
	}
}