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

    public class OptionsAction : BaseActions<ams_sub_parts>
    {
        public override void Add(ams_sub_parts t)
        {
            ctx.ams_sub_parts.Add(t);
            Save();
        }

        public override void Change(ams_sub_parts t)
        {
            Save();
        }

        public override void Delete(int id)
        {
            ctx.ams_sub_parts.Where(x => x.spt_key == id).Delete();
            Save();
        }

        public override ams_sub_parts Get(int id)
        {
            return ctx.ams_sub_parts.First(x => x.spt_key == id);
        }

        public override IQueryable<ams_sub_parts> GetAll()
        {
            return ctx.ams_sub_parts;
        }

        public bool ValidateOptionName(string OptionTitle, int PartId)
        {
            bool TitleAvailable = true;
            var data = ctx.ams_sub_parts.Where(x => x.spt_title == OptionTitle && x.spt_prt_key== PartId).FirstOrDefault();
            if (data == null)
            {
                TitleAvailable = false;
            }
            return TitleAvailable;
        }

        public void AdOption(string OptionTitle, int UserId, int PartId)
        {
            ams_sub_parts part = new ams_sub_parts();
            part.spt_title = OptionTitle;
            part.spt_prt_key = PartId;
            Add(part);
        }

        public List<OptionsDTO> GetPartsList(int OptionId)
        {
            return ctx.ams_sub_parts.Where(x => x.spt_prt_key == OptionId).Select(x => new OptionsDTO
            {
                OptionId = x.spt_key,
                OptionTitle = x.spt_title
            }).ToList();
        }
    }
}