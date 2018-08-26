using BaiGuiXe_Smart_API.Models;
using BaiGuiXe_Smart_API.Models.UserSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiGuiXe_Smart_API.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession)Session["loginsession"];
            
            if (session != null && session.XacThucEmail == false)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Verification", Action = "XacNhanEmail" }));

            }
            if(session != null && session.LoaiTaiKhoan == -1)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Verification", Action = "TaiKhoanBiKhoa" }));
            }

            base.OnActionExecuting(filterContext);
        }

    }
}