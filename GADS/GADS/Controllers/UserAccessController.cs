using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GADS.Models;

namespace GADS.Controllers
{
    public class UserAccessController : Controller
    {

        CommonController CommonController = new CommonController();

        UserAccessDB UserAccessDB  = new UserAccessDB();


        // GET: UserAccess
        public ActionResult Index()
        {
            return CommonController.CommonViewBagValue();
        }

         public ActionResult AddStatus()
        {
            return CommonController.CommonViewBagValue();
        }
        public ActionResult AddGroup()
        {
            return CommonController.CommonViewBagValue();
        }


        public JsonResult Userlist()
        {
            return Json(UserAccessDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUsers(int ID)
        {
            return Json(UserAccessDB.DeleteUsers(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AccessList()
        {
            return Json(UserAccessDB.getAccess(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AllDeptList()
        {
            return Json(UserAccessDB.GetAllDept(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserbyID(int ID)
        {
            var UserInfo = UserAccessDB.ListAll().Find(x => x.Usercode.Equals(ID));
            return Json(UserInfo, JsonRequestBehavior.AllowGet);
        }
     


        public JsonResult UpdateUsers(UserAccess info)
        {
            return Json(UserAccessDB.UpdateUsers(info), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatus(int ID)
        {
            var UserInfo = UserAccessDB.ListAllStatus().Find(x => x.UserAccessCode.Equals(ID));
            return Json(UserInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserStatuslist()
        {
            return Json(UserAccessDB.ListAllStatus(), JsonRequestBehavior.AllowGet);
         
        }

        public JsonResult GetMasterStatus()
        {
            return Json(UserAccessDB.getMasterStatus(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateUserAccess(UserAccess info)
        {
            return Json(UserAccessDB.UpdateUserAccess(info), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUserAccess(UserAccess info)
        {
            return Json(UserAccessDB.AddUserAccess(info), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupsbyID(int ID)
        {
           
            var GroupInfo = UserAccessDB.getAccess().Find(x => int.Parse(x.Value).Equals(ID));
            return Json(GroupInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUserGroup(UserAccess info)
        {
            return Json(UserAccessDB.UpdateUserGroup(info), JsonRequestBehavior.AllowGet);
        }
        



    }
    }


