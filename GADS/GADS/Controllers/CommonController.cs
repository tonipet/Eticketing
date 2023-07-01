using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;
using System.Web.Security;

namespace GADS.Controllers
{
    public class CommonController : Controller
    {
        commonfunctionsDB commonfunc = new commonfunctionsDB();

        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getstatus()
        {
            return Json(commonfunc.getTickteStatus(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAssignto()
        {
            return Json(commonfunc.getAssignto(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDialoglist(commonfunctions info)
        {
            return Json(commonfunc.GetDialoglist(info), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDialogAttachment(commonfunctions info)
        {
            return Json(commonfunc.GetDialogAttachment(info), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getTags(commonfunctions info)
        {
            return Json(commonfunc.getTags(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult saveDialog(commonfunctions info)
        {
            return Json(commonfunc.addNewDialog(info), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAttachement(commonfunctions info)
        {
            return Json(commonfunc.DeleteAttachmentDetails(info), JsonRequestBehavior.AllowGet);
        }





        public ActionResult CommonViewBagValue()
        {
            try

            {

                ViewBag.UserName = System.Web.HttpContext.Current.Session["FirstName"].ToString() + " " + System.Web.HttpContext.Current.Session["LastName"].ToString();
                ViewBag.GetSubMenu = commonfunc.GetSubMenu();
                ViewBag.GetMenu = commonfunc.GetMenu();
                return View();
            }
            catch
            {
             
            
                return RedirectToAction("login", "login");

            }
        }



    }
}