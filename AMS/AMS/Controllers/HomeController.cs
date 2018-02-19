using AMS.Data;
using AMS.DataTransferObjects.CookieHelpers;
using AMS.DataTransferObjects.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CookieHelper.CreateUserCookie("Admin",1, "common.jpg", true);
            return View();
        }

        public PartialViewResult GetPartial(string PartialViewName)
        {
            return PartialView(PartialViewName);
        }

        [HttpPost]
        public ActionResult AddTag(string Title)
        {
            try
            {
                CookieVM cookieData = CookieHelper.GetAllValues();
                int UserId = cookieData.UserId;
                string message = "";
                bool isValid = DataAccess.Instance.TagActions.ValidateTagName(Title, 0);

                if (isValid)
                {
                    message = "tag with the same name is already exists";
                    return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DataAccess.Instance.TagActions.AddTag(Title, UserId);
                    message = "Tag added successfully.";
                    return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _GetTags()
        {
            return PartialView(DataAccess.Instance.TagActions.GetTagsList());
        }

        [HttpPost]
        public ActionResult DeleteTag(int TagId)
        {
            try
            {
                DataAccess.Instance.TagActions.Delete(TagId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = "Can not delete tag, it has parts" }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GetPartsPartial()
        {
            List<TagsDTO> List = DataAccess.Instance.TagActions.GetAll().Select(x => new TagsDTO
            {
                TagId = x.equ_key,
                TagTitle = x.equ_title
            }).ToList();

            List.Insert(0, new TagsDTO {
                TagId = 0,
                TagTitle = "Select Tag"
            });
            ViewBag.Tags = new SelectList(List, "TagId", "TagTitle");
            return PartialView("_Parts");
        }


        [HttpPost]
        public ActionResult AddNewPart(string Title, int TagId)
        {
            try
            {
                CookieVM cookieData = CookieHelper.GetAllValues();
                int UserId = cookieData.UserId;
                string message = "";
                bool isValid = DataAccess.Instance.PartActions.ValidatePartName(Title, 0);

                if (isValid)
                {
                    message = "Part with the same name already exists";
                    return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DataAccess.Instance.PartActions.AddPart(Title, UserId, TagId);
                    message = "Part added successfully.";
                    return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeletePart(int PartId)
        {
            try
            {
                int[] OptionsList = DataAccess.Instance.OptionsActions.GetAll().Where(x => x.spt_prt_key == PartId).Select(x=>x.spt_key).ToArray();
                foreach (var item in OptionsList)
                {
                    DataAccess.Instance.OptionsActions.Delete(item);
                }
                DataAccess.Instance.PartActions.Delete(PartId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message =ee }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GetTagParts(int TagId)
        {
            return PartialView("_GetPartsList",DataAccess.Instance.PartActions.GetPartsList(TagId));
        }

        [HttpPost]
        public ActionResult AddNewOption(string Title, int PartId)
        {
            try
            {
                CookieVM cookieData = CookieHelper.GetAllValues();
                int UserId = cookieData.UserId;
                string message = "";
                DataAccess.Instance.OptionsActions.AdOption(Title, UserId, PartId);
                message = "Option added successfully.";
                return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
                //bool isValid = DataAccess.Instance.OptionsActions.ValidateOptionName(Title, PartId);

                //if (isValid)
                //{
                //    message = "Option with the same name already exists";
                //    return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    DataAccess.Instance.OptionsActions.AdOption(Title, UserId, PartId);
                //    message = "Option added successfully.";
                //    return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteOption(int OptionId)
        {
            try
            {
                DataAccess.Instance.OptionsActions.Delete(OptionId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _GetOptions(int PartId)
        {
            return PartialView(DataAccess.Instance.OptionsActions.GetPartsList(PartId));
        }

        public PartialViewResult _GetDocuments()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddDocument(HttpPostedFileBase txtDocumentName)
        {
            try
            {
                string FileName = System.IO.Path.GetFileNameWithoutExtension(txtDocumentName.FileName);
                string FileExtension = System.IO.Path.GetExtension(txtDocumentName.FileName);
                int UserId = CookieHelper.GetUserId();
                string FilePath = Server.MapPath("~/Content/Documents/" + txtDocumentName.FileName);
                txtDocumentName.SaveAs(FilePath);
                DataAccess.Instance.DocumentActions.AddDocument(UserId, FileName, FileExtension);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _GetDocumentsList()
        {
            return PartialView(DataAccess.Instance.DocumentActions.GetDocuments(CookieHelper.GetUserId()));
        }

        [HttpPost]
        public ActionResult DeleteDocument(int DocumentId)
        {
            try
            {
                DataAccess.Instance.DocumentActions.Delete(DocumentId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult DownloadDocument(int DocumentId)
        {
            try
            {
                var Document = DataAccess.Instance.DocumentActions.Get(DocumentId);
                string FileName = Document.doc_title + Document.doc_type;
                string path = Server.MapPath("~/Content/Documents/"+ FileName);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
            }
            catch (Exception)
            {
                return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, "Error");
            }
        }


        public PartialViewResult _GetItemsPartial()
        {
            List<TagsDTO> List = DataAccess.Instance.TagActions.GetAll().Select(x => new TagsDTO
            {
                TagId = x.equ_key,
                TagTitle = x.equ_title
            }).ToList();

            List.Insert(0, new TagsDTO
            {
                TagId = 0,
                TagTitle = "Select Tag"
            });
            ViewBag.Tags = new SelectList(List, "TagId", "TagTitle");
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetPartsWithOptions(int TagId)
        {
            try
            {
                return Json(new { success = true, data = DataAccess.Instance.PartActions.GetPartsWithOptions(TagId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, message = ee }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}