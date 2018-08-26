using BaiGuiXe_Smart_API.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using BaiGuiXe_Smart_API.Models.XacMinh;

namespace BaiGuiXe_Smart_API.Controllers
{
    public class VerificationController : Controller
    {
        XacMinh_Model xm_mod = new XacMinh_Model();
        UserModel useMod = new UserModel();
        // GET: Verification
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult XacNhanEmail(string code)
        {
            
            var xm = xm_mod.Find(code);
            if (xm != null)
            {
                ViewBag.ObjectId = code;
                var use = useMod.FindId(xm.Id_User);
                return View(use);
            }
            else
            {
                return View();
            }
        }
        
        public int Xacnhan(string code)
        {
            var x = xm_mod.Find(code);
            if (x != null)
            {
                if (sosanhngay(x.Timer,DateTime.Now) >=3)
                {
                    xm_mod.Delete(code);
                    return -1; // code hết hạn sử dụng
                }
                else
                {
                    var use = useMod.FindId(x.Id_User);
                    if (use != null)
                    {
                        UserController us = new UserController();


                        use.XacThucEmail = true;
                        useMod.Update(use);
                        xm_mod.Delete(code);
                        return 1; // thành công
                    }
                    else
                    {
                        return -2; // xác thực thành công
                    }
                   
                }

            }
            else
            {
                return 0; // code không tồn tại
            }
            
           
           
        }
        // hàm lấy gia trị thời gian + 1 day
        public int sosanhngay(DateTime d1 , DateTime d2)
        {
            DateTime ngay1 = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", d1.ToString()));
            DateTime ngay2 = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", d2.ToString()));
            TimeSpan Time = ngay2 - ngay1;
            int TongSoNgay = Time.Days;
            return TongSoNgay;

        }

        public void builEmail(User x)
        {
           var code = xm_mod.RandomString();
            XacMinh xm = new XacMinh();
            xm.Code = code;
            xm.Id_User = x.Id;
            xm.Timer = Convert.ToDateTime(String.Format("{0:yyyy-MM-dd}", DateTime.Now.ToString()));

            var flag = xm_mod.Create(xm);



            if (flag == 1)
            {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/" + "Email" + ".cshtml"));
                var url = "http://localhost:51332/" + "Verification/XacNhanEmail?code="+xm.Code;
                body = body.Replace("@ViewBag.linkxacnhan", url);
                body = body.ToString();
                builEmail("Thư xác minh !", body, x.Email);
            }
            else
            {
                
            }
        }

        public void builEmail(string title, string textmail, string emailfrom)
        {
            string from, to,bcc, cc, subject, body;
            from = "Duongsanght97@gmail.com";
            to = emailfrom;
            cc = "";
            bcc = "";
            subject = title;
            StringBuilder sd = new StringBuilder();
            sd.Append(textmail);
            body = sd.ToString();
            MailMessage ms = new MailMessage();
            ms.From = new MailAddress(from);
            ms.To.Add(new MailAddress(to));
            if(!string.IsNullOrEmpty(bcc))
            {
                ms.Bcc.Add(new MailAddress(bcc));

            }
            if(!string.IsNullOrEmpty(cc))
            {
                ms.Bcc.Add(new MailAddress(cc));
            }
            ms.Subject = subject;
            ms.Body = body;
            ms.IsBodyHtml = true;
            SendMail(ms);
        }

        public static void SendMail(MailMessage ms)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("skaudioshop@gmail.com", "sang@729708");
            try
            {
                client.Send(ms);
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}