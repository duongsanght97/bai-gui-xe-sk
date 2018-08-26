using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiGuiXe_Smart_API.Models
{
    public class MaHoa
    {
        public string md5(string pass)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(pass);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str.ToLower();
        }
    }
}