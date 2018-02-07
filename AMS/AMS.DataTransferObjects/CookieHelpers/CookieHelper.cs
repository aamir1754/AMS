using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.DataTransferObjects.CookieHelpers
{
    public static class CookieHelper
    {
        public static void CreateUserCookie(string UserName, int UserId, string UserImage, bool UserIsAdmin)
        {
            HttpCookie cookie = new HttpCookie("userInfo");
            cookie.Values["UserName"] = UserName;
            cookie.Values["UserId"] = UserId.ToString();
            cookie.Values["UserImage"] = UserImage;
            cookie.Values["UserIsAdmin"] = UserIsAdmin ? bool.TrueString : bool.FalseString;
            cookie.Values["UserCookieDate"] = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void DeleteCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userInfo"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static CookieVM GetAllValues()
        {
            CookieVM cookieModel = new CookieVM();
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userInfo"];
            if (cookie != null)
            {
                cookieModel.UserId = Convert.ToInt32(cookie["UserId"].ToString());
                cookieModel.UserName = cookie["UserName"].ToString();
                cookieModel.UserIsAdmin = cookie["UserIsAdmin"].ToString() == "True" ? true : false;
                cookieModel.UserImage = cookie["UserImage"].ToString();
            }

            return cookieModel;
        }

        public static bool IsAdmin()
        {
            bool isAdmin = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userInfo"];

            if (cookie != null)
            {
                isAdmin = cookie["UserIsAdmin"].ToString() == "True" ? true : false;
            }

            return isAdmin;
        }

        public static void SetIsAdmin(bool IsAdmin)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies["userInfo"];
            if (cookie != null)
            {
                CookieVM cookiePrevData = new CookieVM();
                cookiePrevData = GetAllValues();

                CreateUserCookie(cookiePrevData.UserName,
                    cookiePrevData.UserId,
                    cookiePrevData.UserImage,
                    IsAdmin);
            }

        }


        public static void UpdateCookie(string Firstname)
        {
            CookieVM cookieModel = GetAllValues();

            HttpCookie cookie = new HttpCookie("userInfo");
            cookie.Values["UserName"] = Firstname;
            cookie.Values["UserId"] = cookieModel.UserId.ToString();
            cookie.Values["UserImage"] = cookieModel.UserImage;
            cookie.Values["UserIsAdmin"] = cookieModel.UserIsAdmin ? bool.TrueString : bool.FalseString;
            cookie.Values["UserCookieDate"] = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void UpdateCookie(string Firstname, string lastName)
        {
            CookieVM cookieModel = GetAllValues();

            HttpCookie cookie = new HttpCookie("userInfo");
            cookie.Values["UserName"] = Firstname+" "+ lastName;
            cookie.Values["UserId"] = cookieModel.UserId.ToString();
            cookie.Values["UserImage"] = cookieModel.UserImage;
            cookie.Values["UserIsAdmin"] = cookieModel.UserIsAdmin ? bool.TrueString : bool.FalseString;
            cookie.Values["UserCookieDate"] = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        public static int GetUserId()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userInfo"];
            if (cookie != null)
            {
                return Convert.ToInt32(cookie["UserId"].ToString());
            }

            return 0;
        }

    }
}