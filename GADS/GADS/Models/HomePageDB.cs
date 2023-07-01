using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GADS.Models
{
    public class HomePageDB
    {
        commonfunctionsDB commomfunc = new commonfunctionsDB();

        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        
        public List<HomePage> ListAll()
        {
          
            List<HomePage> Details = new List<HomePage>();
            using (SqlConnection con = new SqlConnection(connectionstring))
                  {
                con.Open();
                string usercode;
                usercode = HttpContext.Current.Session["Usercode"].ToString();
                SqlCommand com = new SqlCommand("TicketingSystem_Select_Ticket", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@USERCODE", usercode );
                com.Parameters.AddWithValue("@DepartmentCode", HttpContext.Current.Session["DepartmentCode"].ToString());
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Details.Add(new HomePage
                    {
                        TicketNo = Convert.ToInt32(rdr["TicketNo"]),
                        TicketID = rdr["TicketID"].ToString(),
                        DepartmentName = rdr["DepartmentName"].ToString(),
                        TicketType = rdr["TicketType"].ToString(),
                        BriefDescription = rdr["BriefDescription"].ToString(),
                        TicketStatus = rdr["TicketStatus"].ToString(),
                        CreatedDate = rdr["CreatedDateTime"].ToString(),
                        TargetCompletionDate = rdr["TargetCompletionDate"].ToString(),
                        AssignedTo = rdr["AssignedTo"].ToString(),
                        AssignToCode= rdr["AssignToCode"].ToString(),
                        Dialogtags= rdr["Dialogtags"].ToString(),
                        DepartmentCode = Convert.ToInt32(rdr["DepartmentCode"]),
                        TicketTypeCode = Convert.ToInt32(rdr["TicketTypeCode"]),
                        AttachmentFile = rdr["AttachmentFile"].ToString(),
                        HtmlTextColor = rdr["HtmlTextColor"].ToString(),
                        Title = rdr["Title"].ToString(),
                        SubTicketType = rdr["SubTicketType"].ToString(),
                        TicketStatusCode= Convert.ToInt32(rdr["TicketStatusCode"]),
                        Customer = rdr["Customer"].ToString()


                    });

                }

                    con.Close();
            }
                return Details;
        }


       
       

        public Boolean AddNewTicket(HomePage Info)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
              
                string filename;
                if (Info.AttachmentFile != null)
                {
                    filename = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Info.AttachmentFile;
                }
                else
                {
                    filename = "";
                }
                con.Open();
                SqlCommand com = new SqlCommand("TicketingSystem_AddNew_Ticket", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TicketTypeCode", Info.TicketTypeCode);
                com.Parameters.AddWithValue("@BriefDescription", Info.BriefDescription );
                com.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["Usercode"].ToString());
                com.Parameters.AddWithValue("@DepartmentCode",Info.DepartmentCode);
                com.Parameters.AddWithValue("@AttachmentFile", filename);
                com.Parameters.AddWithValue("@Title", Info.Title);
                com.Parameters.AddWithValue("@SubTicketType", Info.SubTicketType);
                
                com.ExecuteNonQuery();
                result = true;
                con.Close();
            }
            return result;
        }
        public Boolean UpdateTicket(HomePage Info)
        {
            Boolean result;

          
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                
                con.Open();
                string filename;
                if (Info.AttachmentFile != null)
                {
                    filename = DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Info.AttachmentFile;
                }else
                {
                    filename = "";
                }

                SqlCommand com = new SqlCommand("TicketingSystem_Update_Ticket", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TicketNo", Info.TicketNo);
                com.Parameters.AddWithValue("@TicketTypeCode ", Info.TicketTypeCode);
                com.Parameters.AddWithValue("@DepartmentCode", Info.DepartmentCode);
                com.Parameters.AddWithValue("@AttachmentFile", filename);
                com.Parameters.AddWithValue("@BriefDescription", Info.BriefDescription);
                com.Parameters.AddWithValue("@Title", Info.Title);
                com.Parameters.AddWithValue("@SubTicketType", Info.SubTicketType);
                com.ExecuteNonQuery();
                result = true;
                con.Close();
            }
            return result;
        }


        


        public List<ListItem> getDepartmentPerUser()
        {
            return  commomfunc.getDepartmentPerUser(HttpContext.Current.Session["DivisionCode"].ToString());
          
        }

        public List<ListItem> SubTicketType(HomePage Info)
        {
            List<ListItem> SubTicketTypeList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("TicketingSystem_Select_SubTicketType", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@TICKETTYPECODE", Info.TicketTypeCode);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    {
                        SubTicketTypeList.Add(new ListItem()
                        {
                            Value = dr["TickettypeCode"].ToString(),
                            Text = dr["TickettypeCode"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return SubTicketTypeList;
        }

        public List<ListItem> getStickertype()
        {
            return commomfunc.getStickertype();
       
        }
        public Boolean DeleteTicket(int ID)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Delete_Ticket]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                com.ExecuteNonQuery();
                result = true;
            }
            return result;
        }

        public List<HomePage> DialogDetails(HomePage info)
        {
            List<HomePage> Details = new List<HomePage>();
     
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_DialogDetails]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DialogID", info.DialogID);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Details.Add(new HomePage
                    {
                        DialogID = rdr["DialogID"].ToString(),
                        Dialog = rdr["Dialog"].ToString()
                    });
                }

                con.Close();
            }
            return Details;
                
        }


        public Boolean UpdateDialogDetails(HomePage Info)
        {
            Boolean result;


            using (SqlConnection con = new SqlConnection(connectionstring))
            {


                con.Open();
                SqlCommand com = new SqlCommand("TicketingSystem_Update_DialogDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DialogID", Info.DialogID);
                com.Parameters.AddWithValue("@Dialog", Info.Dialog);
           
                com.ExecuteNonQuery();
                result = true;
                con.Close();
            }
            return result;
        }
    }
}