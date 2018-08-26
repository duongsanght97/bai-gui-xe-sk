using System.Web.Mvc;

namespace BaiGuiXe_Smart_API.Areas.QuanTriVien
{
    public class QuanTriVienAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QuanTriVien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QuanTriVien_default",
                "QuanTriVien/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                 new[] { "BaiGuiXe_Smart_API.Areas.QuanTriVien.Controllers" }
            );
        }
    }
}