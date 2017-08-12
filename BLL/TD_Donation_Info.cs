using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using ShiMiao.Model;
namespace ShiMiao.BLL {
	    //TD_Donation_Info
	    public partial class TD_Donation_Info
	{
		private readonly ShiMiao.DAL.TD_Donation_Info dal= new ShiMiao.DAL.TD_Donation_Info();
		public TD_Donation_Info()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid DonationID)
		{
			return dal.Exists(DonationID);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid DonationID, string tranID)
		{
			return dal.Exists(DonationID,  tranID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Info model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShiMiao.Model.TD_Donation_Info model, string tranID)
		{
			return dal.Add(model, tranID);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Info model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ShiMiao.Model.TD_Donation_Info model, string tranID)
		{
			return dal.Update(model, tranID);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid DonationID)
		{
			
			return dal.Delete(DonationID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Guid DonationID, string tranID)
		{
			
			return dal.Delete(DonationID, tranID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(Guid DonationID)
		{
			
			return dal.DeleteLogic(DonationID);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteLogic(Guid DonationID, string tranID)
		{
			
			return dal.DeleteLogic(DonationID, tranID);
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
		public IList<ShiMiao.Model.TD_Donation_Info> GetList(IList<Guid> list)
		{
			return dal.GetList(list);
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShiMiao.Model.TD_Donation_Info GetModel(string DonationID)
		{			
			return dal.GetModel(DonationID);
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
		public IList<ShiMiao.Model.TD_Donation_Info> GetTopList(int top, string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetTopList(top,where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Info> GetList(string where, string orderBy, IDictionary<string, object> dictParams)
		{
			return dal.GetList(where,orderBy, dictParams);
		}

		/// <summary>
		/// 获取分页数据列表
		/// </summary>
		public IList<ShiMiao.Model.TD_Donation_Info> GetListByPage(string where, string orderby, IDictionary<string, object> dictParams, int startIndex, int pageSize)
		{
			return dal.GetListByPage(where, orderby, dictParams, startIndex, pageSize);
		}
		#endregion   
	}
}