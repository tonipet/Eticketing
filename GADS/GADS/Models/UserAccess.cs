using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GADS.Models
{
    public class UserAccess
    {
        public int Usercode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public string DesignationName { get; set; }
        public string Email { get; set; }
        public int UserAccessCode { get; set; }
        public string Access { get; set; }
        public string TicketStatusCode { get; set; }
        public string StatusList { get; set; }

    }
}