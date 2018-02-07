using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AMS.Data.Model
{
    public partial class AMSEntities : DbContext
    {
        public AMSEntities(string cnnstr) : base(cnnstr) { }
    }
}