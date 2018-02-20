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

    public class DocumentAction : BaseActions<ams_documents>
    {
        public override void Add(ams_documents t)
        {
            ctx.ams_documents.Add(t);
            Save();
        }

        public override void Change(ams_documents t)
        {
            Save();
        }

        public override void Delete(int id)
        {
            ctx.ams_sub_parts.Where(x => x.spt_key == id).Delete();
            Save();
        }

        public override ams_documents Get(int id)
        {
            return ctx.ams_documents.First(x => x.doc_key == id);
        }

        public override IQueryable<ams_documents> GetAll()
        {
            return ctx.ams_documents;
        }

        public List<DocumentsDTO> GetDocuments(int UserId)
        {
            return ctx.ams_documents.Where(x => x.doc_created_by == UserId).Select(x => new DocumentsDTO
            {
                DocumentId = x.doc_key,
                DocumentTitle = x.doc_title
            }).ToList();
        }

        public void AddDocument(int userId, string fileName, string fileExtension)
        {
            ams_documents document = new ams_documents();
            document.doc_title = fileName;
            document.doc_type = fileExtension;
            document.doc_created_date = System.DateTime.Now;
            document.doc_created_by = userId;
            Add(document);
        }
    }
}