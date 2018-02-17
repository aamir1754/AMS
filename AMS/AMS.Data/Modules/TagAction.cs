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
    public class TagAction : BaseActions<ams_equipments>
    {
        public override void Add(ams_equipments t)
        {
            ctx.ams_equipments.Add(t);
            Save();
        }

        public override void Change(ams_equipments t)
        {
            Save();
        }

        public override void Delete(int id)
        {
            ctx.ams_equipments.Where(x => x.equ_key == id).Delete();
            Save();
        }

        public override ams_equipments Get(int id)
        {
            return ctx.ams_equipments.First(x => x.equ_key == id);
        }

        public override IQueryable<ams_equipments> GetAll()
        {
            return ctx.ams_equipments;
        }

        public bool ValidateTagName(string TagTitle, int userId)
        {
            bool TitleAvailable = true;
            if(userId==0)
            {
                var data = ctx.ams_equipments.Where(x => x.equ_title == TagTitle).FirstOrDefault();
                if (data == null)
                {
                    TitleAvailable = false;
                }
            }
            return TitleAvailable;
        }

        public void AddTag(string TagTitle, int UserId)
        {
            ams_equipments tag = new ams_equipments();
            tag.equ_created_date = System.DateTime.Now;
            tag.equ_user_key = UserId;
            tag.equ_title = TagTitle;
            Add(tag);
        }

        public List<TagsDTO> GetTagsList()
        {
            return ctx.ams_equipments.Select(x=> new TagsDTO {
                TagId = x.equ_key,
                TagTitle = x.equ_title
            }).ToList();
        }
    }
}