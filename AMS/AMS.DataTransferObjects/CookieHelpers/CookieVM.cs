using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.DataTransferObjects.CookieHelpers
{
    public class CookieVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public bool UserIsAdmin { get; set; }
    }
}