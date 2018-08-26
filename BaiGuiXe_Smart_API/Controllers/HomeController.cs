using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGuiXe_Smart_API.Models;
using BaiGuiXe_Smart_API.Models.Slide;

namespace BaiGuiXe_Smart_API.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //UserModel useMol = new UserModel();
            //var use = useMol.FindAll();
         
                return View();
        
        }
        public ActionResult LoadSlide()
        {
            SlideModel slmod = new SlideModel();
            LinkSlide_Model llmodel = new LinkSlide_Model();
            List<ListLink> linklist = llmodel.FindAll();
            List<Slide> listsl = slmod.FindAll();
            ViewBag.list_model = linklist;
            return PartialView(listsl);
        }
    }
}
