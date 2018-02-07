using AMS.Data.Core;
using AMS.Data.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Data
{
    public class DataAccess
    {
        internal Model.AMSEntities _ctx = null;
        static DataAccess self = null;
        public object ModulePermission;

        private DataAccess()
        {
            var tmp = new ConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            _ctx = new Model.AMSEntities(tmp.AMSString);

            // temporarily dissabling the EF entity validation - it should be removed in the next code sync.
            _ctx.Configuration.ValidateOnSaveEnabled = false;
        }
        public static DataAccess Instance
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    const string kApplicationSettings = "ApplicationObject";
                    if (context != null && context.Items[kApplicationSettings] != null)
                    {
                        var da = context.Items[kApplicationSettings] as DataAccess;
                        return da;
                    }

                    self = new DataAccess();
                    context.Items[kApplicationSettings] = self;
                }
                else
                {
                    self = new DataAccess();
                }
                return self;
            }
        }
        public void Save()
        {
            _ctx.SaveChanges();
        }
        internal Model.AMSEntities Ctx { get { return _ctx; } }
        public void Dispose()
        {
            if (self != null)
            {
                const string kApplicationSettings = "ApplicationObject";
                var context = HttpContext.Current;
                if (context != null && context.Items[kApplicationSettings] != null)
                    context.Items[kApplicationSettings] = null;
                _ctx.Dispose();
                _ctx = null;
                GC.SuppressFinalize(this);
                self = null;
            }
        }

        public Model.AMSEntities Context
        {
            get
            {
                return this._ctx;
            }
        }
        public UserAction UsersActions
        {
            get
            {
                UserAction Ans = new UserAction();
                Ans.SetContainer(this);
                return Ans;
            }
        }
    }
}