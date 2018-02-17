using AMS.Data.Actions;
using AMS.Data.Model;
using AMS.DataTransferObjects.Modules;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Data.Modules
{
    public class PartAction : BaseActions<ams_parts>
    {
        public override void Add(ams_parts t)
        {
            ctx.ams_parts.Add(t);
            Save();
        }

        public override void Change(ams_parts t)
        {
            Save();
        }

        public override void Delete(int id)
        {
            ctx.ams_parts.Where(x => x.prt_key == id).Delete();
            Save();
        }

        public override ams_parts Get(int id)
        {
            return ctx.ams_parts.First(x => x.prt_key == id);
        }

        public override IQueryable<ams_parts> GetAll()
        {
            return ctx.ams_parts;
        }

        public bool ValidatePartName(string TagTitle, int userId)
        {
            bool TitleAvailable = true;
            if (userId == 0)
            {
                var data = ctx.ams_parts.Where(x => x.prt_title == TagTitle).FirstOrDefault();
                if (data == null)
                {
                    TitleAvailable = false;
                }
            }
            return TitleAvailable;
        }

        public void AddPart(string PartTitle, int UserId, int TagId)
        {
            ams_parts tag = new ams_parts();
            tag.prt_created_date = System.DateTime.Now;
            tag.prt_equ_key = TagId;
            tag.prt_title = PartTitle;
            tag.prt_added_by = UserId;
            Add(tag);
        }

        public List<PartsDTo> GetPartsList(int TagId)
        {
            return ctx.ams_parts.Where(x=>x.prt_equ_key==TagId).Select(x => new PartsDTo
            {
                PartId = x.prt_key,
                PartName = x.prt_title
            }).ToList();
        }
    }
}