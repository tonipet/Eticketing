using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GADS.Models
{
 
    public class Registration
    {
        public int Usercode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int DivisionCode { get; set; }
        public int DepartmentCode { get; set; }
        public int Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}