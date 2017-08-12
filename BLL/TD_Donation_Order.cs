using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using ShiMiao.Model;
namespace ShiMiao.BLL {
	    //TD_Donation_Order
	    public partial class TD_Donation_Order
	{
		private readonly ShiMiao.DAL.TD_Donation_Order dal= new ShiMiao.DAL.TD_Donation_Order();
		public TD_Donation_Order()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid OrderID)
		{
			return dal.Exists(OrderID);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid OrderID, string tranID)
		{
			return dal.Exists(OrderID,  tranID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Order model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Order model, string tranID)
		{
			return dal.Add(model, tranID);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Order model)
		{
			return dal.Update(model);
		}

        public IList<decimal> GetCount(string where, IDictionary<string, object> dict)
        {
            return dal.GetCount(where, dict);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ShiMiao.Model.TD_Donation_Order model, string tranID)
		{
			return dal.Update(model, tranID);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid OrderID)
		{
			
			return dal.Delete(OrderID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid OrderID, string tranID)
		{
			
			return dal.Delete(OrderID, tranID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(Guid OrderID)
		{
			
			return dal.DeleteLogic(OrderID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(Guid OrderID, string tranID)
		{
			
			return dal.DeleteLogic(OrderID, tranID);
		}
		
				
		/// <summary>
		/// 增加阅读数
		/// </summary>
		public int AddReadCount(Guid id)
		{
			return dal.AddReadCount(id);
		}
		
		/// <summary>
		/// 批量软删除一批数据
		/// </summary>
		public int DeleteLogic(IList<Guid> list)
		{
			return dal.DeleteLogic(list);
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int Delete(IList<Guid> list)
		{
			return dal.Delete(list);
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int DeletePhysical(IList<Guid> list)
		{
			return dal.DeletePhysical(list);
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int SetEnabled(IList<Guid> list)
		{
			return dal.SetEnabled(list);
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public int SetDisabled(IList<Guid> list)
		{
			return dal.SetDisabled(list);
		}
		
		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Order> GetList(IList<Guid> list)
		{
			return dal.GetList(list);
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShiMiao.Model.TD_Donation_Order GetModel(string OrderID)
		{			
			return dal.GetModel(OrderID);
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
		public IList<ShiMiao.Model.TD_Donation_Order> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetTopList(top,where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Order> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetList(where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取分页数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Order> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
		{
			return dal.GetListByPage(where, orderby, dictParams, startIndex, pageSize);
		}
		#endregion   
	}
}