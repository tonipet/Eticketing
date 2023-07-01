using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;

namespace GADS.Controllers
{
    public class LoginController : Controller
    {
        LoginDB DBLogin = new LoginDB();
        Login DBLogin1 = new Login();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
         public JsonResult UserLogin(Login emp)
        {

            return Json(DBLogin.LoginUser(emp), JsonRequestBehavior.AllowGet);
        }

    }
}