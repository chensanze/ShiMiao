using ShiMiao.Common;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShiMiao.WebCommon
{
    public class MiniWebBaseController : Controller
    {
        public ActionResult GotoErrorResult(string message)
        {
            return new RedirectToRouteResult("Default",
                                                 new RouteValueDictionary(new
                                                 {
                                                     controller = "Home",
                                                     action = "Error",
                                                     message = message
                                                 }));
        }

        public ActionResult GotoDeletedResultInMiniWeb()
        {
            return new RedirectToRouteResult("Default",
                                                 new RouteValueDictionary(new
                                                 {
                                                     controller = "Home",
                                                     action = "Deleted",
                                                     url = "/Home/Index"
                                                 }));
        }

        protected JsonResult GetErrorResult(string errorMessage)
        {
            return Json(new 
            {
                statusCode = (int)Constants.StatusCode.Failed,
                message = errorMessage
            });
        }

        protected virtual JsonResult GetSucceedResult(object data, string message)
        {
            return Json(new 
            {
                statusCode = (int)Constants.StatusCode.Succeed,
                Data = data,
                message = message
            });
        }

        public int PageIndex { private set; get; }

        public int PageSize { private set; get; }

        public int PageCount { private set; get; }

        public void InitPager(int? recordCount)
        {
            int pageSize = Constants.Pages.PageSize;
            InitPager(recordCount, pageSize);
        }

        public void InitPager(int? recordCount, int pageSize)
        {
            int pageIndex = 1;
            string command = "none";
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                pageSize = int.Parse(DESEncrypt.Decrypt(Request["pageSize"], DESEncrypt.Keys.SplitPage));
            }
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = int.Parse(DESEncrypt.Decrypt(Request["pageIndex"], DESEncrypt.Keys.SplitPage));
                if (!string.IsNullOrEmpty(Request["pagercommand"]))
                {
                    command = Request["pagercommand"].Trim();
                }
            }
            PageCount = (int)Math.Ceiling((decimal)recordCount / pageSize);
            if (pageIndex > PageCount)
            {
                pageIndex = PageCount;
            }
            else if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            switch (command)
            {
                case "first":
                    pageIndex = 1;
                    break;
                case "prev":
                    pageIndex--;
                    break;
                case "next":
                    pageIndex++;
                    break;
                case "last":
                    if (recordCount.HasValue)
                    {
                        pageIndex = PageCount;
                    }
                    break;
            }
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public JsonResult GetSucceedListResult(object list, int recordCount)
        {
            return GetSucceedResult(new
            {
                list = list,
                RecordCount = recordCount,
                PageCount = PageCount,
                PageIndex = (recordCount > 0 ? PageIndex : 0),
                EnPageCount = DESEncrypt.Encrypt(PageCount.ToString(), DESEncrypt.Keys.SplitPage),
                EnPageIndex = DESEncrypt.Encrypt(PageIndex.ToString(), DESEncrypt.Keys.SplitPage),
                HasPrev = (PageIndex > 1),
                HasNext = (PageIndex < PageCount),
            }, "");
        }
    }
}
