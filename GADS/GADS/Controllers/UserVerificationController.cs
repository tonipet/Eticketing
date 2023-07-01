using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;

namespace GADS.Controllers
{
    public class UserVerificationController : Controller
    {

        UserVerificationDB verDB = new UserVerificationDB();
        // GET: UserVerification
        public ActionResult UserVerification()
        {
            return View();
        }
        public JsonResult VerifyAccount(UserVerification emp)
        {
            return Json(verDB.verifyAccount(emp), JsonRequestBehavior.AllowGet);
        }
      

    }

}