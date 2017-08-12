using ShiMiao.WebCommon;
using Stone.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiMiao.Web.Controllers
{
    public class ManageController : MiniWebBaseController
    {
        private static readonly BLL.TD_WeiXin_Menu menuBLL = new BLL.TD_WeiXin_Menu();
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateMenu()
        {
            string where = string.Format("OrgID={0}", 1);
            IList<Model.TD_WeiXin_Menu> list = menuBLL.GetList(where, null, null);
            WeiXinMenus menus = GetTree(list);
            WeiXinPort port = new WeiXinPort();
            string message = port.CreateMenu(menus);
            if (string.IsNullOrEmpty(message))
            {
                return GetSucceedResult(1, "发布成功");
            }
            else
            {
                return GetErrorResult(message);
            }
        }

        private WeiXinMenus GetTree(IList<Model.TD_WeiXin_Menu> allList)
        {
            WeiXinMenus menus = new WeiXinMenus();
            IList<Model.TD_WeiXin_Menu> list = allList.Where((model) => { return model.ParentID == 1; }).OrderBy((x) => { return x.OrderIndex; }).ToList();
            foreach (Model.TD_WeiXin_Menu model in list)
            {
                allList.Remove(model);
                if (model.MenuValue == string.Empty)
                {
                    model.MenuValue = null;
                }
                WeiXinMenu menu = null;
                if (model.MenuType == 1
                    || model.MenuType == 0)
                {
                    menu = new WeiXinMenu(model.MenuName, ButtonType.click, model.MenuValue);
                }
                else if (model.MenuType == 2)
                {
                    menu = new WeiXinMenu(model.MenuName, ButtonType.view, model.MenuValue);
                }
                menus.button.Add(menu);
                GetSubTree(allList, model, menu);
            }
            return menus;
        }

        private void GetSubTree(IList<Model.TD_WeiXin_Menu> allList, Model.TD_WeiXin_Menu parentModel, WeiXinMenu parentMenu)
        {
            IList<Model.TD_WeiXin_Menu> list = allList.Where((model) => { return model.ParentID == parentModel.MenuID; }).OrderBy((x) => { return x.OrderIndex; }).ToList();
            if (list.Count > 0)
            {
                parentMenu.sub_button = new List<WeiXinMenu>();
            }
            foreach (Model.TD_WeiXin_Menu model in list)
            {
                allList.Remove(model);
                if (model.MenuValue == string.Empty)
                {
                    model.MenuValue = null;
                }
                WeiXinMenu menu = null;
                if (model.MenuType == 1
                    || model.MenuType == 0)
                {
                    menu = new WeiXinMenu(model.MenuName, ButtonType.click, model.MenuValue);
                }
                else if (model.MenuType == 2)
                {
                    menu = new WeiXinMenu(model.MenuName, ButtonType.view, model.MenuValue);
                }
                parentMenu.sub_button.Add(menu);
                GetSubTree(allList, model, menu);
            }
        }
    }
}