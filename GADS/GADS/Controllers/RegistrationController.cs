using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;

namespace GADS.Controllers
{
    public class RegistrationController : Controller
    {
        RegistrationDB RegDB = new RegistrationDB();

        // GET: Registration
        public ActionResult Registration()
        {
            return View();
        }
        public JsonResult AddUser(Registration emp)
        {
            return Json(RegDB.AddUser(emp), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getDivision()
        {
            return Json(RegDB.getDivision(), JsonRequestBehavior.AllowGet);
        }
 
        public JsonResult getDeparment(Registration emp )
        {
            return Json(RegDB.getDepartMent(emp), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPosition()
        {
            return Json(RegDB.getPosition(), JsonRequestBehavior.AllowGet);
        }
    }
}