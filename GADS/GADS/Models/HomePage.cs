using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GADS.Models
{
    public class HomePage
    {
        public int TicketNo { get; set; }
        public String TicketID { get; set; }
        public int TicketTypeCode { get; set; }
        public String TicketType { get; set; }
        public int DepartmentCode { get; set; }
        public String TicketStatus { get; set; }
        public String TargetCompletionDate { get; set; }
        public String AssignedTo { get; set; }
        public String AssignToCode { get; set; }
        public String BriefDescription { get; set; }
        public String CreatedDate { get; set; }
        public String DepartmentName { get; set; }
        public String AttachmentFile { get; set; }
        public String HtmlTextColor { get; set; }
        public int TicketStatusCode { get; set; }

        public String SubTicketType { get; set; }
        public String Dialogtags { get; set; }
        public String DialogID { get; set; }
        public String Dialog { get; set; }
        public String Customer { get; set; }
        public String Title { get; set; }



    }
}