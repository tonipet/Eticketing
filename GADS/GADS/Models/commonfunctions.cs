using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GADS.Models
{
    public class commonfunctions
    {
        public string SubMenu { get; set; }
        public string MenuCode { get; set; }
        public string SubMenuName { get; set; }
        public string MenuDescription { get; set; }
        public string PageLink { get; set; }
        public string TicketNo { get; set; }
        public string CreatedDate { get; set; }
        public string Dialog { get; set; }
    
        public string FullName { get; set; }
        public string USERCODE { get; set; }
        public string TargetCompletionDate { get; set; }
        public string TAGS { get; set; }
        public string TicketStatusCode { get; set; }
        public string AssignedTo { get; set; }
        public string DIALOGMSG { get; set; }
        public string DialogID { get; set; }
        public string AttachementID { get; set; }
        public string AttachmentFile { get; set; }

        public string ConvoDateExp { get; set; }

        public string Editable { get; set;}
        public string TicketTypeCode { get; set; }
        public string SubTicketType { get; set; }

    }


}