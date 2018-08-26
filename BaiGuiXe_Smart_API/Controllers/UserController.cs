using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiGuiXe_Smart_API.Models;
using BaiGuiXe_Smart_API.Models.GoogleAPI;
using BaiGuiXe_Smart_API.Models.UserSession;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace BaiGuiXe_Smart_API.Controllers
{
    public class UserController : BaseController
    {
        UserModel usemol = new UserModel();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexHome()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }
        public int checklogin(string use , string password)
        {
           
            if(use == "" || password == "")
            {
                return 0;
            }
            else
            {
                MaHoa mh = new MaHoa();
                var x = mh.md5(password);
                var user_result = usemol.login(use, x);
                if(user_result != null)
                {
                    
                    //SessionHelper.SetSession(new UserSession() { Ten = user_result.Ten , Id = user_result.Id.ToString(), Email = user_result.Email,LoaiTaiKhoan = user_result.LoaiTaiKhoan, });
                    
                
                    if(user_result.XacThucEmail != true)
                    {
                        VerificationController vs = new VerificationController();
                        vs.builEmail(user_result);
                        return -2;
                    }
                    else
                    {
                        var usesession = new UserSession();
                        usesession.Id = user_result.Id.ToString();
                        usesession.Ten = user_result.Ten;
                        usesession.Email = user_result.Email;
                        usesession.LoaiTaiKhoan = user_result.LoaiTaiKhoan;
                        usesession.XacThucEmail = user_result.XacThucEmail;
                        SessionHelper.SetSession(usesession);
                        return 1;
                    }
                    
                }
                else
                {
                    return -1;
                }
            }

            //return Content("xác nhận","text/plain");
        }
        public ActionResult Logout()
        {
            Session["loginsession"] = null;
            return RedirectToAction("Index", "Home");
            

        }

        [HttpGet]
        public ActionResult Registration()
        {
            var session = (BaiGuiXe_Smart_API.Models.UserSession.UserSession)Session["loginsession"];
            if (session != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(FormCollection f)
        {
            
            if(usemol.checkemail(f["txtemail"]) == true)
            {
                CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
                if (response.Success && ModelState.IsValid)
                {
                    User use = new User();
                    use.Email = f["txtemail"];
                    use.HoTenDem = f["txthotendem"];
                    use.Ten = f["txtten"].ToString();
                    use.NgaySinh = Convert.ToDateTime(String.Format("{0:yyyy-MM-dd}", f["txtngaysinh"]));
                    use.LoaiTaiKhoan = 0;
                  
                    use.XacThucEmail = false;
                    use.GioiTinh = Convert.ToBoolean(Convert.ToInt32(f["txtgioitinh"]));

                    use.DiaChi = f["txtdiachi"];
                    use.SDT = f["txtdienthoai"];
                    if(string.Compare(f["txtmatkhau"],f["txtnhaplaimatkhau"]) == 0)
                    {
                        MaHoa mh = new MaHoa();
                        use.PassWord = mh.md5(f["txtmatkhau"]);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không trùng khớp , vui lòng kiểm tra lại !");
                        return View();
                    }
                    
                   
                    use.ThoiGianDangKy = Convert.ToDateTime(String.Format("{0:yyyy-MM-dd}", DateTime.Now.ToString()));
                    try
                    {
                        usemol.Create(use);
                        if (checklogin(use.Email, f["txtmatkhau"]) == 1)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng Ký thành công ! truy cập email của bạn để kích hoạt tài khoản ");
                            return View();
                        }
                        
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Có lỗi xảy ra , vui lòng thử lại sau !");
                        return View();
                    }
                    
                }
                else
                {
                    
                    ModelState.AddModelError("", "Bạn phải xác thực Captcha");
                    return View();

                }
            }
            else
            {
                ModelState.AddModelError("", "Email đã tồn tại");
                return View();
            }
            
            
            
            
        }

        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }

     

    }
}