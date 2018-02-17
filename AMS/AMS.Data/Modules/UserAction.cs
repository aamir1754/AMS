using AMS.Data.Actions;
using AMS.Data.Model;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Data.Modules
{
    public class UserAction : BaseActions<ams_users>
    {
        public override void Add(ams_users t)
        {
            ctx.ams_users.Add(t);
            Save();
        }

        public override void Change(ams_users t)
        {
            Save();
        }

        public override void Delete(int id)
        {
            ctx.ams_users.Where(x => x.user_key == id).Delete();
            Save();
        }

        public override ams_users Get(int id)
        {
            return ctx.ams_users.First(x => x.user_key == id);
        }
       
        public override IQueryable<ams_users> GetAll()
        {
            return ctx.ams_users;
        }
    }
}