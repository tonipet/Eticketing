using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GADS.Models
{
    public class commonfunctionsDB
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        public List<ListItem> getDepartmentPerUser(string DivisionCode)
        {

            List<ListItem> DepartmentList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("Registration_Select_DepartMent", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DivisionCode", DivisionCode);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        DepartmentList.Add(new ListItem()
                        {
                            Value = dr["DepartmentCode"].ToString(),
                            Text = dr["DepartmentName"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return DepartmentList;
        }


        public List<ListItem> getTickteStatus()
        {

            List<ListItem> StatusList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("TicketingSystem_Select_Status", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserAccessCode", HttpContext.Current.Session["UserAccessCode"].ToString());
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        StatusList.Add(new ListItem()
                        {
                            Value = dr["TicketStatusCode"].ToString(),
                            Text = dr["TicketStatus"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return StatusList;
        }

        public List<ListItem> getTags()
        {

            List<ListItem> TagsList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("TicketingSystem_Select_Tags", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DepartmentCode", HttpContext.Current.Session["DepartmentCode"].ToString());
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        TagsList.Add(new ListItem()
                        {
                            Value = dr["TicketID"].ToString(),
                            Text = dr["TicketID"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return TagsList;
        }



        public List<ListItem> getStickertype()
        {

            List<ListItem> StickerList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("Select_Sticker_Type", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    {
                        StickerList.Add(new ListItem()
                        {
                            Value = dr["TicketTypeCode"].ToString(),
                            Text = dr["TicketType"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return StickerList;
        }

        public List<ListItem> getAssignto()
        {

            List<ListItem> getAssigntoList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_AssignTo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DivisionCode", HttpContext.Current.Session["DivisionCode"].ToString());
                com.Parameters.AddWithValue("@DepartmentCode", HttpContext.Current.Session["DepartmentCode"].ToString());
                com.Parameters.AddWithValue("@UserAccessCode", HttpContext.Current.Session["UserAccessCode"].ToString());
                con.Open();


                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        getAssigntoList.Add(new ListItem()
                        {
                            Value = dr["Usercode"].ToString(),
                            Text = dr["FullName"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return getAssigntoList;
        }


        public List<commonfunctions> GetSubMenu()
        {

            List<commonfunctions> GetSubMenuList = new List<commonfunctions>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_SubMenu]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Usercode", HttpContext.Current.Session["Usercode"].ToString());
                com.Parameters.AddWithValue("@UserAccessCode", HttpContext.Current.Session["UserAccessCode"].ToString());
                con.Open();


                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        GetSubMenuList.Add(new commonfunctions()
                        {
                            MenuCode = dr["MenuCode"].ToString(),
                            SubMenu = dr["SubMenu"].ToString(),
                            SubMenuName = dr["SubMenuName"].ToString(),
                            PageLink = dr["PageLink"].ToString()


                        });
                    }
                }
                con.Close();
            }
            return GetSubMenuList;
        }


        public List<commonfunctions> GetMenu()
        {

            List<commonfunctions> GetMenuList = new List<commonfunctions>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_Menu]", con);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        GetMenuList.Add(new commonfunctions()
                        {
                            MenuCode = dr["MenuCode"].ToString(),
                            MenuDescription = dr["MenuDescription"].ToString(),
                        });
                    }
                }
                con.Close();
            }
            return GetMenuList;
        }

        
        public List<commonfunctions> GetDialogAttachment(commonfunctions Info)
        {
            List<commonfunctions> GetDialogAttachment = new List<commonfunctions>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_Dialog_Attachment]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TicketNo", Info.TicketNo);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        GetDialogAttachment.Add(new commonfunctions()
                        {
                            AttachementID = dr["AttachementID"].ToString(),
                            AttachmentFile = dr["AttachmentFile"].ToString()
                        });
                    }
                }


                con.Close();

                return GetDialogAttachment;
                }
        }


        public List<commonfunctions> GetDialoglist(commonfunctions Info)
        {
            List<commonfunctions> GetDialoglist = new List<commonfunctions>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_Dialog]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TicketNo", Info.TicketNo);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        GetDialoglist.Add(new commonfunctions()
                        {
                            Dialog = dr["Dialog"].ToString(),
                            DialogID = dr["DialogID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            ConvoDateExp = dr["ConvoDateExp"].ToString(),
                            Editable = dr["Editable"].ToString()
                        });
                    }
                }


                con.Close();

                return GetDialoglist;
            }

        }

        public Boolean addNewDialog(commonfunctions Info)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string filename;
                if (Info.AttachmentFile != " ")
                {
                    filename = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Info.AttachmentFile;
                }
                else
                {
                    filename = "";
                }
                con.Open();
                SqlCommand com = new SqlCommand("TicketingSystem_Add_Dialog", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TICKETNO", Info.TicketNo);
                com.Parameters.AddWithValue("@USERCODE", HttpContext.Current.Session["Usercode"].ToString());
                com.Parameters.AddWithValue("@TargetCompletionDate",Info.TargetCompletionDate );
                com.Parameters.AddWithValue("@TicketStatusCode", Info.TicketStatusCode);
                com.Parameters.AddWithValue("@AssignedTo", Info.AssignedTo);
                com.Parameters.AddWithValue("@DIALOGMSG", Info.DIALOGMSG);
                com.Parameters.AddWithValue("@TAGS", Info.TAGS);
                com.Parameters.AddWithValue("@Attachment", filename);
                com.Parameters.AddWithValue("@TicketTypeCode", Info.TicketTypeCode);
                com.Parameters.AddWithValue("@SubTicketType", Info.SubTicketType);
                com.ExecuteNonQuery();
                result = true;
                con.Close();
            }
            return result;
        }
        public List<ListItem> getDivision()
        {

            List<ListItem> Devisionlist = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("Registration_Select_Division", con);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    {
                        Devisionlist.Add(new ListItem()
                        {
                            Value = dr["DivisionCode"].ToString(),
                            Text = dr["DivisionName"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return Devisionlist;
        }

        public Boolean DeleteAttachmentDetails(commonfunctions Info)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Delete_Attachment]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@AttachmentID", Info.AttachementID);
                com.Parameters.AddWithValue("@Usercode", HttpContext.Current.Session["Usercode"].ToString());
                com.Parameters.AddWithValue("@TICKETNO", Info.TicketNo);
                com.ExecuteNonQuery();
                result = true;
            }
            return result;
        }



    }




}