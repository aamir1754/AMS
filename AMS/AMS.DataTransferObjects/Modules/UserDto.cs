using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.DataTransferObjects.Modules
{
    public class UserDto
    {
        public int user_key { get; set; }

        public int ID { get; set; }
        public string username { get; set; }

        public string firstname { get; set; }
        public string FullName { get; set; }

        public string lastname { get; set; }
    }
}