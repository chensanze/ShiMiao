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
		public bool Exists(Guid DonationID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Donation_Info");
			strSql.Append(" where ");
			strSql.Append(" DonationID = @DonationID ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@DonationID", DonationID)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters);
			return result != null;
		}
		
		public bool Exists(Guid DonationID, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Donation_Info");
			strSql.Append(" where ");
			strSql.Append(" DonationID = @DonationID ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@DonationID", DonationID)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(tranID, strSql.ToString(), parameters);
			return result != null;
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Donation_Info(");			
            strSql.Append("DonationID,Title,ImageURL,Content,ReadCount,IsImage,IsDeleted,CreateTime,CreatorID,CreatorName,UpdateTime,UpdaterID,UpdaterName,PublishOrgID,PublishOrgName");
			strSql.Append(") values (");
            strSql.Append("@DonationID,@Title,@ImageURL,@Content,@ReadCount,@IsImage,@IsDeleted,@CreateTime,@CreatorID,@CreatorName,@UpdateTime,@UpdaterID,@UpdaterName,@PublishOrgID,@PublishOrgName");            
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@DonationID", model.DonationID),            
                        new MySqlParameter("@Title", model.Title),            
                        new MySqlParameter("@ImageURL", model.ImageURL),            
                        new MySqlParameter("@Content", model.Content),            
                        new MySqlParameter("@ReadCount", model.ReadCount),            
                        new MySqlParameter("@IsImage", model.IsImage),            
                        new MySqlParameter("@IsDeleted", model.IsDeleted),            
                        new MySqlParameter("@CreateTime", model.CreateTime),            
                        new MySqlParameter("@CreatorID", model.CreatorID),            
                        new MySqlParameter("@CreatorName", model.CreatorName),            
                        new MySqlParameter("@UpdateTime", model.UpdateTime),            
                        new MySqlParameter("@UpdaterID", model.UpdaterID),            
                        new MySqlParameter("@UpdaterName", model.UpdaterName),            
                        new MySqlParameter("@PublishOrgID", model.PublishOrgID),            
                        new MySqlParameter("@PublishOrgName", model.PublishOrgName)            
              
            };
			 return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);			
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Info model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Donation_Info(");			
            strSql.Append("DonationID,Title,ImageURL,Content,ReadCount,IsImage,IsDeleted,CreateTime,CreatorID,CreatorName,UpdateTime,UpdaterID,UpdaterName,PublishOrgID,PublishOrgName");
			strSql.Append(") values (");
            strSql.Append("@DonationID,@Title,@ImageURL,@Content,@ReadCount,@IsImage,@IsDeleted,@CreateTime,@CreatorID,@CreatorName,@UpdateTime,@UpdaterID,@UpdaterName,@PublishOrgID,@PublishOrgName");            
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@DonationID", model.DonationID),            
                        new MySqlParameter("@Title", model.Title),            
                        new MySqlParameter("@ImageURL", model.ImageURL),            
                        new MySqlParameter("@Content", model.Content),            
                        new MySqlParameter("@ReadCount", model.ReadCount),            
                        new MySqlParameter("@IsImage", model.IsImage),            
                        new MySqlParameter("@IsDeleted", model.IsDeleted),            
                        new MySqlParameter("@CreateTime", model.CreateTime),            
                        new MySqlParameter("@CreatorID", model.CreatorID),            
                        new MySqlParameter("@CreatorName", model.CreatorName),            
                        new MySqlParameter("@UpdateTime", model.UpdateTime),            
                        new MySqlParameter("@UpdaterID", model.UpdaterID),            
                        new MySqlParameter("@UpdaterName", model.UpdaterName),            
                        new MySqlParameter("@PublishOrgID", model.PublishOrgID),            
                        new MySqlParameter("@PublishOrgName", model.PublishOrgName)            
              
            };
			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Info set ");
			                        
            strSql.Append(" DonationID = @DonationID , ");                                    
            strSql.Append(" Title = @Title , ");                                    
            strSql.Append(" ImageURL = @ImageURL , ");                                    
            strSql.Append(" Content = @Content , ");                                    
            strSql.Append(" ReadCount = @ReadCount , ");                                    
            strSql.Append(" IsImage = @IsImage , ");                                    
            strSql.Append(" IsDeleted = @IsDeleted , ");                                    
            strSql.Append(" CreateTime = @CreateTime , ");                                    
            strSql.Append(" CreatorID = @CreatorID , ");                                    
            strSql.Append(" CreatorName = @CreatorName , ");                                    
            strSql.Append(" UpdateTime = @UpdateTime , ");                                    
            strSql.Append(" UpdaterID = @UpdaterID , ");                                    
            strSql.Append(" UpdaterName = @UpdaterName , ");                                    
            strSql.Append(" PublishOrgID = @PublishOrgID , ");                                    
            strSql.Append(" PublishOrgName = @PublishOrgName  ");            			
			strSql.Append(" where DonationID=@DonationID  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@DonationID", model.DonationID) ,            
                        new MySqlParameter("@Title", model.Title) ,            
                        new MySqlParameter("@ImageURL", model.ImageURL) ,            
                        new MySqlParameter("@Content", model.Content) ,            
                        new MySqlParameter("@ReadCount", model.ReadCount) ,            
                        new MySqlParameter("@IsImage", model.IsImage) ,            
                        new MySqlParameter("@IsDeleted", model.IsDeleted) ,            
                        new MySqlParameter("@CreateTime", model.CreateTime) ,            
                        new MySqlParameter("@CreatorID", model.CreatorID) ,            
                        new MySqlParameter("@CreatorName", model.CreatorName) ,            
                        new MySqlParameter("@UpdateTime", model.UpdateTime) ,            
                        new MySqlParameter("@UpdaterID", model.UpdaterID) ,            
                        new MySqlParameter("@UpdaterName", model.UpdaterName) ,            
                        new MySqlParameter("@PublishOrgID", model.PublishOrgID) ,            
                        new MySqlParameter("@PublishOrgName", model.PublishOrgName)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Info model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Info set ");
			                        
            strSql.Append(" DonationID = @DonationID , ");                                    
            strSql.Append(" Title = @Title , ");                                    
            strSql.Append(" ImageURL = @ImageURL , ");                                    
            strSql.Append(" Content = @Content , ");                                    
            strSql.Append(" ReadCount = @ReadCount , ");                                    
            strSql.Append(" IsImage = @IsImage , ");                                    
            strSql.Append(" IsDeleted = @IsDeleted , ");                                    
            strSql.Append(" CreateTime = @CreateTime , ");                                    
            strSql.Append(" CreatorID = @CreatorID , ");                                    
            strSql.Append(" CreatorName = @CreatorName , ");                                    
            strSql.Append(" UpdateTime = @UpdateTime , ");                                    
            strSql.Append(" UpdaterID = @UpdaterID , ");                                    
            strSql.Append(" UpdaterName = @UpdaterName , ");                                    
            strSql.Append(" PublishOrgID = @PublishOrgID , ");                                    
            strSql.Append(" PublishOrgName = @PublishOrgName  ");            			
			strSql.Append(" where DonationID=@DonationID  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@DonationID", model.DonationID) ,            
                        new MySqlParameter("@Title", model.Title) ,            
                        new MySqlParameter("@ImageURL", model.ImageURL) ,            
                        new MySqlParameter("@Content", model.Content) ,            
                        new MySqlParameter("@ReadCount", model.ReadCount) ,            
                        new MySqlParameter("@IsImage", model.IsImage) ,            
                        new MySqlParameter("@IsDeleted", model.IsDeleted) ,            
                        new MySqlParameter("@CreateTime", model.CreateTime) ,            
                        new MySqlParameter("@CreatorID", model.CreatorID) ,            
                        new MySqlParameter("@CreatorName", model.CreatorName) ,            
                        new MySqlParameter("@UpdateTime", model.UpdateTime) ,            
                        new MySqlParameter("@UpdaterID", model.UpdaterID) ,            
                        new MySqlParameter("@UpdaterName", model.UpdaterName) ,            
                        new MySqlParameter("@PublishOrgID", model.PublishOrgID) ,            
                        new MySqlParameter("@PublishOrgName", model.PublishOrgName)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid DonationID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Donation_Info ");
			strSql.Append(" where DonationID=@DonationID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@DonationID", DonationID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid DonationID, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Donation_Info ");
			strSql.Append(" where DonationID=@DonationID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@DonationID", DonationID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(Guid DonationID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Info set IsDeleted='1'");
			strSql.Append(" where DonationID=@DonationID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@DonationID", DonationID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(Guid DonationID, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Donation_Info set IsDeleted='1'");
			strSql.Append(" where DonationID=@DonationID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@DonationID", DonationID)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
				/// <summary>
		/// 增加阅读数
		/// </summary>
		public int AddReadCount(Guid id)
		{
			string sql = "UPDATE TD_Donation_Info SET ReadCount=ReadCount+1 WHERE DonationID=@ID";
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
            sql.Append(" UPDATE TD_Donation_Info SET IsDeleted='1' FROM TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
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
            sql.Append(" UPDATE TD_Donation_Info SET IsDeleted='1' FROM TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
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
            sql.Append(" DELETE TD_Donation_Info FROM TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
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
            sql.Append(" UPDATE TD_Donation_Info SET IsEnabled='1' FROM TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
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
            sql.Append(" UPDATE TD_Donation_Info SET IsEnabled='0' FROM TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
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
		public IList<ShiMiao.Model.TD_Donation_Info> GetList(IList<Guid> list)
		{
			StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE #TEMP(id uniqueidentifier primary key)");
            for (int i = 0; i < list.Count; i++)
            {
                sql.AppendFormat(" INSERT INTO #TEMP VALUES(@Parameter{0})", i);
                sql.AppendLine();
            }
            sql.Append(" SELECT t1.DonationID, t1.Title, t1.ImageURL, t1.Content, t1.ReadCount, t1.IsImage, t1.IsDeleted, t1.CreateTime, t1.CreatorID, t1.CreatorName, t1.UpdateTime, t1.UpdaterID, t1.UpdaterName, t1.PublishOrgID, t1.PublishOrgName ");
            sql.Append(" from TD_Donation_Info t1 INNER JOIN #TEMP t2 ON t1.DonationID=t2.id");
            sql.Append(" drop table #TEMP");
            List<MySqlParameter> parameters = new List<MySqlParameter>();
			for (int i = 0; i < list.Count; i++)
            {
                MySqlParameter parameter = new MySqlParameter("@Parameter" + i.ToString(), list[i]);
                parameters.Add(parameter);
            }
			IList<ShiMiao.Model.TD_Donation_Info> modelList= new List<ShiMiao.Model.TD_Donation_Info>();
            using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(sql.ToString(), parameters.ToArray()))
			{
				while(dr.Read())
				{
					ShiMiao.Model.TD_Donation_Info model = new ShiMiao.Model.TD_Donation_Info();
										if (dr[0] != DBNull.Value)
            		{
						model.DonationID=																																										dr.GetString(0);												            		}
            							if (dr[1] != DBNull.Value)
            		{
						model.Title=																														dr.GetString(1);																								            		}
            							if (dr[2] != DBNull.Value)
            		{
						model.ImageURL=																														dr.GetString(2);																								            		}
            							if (dr[3] != DBNull.Value)
            		{
						model.Content=																														dr.GetString(3);																								            		}
            							if (dr[4] != DBNull.Value)
            		{
						model.ReadCount=dr.GetInt32(4);																																																						            		}
            							if (dr[5] != DBNull.Value)
            		{
						model.IsImage=																														dr.GetString(5);																								            		}
            							if (dr[6] != DBNull.Value)
            		{
						model.IsDeleted=																														dr.GetString(6);																								            		}
            							if (dr[7] != DBNull.Value)
            		{
						model.CreateTime=																		dr.GetDateTime(7);																																				            		}
            							if (dr[8] != DBNull.Value)
            		{
						model.CreatorID=																								dr.GetDecimal(8);																														            		}
            							if (dr[9] != DBNull.Value)
            		{
						model.CreatorName=																														dr.GetString(9);																								            		}
            							if (dr[10] != DBNull.Value)
            		{
						model.UpdateTime=																		dr.GetDateTime(10);																																				            		}
            							if (dr[11] != DBNull.Value)
            		{
						model.UpdaterID=																								dr.GetDecimal(11);																														            		}
            							if (dr[12] != DBNull.Value)
            		{
						model.UpdaterName=																														dr.GetString(12);																								            		}
            							if (dr[13] != DBNull.Value)
            		{
						model.PublishOrgID=																								dr.GetDecimal(13);																														            		}
            							if (dr[14] != DBNull.Value)
            		{
						model.PublishOrgName=																														dr.GetString(14);																								            		}
            		            		modelList.Add(model);
				}
				parameters.Clear();
				return modelList;
			}
		}
		
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShiMiao.Model.TD_Donation_Info GetModel(string DonationID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DonationID, Title, ImageURL, Content, ReadCount, IsImage, IsDeleted, CreateTime, CreatorID, CreatorName, UpdateTime, UpdaterID, UpdaterName, PublishOrgID, PublishOrgName  ");			
			strSql.Append("  from TD_Donation_Info ");
			strSql.Append(" where DonationID=@DonationID ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@DonationID", DonationID)            
              
                        };
			
			ShiMiao.Model.TD_Donation_Info model=null;
			using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
			{
				while(dr.Read())
				{
				model = new ShiMiao.Model.TD_Donation_Info();
				if (dr[0] != DBNull.Value)
            	{
            	model.DonationID=            																									dr.GetString(0);								            	}
	            if (dr[1] != DBNull.Value)
            	{
            	model.Title=            																	dr.GetString(1);																            	}
	            if (dr[2] != DBNull.Value)
            	{
            	model.ImageURL=            																	dr.GetString(2);																            	}
	            if (dr[3] != DBNull.Value)
            	{
            	model.Content=            																	dr.GetString(3);																            	}
	            if (dr[4] != DBNull.Value)
            	{
            	model.ReadCount=dr.GetInt32(4);            																																	            	}
	            if (dr[5] != DBNull.Value)
            	{
            	model.IsImage=            																	dr.GetString(5);																            	}
	            if (dr[6] != DBNull.Value)
            	{
            	model.IsDeleted=            																	dr.GetString(6);																            	}
	            if (dr[7] != DBNull.Value)
            	{
            	model.CreateTime=            									dr.GetDateTime(7);																								            	}
	            if (dr[8] != DBNull.Value)
            	{
            	model.CreatorID=            													dr.GetDecimal(8);																				            	}
	            if (dr[9] != DBNull.Value)
            	{
            	model.CreatorName=            																	dr.GetString(9);																            	}
	            if (dr[10] != DBNull.Value)
            	{
            	model.UpdateTime=            									dr.GetDateTime(10);																								            	}
	            if (dr[11] != DBNull.Value)
            	{
            	model.UpdaterID=            													dr.GetDecimal(11);																				            	}
	            if (dr[12] != DBNull.Value)
            	{
            	model.UpdaterName=            																	dr.GetString(12);																            	}
	            if (dr[13] != DBNull.Value)
            	{
            	model.PublishOrgID=            													dr.GetDecimal(13);																				            	}
	            if (dr[14] != DBNull.Value)
            	{
            	model.PublishOrgName=            																	dr.GetString(14);																            	}
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
			strSql.Append("select count(1) from TD_Donation_Info");
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
		public IList<ShiMiao.Model.TD_Donation_Info> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DonationID, Title, ImageURL, Content, ReadCount, IsImage, IsDeleted, CreateTime, CreatorID, CreatorName, UpdateTime, UpdaterID, UpdaterName, PublishOrgID, PublishOrgName  ");
			strSql.Append(" FROM TD_Donation_Info ");
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
			IList<ShiMiao.Model.TD_Donation_Info> list= new List<ShiMiao.Model.TD_Donation_Info>();
			using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
			{
				while(dr.Read())
				{
				ShiMiao.Model.TD_Donation_Info model = new ShiMiao.Model.TD_Donation_Info();
				if (dr[0] != DBNull.Value)
            	{
            	model.DonationID=            																									dr.GetString(0);								            	}
	            if (dr[1] != DBNull.Value)
            	{
            	model.Title=            																	dr.GetString(1);																            	}
	            if (dr[2] != DBNull.Value)
            	{
            	model.ImageURL=            																	dr.GetString(2);																            	}
	            if (dr[3] != DBNull.Value)
            	{
            	model.Content=            																	dr.GetString(3);																            	}
	            if (dr[4] != DBNull.Value)
            	{
            	model.ReadCount=dr.GetInt32(4);            																																	            	}
	            if (dr[5] != DBNull.Value)
            	{
            	model.IsImage=            																	dr.GetString(5);																            	}
	            if (dr[6] != DBNull.Value)
            	{
            	model.IsDeleted=            																	dr.GetString(6);																            	}
	            if (dr[7] != DBNull.Value)
            	{
            	model.CreateTime=            									dr.GetDateTime(7);																								            	}
	            if (dr[8] != DBNull.Value)
            	{
            	model.CreatorID=            													dr.GetDecimal(8);																				            	}
	            if (dr[9] != DBNull.Value)
            	{
            	model.CreatorName=            																	dr.GetString(9);																            	}
	            if (dr[10] != DBNull.Value)
            	{
            	model.UpdateTime=            									dr.GetDateTime(10);																								            	}
	            if (dr[11] != DBNull.Value)
            	{
            	model.UpdaterID=            													dr.GetDecimal(11);																				            	}
	            if (dr[12] != DBNull.Value)
            	{
            	model.UpdaterName=            																	dr.GetString(12);																            	}
	            if (dr[13] != DBNull.Value)
            	{
            	model.PublishOrgID=            													dr.GetDecimal(13);																				            	}
	            if (dr[14] != DBNull.Value)
            	{
            	model.PublishOrgName=            																	dr.GetString(14);																            	}
	            	            list.Add(model);
				}
				parameters.Clear();
				return list;
			}
		}
	}
}