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
	    //微信支付订单记录
	public partial class TD_Order_WeiXinPay
	{
		public bool Exists(string OrderID,string NonceStr)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Order_WeiXinPay");
			strSql.Append(" where ");
			strSql.Append(" OrderID = @OrderID and  ");strSql.Append(" NonceStr = @NonceStr ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@OrderID", OrderID),		new MySqlParameter("@NonceStr", NonceStr)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(strSql.ToString(), parameters);
			return result != null;
		}
		
		public bool Exists(string OrderID,string NonceStr, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select 1 from TD_Order_WeiXinPay");
			strSql.Append(" where ");
			strSql.Append(" OrderID = @OrderID and  ");strSql.Append(" NonceStr = @NonceStr ");            
            MySqlParameter[] parameters = {
            	new MySqlParameter("@OrderID", OrderID),		new MySqlParameter("@NonceStr", NonceStr)		
			};
			object result = MySqlHelperUtil.ExecuteScalar(tranID, strSql.ToString(), parameters);
			return result != null;
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Order_WeiXinPay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Order_WeiXinPay(");			
            strSql.Append("OrderID,NonceStr,Timestamp,OrgID,WeiXinOrderID,OrderFee,CashFee,Package,Status,PayTime,CallBackTime");
			strSql.Append(") values (");
            strSql.Append("@OrderID,@NonceStr,@Timestamp,@OrgID,@WeiXinOrderID,@OrderFee,@CashFee,@Package,@Status,@PayTime,@CallBackTime");            
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID),            
                        new MySqlParameter("@NonceStr", model.NonceStr),            
                        new MySqlParameter("@Timestamp", model.Timestamp),            
                        new MySqlParameter("@OrgID", model.OrgID),            
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID),            
                        new MySqlParameter("@OrderFee", model.OrderFee),            
                        new MySqlParameter("@CashFee", model.CashFee),            
                        new MySqlParameter("@Package", model.Package),            
                        new MySqlParameter("@Status", model.Status),            
                        new MySqlParameter("@PayTime", model.PayTime),            
                        new MySqlParameter("@CallBackTime", model.CallBackTime)            
              
            };
			 return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);			
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Order_WeiXinPay model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TD_Order_WeiXinPay(");			
            strSql.Append("OrderID,NonceStr,Timestamp,OrgID,WeiXinOrderID,OrderFee,CashFee,Package,Status,PayTime,CallBackTime");
			strSql.Append(") values (");
            strSql.Append("@OrderID,@NonceStr,@Timestamp,@OrgID,@WeiXinOrderID,@OrderFee,@CashFee,@Package,@Status,@PayTime,@CallBackTime");            
            strSql.Append(") ");            
            		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID),            
                        new MySqlParameter("@NonceStr", model.NonceStr),            
                        new MySqlParameter("@Timestamp", model.Timestamp),            
                        new MySqlParameter("@OrgID", model.OrgID),            
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID),            
                        new MySqlParameter("@OrderFee", model.OrderFee),            
                        new MySqlParameter("@CashFee", model.CashFee),            
                        new MySqlParameter("@Package", model.Package),            
                        new MySqlParameter("@Status", model.Status),            
                        new MySqlParameter("@PayTime", model.PayTime),            
                        new MySqlParameter("@CallBackTime", model.CallBackTime)            
              
            };
			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Order_WeiXinPay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Order_WeiXinPay set ");
			                        
            strSql.Append(" OrderID = @OrderID , ");                                    
            strSql.Append(" NonceStr = @NonceStr , ");                                    
            strSql.Append(" Timestamp = @Timestamp , ");                                    
            strSql.Append(" OrgID = @OrgID , ");                                    
            strSql.Append(" WeiXinOrderID = @WeiXinOrderID , ");                                    
            strSql.Append(" OrderFee = @OrderFee , ");                                    
            strSql.Append(" CashFee = @CashFee , ");                                    
            strSql.Append(" Package = @Package , ");                                    
            strSql.Append(" Status = @Status , ");                                    
            strSql.Append(" PayTime = @PayTime , ");                                    
            strSql.Append(" CallBackTime = @CallBackTime  ");            			
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID) ,            
                        new MySqlParameter("@NonceStr", model.NonceStr) ,            
                        new MySqlParameter("@Timestamp", model.Timestamp) ,            
                        new MySqlParameter("@OrgID", model.OrgID) ,            
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID) ,            
                        new MySqlParameter("@OrderFee", model.OrderFee) ,            
                        new MySqlParameter("@CashFee", model.CashFee) ,            
                        new MySqlParameter("@Package", model.Package) ,            
                        new MySqlParameter("@Status", model.Status) ,            
                        new MySqlParameter("@PayTime", model.PayTime) ,            
                        new MySqlParameter("@CallBackTime", model.CallBackTime)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Order_WeiXinPay model, string tranID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Order_WeiXinPay set ");
			                        
            strSql.Append(" OrderID = @OrderID , ");                                    
            strSql.Append(" NonceStr = @NonceStr , ");                                    
            strSql.Append(" Timestamp = @Timestamp , ");                                    
            strSql.Append(" OrgID = @OrgID , ");                                    
            strSql.Append(" WeiXinOrderID = @WeiXinOrderID , ");                                    
            strSql.Append(" OrderFee = @OrderFee , ");                                    
            strSql.Append(" CashFee = @CashFee , ");                                    
            strSql.Append(" Package = @Package , ");                                    
            strSql.Append(" Status = @Status , ");                                    
            strSql.Append(" PayTime = @PayTime , ");                                    
            strSql.Append(" CallBackTime = @CallBackTime  ");            			
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr  ");
						
			MySqlParameter[] parameters = {
			            new MySqlParameter("@OrderID", model.OrderID) ,            
                        new MySqlParameter("@NonceStr", model.NonceStr) ,            
                        new MySqlParameter("@Timestamp", model.Timestamp) ,            
                        new MySqlParameter("@OrgID", model.OrgID) ,            
                        new MySqlParameter("@WeiXinOrderID", model.WeiXinOrderID) ,            
                        new MySqlParameter("@OrderFee", model.OrderFee) ,            
                        new MySqlParameter("@CashFee", model.CashFee) ,            
                        new MySqlParameter("@Package", model.Package) ,            
                        new MySqlParameter("@Status", model.Status) ,            
                        new MySqlParameter("@PayTime", model.PayTime) ,            
                        new MySqlParameter("@CallBackTime", model.CallBackTime)             
              
            };
            
            return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string OrderID,string NonceStr)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Order_WeiXinPay ");
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID),            
              
                                    new MySqlParameter("@NonceStr", NonceStr)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string OrderID,string NonceStr, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TD_Order_WeiXinPay ");
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID),            
              
                                    new MySqlParameter("@NonceStr", NonceStr)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(string OrderID,string NonceStr)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Order_WeiXinPay set IsDeleted='1'");
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID),            
              
                                    new MySqlParameter("@NonceStr", NonceStr)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 软删除一条数据
		/// </summary>
		public int DeleteLogic(string OrderID,string NonceStr, string tranID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TD_Order_WeiXinPay set IsDeleted='1'");
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID),            
              
                                    new MySqlParameter("@NonceStr", NonceStr)            
              
                        };

			return MySqlHelperUtil.ExecuteNonQuery(tranID, strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShiMiao.Model.TD_Order_WeiXinPay GetModel(string OrderID,string NonceStr)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderID, NonceStr, Timestamp, OrgID, WeiXinOrderID, OrderFee, CashFee, Package, Status, PayTime, CallBackTime  ");			
			strSql.Append("  from TD_Order_WeiXinPay ");
			strSql.Append(" where OrderID=@OrderID and NonceStr=@NonceStr ");
			MySqlParameter[] parameters = {
			                        new MySqlParameter("@OrderID", OrderID),            
              
                                    new MySqlParameter("@NonceStr", NonceStr)            
              
                        };
			
			ShiMiao.Model.TD_Order_WeiXinPay model=null;
			using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters))
			{
				while(dr.Read())
				{
				model = new ShiMiao.Model.TD_Order_WeiXinPay();
				if (dr[0] != DBNull.Value)
            	{
            	model.OrderID=            																	dr.GetString(0);																            	}
	            if (dr[1] != DBNull.Value)
            	{
            	model.NonceStr=            																	dr.GetString(1);																            	}
	            if (dr[2] != DBNull.Value)
            	{
            	model.Timestamp=dr.GetInt32(2);            																																	            	}
	            if (dr[3] != DBNull.Value)
            	{
            	model.OrgID=            													dr.GetInt32(3);																				            	}
	            if (dr[4] != DBNull.Value)
            	{
            	model.WeiXinOrderID=            																	dr.GetString(4);																            	}
	            if (dr[5] != DBNull.Value)
            	{
            	model.OrderFee=dr.GetInt32(5);            																																	            	}
	            if (dr[6] != DBNull.Value)
            	{
            	model.CashFee=dr.GetInt32(6);            																																	            	}
	            if (dr[7] != DBNull.Value)
            	{
            	model.Package=            																	dr.GetString(7);																            	}
	            if (dr[8] != DBNull.Value)
            	{
            	model.Status=dr.GetInt32(8);            																																	            	}
	            if (dr[9] != DBNull.Value)
            	{
            	model.PayTime=            									dr.GetDateTime(9);																								            	}
	            if (dr[10] != DBNull.Value)
            	{
            	model.CallBackTime=            									dr.GetDateTime(10);																								            	}
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
			strSql.Append("select count(1) from TD_Order_WeiXinPay");
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
		public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderID, NonceStr, Timestamp, OrgID, WeiXinOrderID, OrderFee, CashFee, Package, Status, PayTime, CallBackTime  ");
			strSql.Append(" FROM TD_Order_WeiXinPay ");
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
			IList<ShiMiao.Model.TD_Order_WeiXinPay> list= new List<ShiMiao.Model.TD_Order_WeiXinPay>();
			using (DbDataReader dr = MySqlHelperUtil.ExecuteReader(strSql.ToString(), parameters.ToArray()))
			{
				while(dr.Read())
				{
				ShiMiao.Model.TD_Order_WeiXinPay model = new ShiMiao.Model.TD_Order_WeiXinPay();
				if (dr[0] != DBNull.Value)
            	{
            	model.OrderID=            																	dr.GetString(0);																            	}
	            if (dr[1] != DBNull.Value)
            	{
            	model.NonceStr=            																	dr.GetString(1);																            	}
	            if (dr[2] != DBNull.Value)
            	{
            	model.Timestamp=dr.GetInt32(2);            																																	            	}
	            if (dr[3] != DBNull.Value)
            	{
            	model.OrgID=            													dr.GetInt32(3);																				            	}
	            if (dr[4] != DBNull.Value)
            	{
            	model.WeiXinOrderID=            																	dr.GetString(4);																            	}
	            if (dr[5] != DBNull.Value)
            	{
            	model.OrderFee=dr.GetInt32(5);            																																	            	}
	            if (dr[6] != DBNull.Value)
            	{
            	model.CashFee=dr.GetInt32(6);            																																	            	}
	            if (dr[7] != DBNull.Value)
            	{
            	model.Package=            																	dr.GetString(7);																            	}
	            if (dr[8] != DBNull.Value)
            	{
            	model.Status=dr.GetInt32(8);            																																	            	}
	            if (dr[9] != DBNull.Value)
            	{
            	model.PayTime=            									dr.GetDateTime(9);																								            	}
	            if (dr[10] != DBNull.Value)
            	{
            	model.CallBackTime=            									dr.GetDateTime(10);																								            	}
	            	            list.Add(model);
				}
				parameters.Clear();
				return list;
			}
		}
	}
}