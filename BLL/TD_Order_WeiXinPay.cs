using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using ShiMiao.Model;
namespace ShiMiao.BLL {
	    //微信支付订单记录
	    public partial class TD_Order_WeiXinPay
	{
		private readonly ShiMiao.DAL.TD_Order_WeiXinPay dal= new ShiMiao.DAL.TD_Order_WeiXinPay();
		public TD_Order_WeiXinPay()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderID,string NonceStr)
		{
			return dal.Exists(OrderID,NonceStr);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderID,string NonceStr, string tranID)
		{
			return dal.Exists(OrderID,NonceStr,  tranID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Order_WeiXinPay model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Order_WeiXinPay model, string tranID)
		{
			return dal.Add(model, tranID);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Order_WeiXinPay model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Order_WeiXinPay model, string tranID)
		{
			return dal.Update(model, tranID);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string OrderID,string NonceStr)
		{
			
			return dal.Delete(OrderID,NonceStr);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string OrderID,string NonceStr, string tranID)
		{
			
			return dal.Delete(OrderID,NonceStr, tranID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(string OrderID,string NonceStr)
		{
			
			return dal.DeleteLogic(OrderID,NonceStr);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(string OrderID,string NonceStr, string tranID)
		{
			
			return dal.DeleteLogic(OrderID,NonceStr, tranID);
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShiMiao.Model.TD_Order_WeiXinPay GetModel(string OrderID,string NonceStr)
		{			
			return dal.GetModel(OrderID,NonceStr);
		}
		
		/// <summary>
		/// 获取记录条数
		/// </summary>
		public int GetRecordCount(string strWhere, IDictionary<string, object> dictParams)
		{
			return dal.GetRecordCount(strWhere, dictParams);
		}
		
		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetTopList(top,where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetList(where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取分页数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Order_WeiXinPay> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
		{
			return dal.GetListByPage(where, orderby, dictParams, startIndex, pageSize);
		}
		#endregion   
	}
}