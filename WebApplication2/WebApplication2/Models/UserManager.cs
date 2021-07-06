using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class UserManager
    {
        Model1 db = new Model1();
        public Boolean checkUserName(string userName)
        {
            List<User> users = db.Users.ToList();

            var ketqua = (from user in users
                          where user.UserName == userName
                          select user).ToList();

            if (ketqua.Count > 0)
            {
                return false;
            }
            return true;
        }
        public Boolean checkEmail(string email)
        {
            List<User> users = db.Users.ToList();

            var ketqua = (from user in users
                          where user.Email == email
                          select user).ToList();

            if (ketqua.Count > 0)
            {
                return false;
            }
            return true;
        }



        public Boolean checkUser(string password, string userName)
        {
            List<User> users = db.Users.ToList();
            var Password = getMD5Hash(password);
            var ketqua = (from user in users
                          where (user.UserName == userName || user.Email == userName) && user.Password == Password
                          select user).ToList();

            if (ketqua.Count > 0)
            {
                return true;
            }


            return false;
        }
        public string getMD5Hash(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(password);
                b = md5.ComputeHash(b);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte x in b)
                    sb.Append(x.ToString("x2"));
                return sb.ToString();
            }

        }
    }
}