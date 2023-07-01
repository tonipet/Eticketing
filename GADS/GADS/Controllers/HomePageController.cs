using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;
using System.IO;

namespace GADS.Controllers
{
    public class HomePageController : Controller
    {
        HomePageDB DBHomepage = new HomePageDB();
        CommonController CommonController = new CommonController();

       
        public ActionResult HomePage()
        {
          
            //var IssuesList = new List<System.Collections.IList>();
            //IssuesList.Add(new listP<string>() { "a", "b" });
        
            return CommonController.CommonViewBagValue();
        }
        // GET: HomePage
        [HttpPost]
        public JsonResult getDepartmentPerUser()
        {
            return Json(DBHomepage.getDepartmentPerUser(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getStickertype()
        {
            return Json(DBHomepage.getStickertype(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult saveTicket(HomePage info)
        {
            return Json(DBHomepage.AddNewTicket(info), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTicket(HomePage info)
        {
            return Json(DBHomepage.UpdateTicket(info), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateDialogDetails(HomePage info)
        {
            return Json(DBHomepage.UpdateDialogDetails(info), JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult TicketList()
        {
            return Json(DBHomepage.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubTicketType(HomePage info)
        {
            return Json(DBHomepage.SubTicketType(info), JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult CreateTicket()
        {
            return CommonController.CommonViewBagValue();
        }

        public JsonResult GetbyID(int ID)
        {
            var TicketInfo = DBHomepage.ListAll().Find(x => x.TicketNo.Equals(ID));
            return Json(TicketInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTicket(int ID)
        {
            return Json(DBHomepage.DeleteTicket(ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DialogByID(HomePage info)
        {
            return Json(DBHomepage.DialogDetails(info), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            string path = Server.MapPath("~/Shared/uploads/");
            HttpFileCollectionBase files = Request.Files;
        
            for (int i = 0; i < files.Count; i++)
            {
                string result;
                HttpPostedFileBase file = files[i];
                result = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(path + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Path.GetFileNameWithoutExtension(file.FileName) +Path.GetExtension(file.FileName));
            }
            return Json(files.Count + " Files Uploaded!");
        }

      
        public FileResult DownloadFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Shared/Forms/"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }


        public FileResult uploadedFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Shared/uploads/"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }

    }

}

  



