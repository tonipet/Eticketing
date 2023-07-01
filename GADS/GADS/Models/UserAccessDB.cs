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
 
    public class UserAccessDB
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
  
        public List<UserAccess> ListAll()
        {
            List<UserAccess> Userlist = new List<UserAccess>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                SqlCommand com = new SqlCommand("TicketingSystem_Select_User", con);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Userlist.Add(new UserAccess
                    {
                        Usercode = Convert.ToInt32(rdr["Usercode"]),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        DivisionCode = rdr["DivisionCode"].ToString(),
                        DivisionName = rdr["DivisionName"].ToString(),
                        DepartmentCode = rdr["DepartmentCode"].ToString(),
                        DepartmentName = rdr["DepartmentName"].ToString(),
                        Position = rdr["Position"].ToString(),
                        DesignationName = rdr["DesignationName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        UserAccessCode = Convert.ToInt32(rdr["UserAccessCode"]),
                       Access = rdr["Access"].ToString()

                    });

                    }
                    con.Close();
            }


            return Userlist;

        }

        public Boolean DeleteUsers(int ID)
        {
            Boolean result = false;

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Delete_User]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id",ID);
                com.ExecuteNonQuery();
                result = true;

            }

            return result;
     

        }
        public List<ListItem> getAccess()
        {

            List<ListItem> AccessList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_Access]", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    {
                        AccessList.Add(new ListItem()
                        {
                            Value = dr["UserAccessCode"].ToString(),
                            Text = dr["UserAccess"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return AccessList;
        }

        public Boolean UpdateUsers(UserAccess Info)
        {
            Boolean result;


            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                con.Open();
                SqlCommand com = new SqlCommand("TicketingSystem_Update_User", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@LastName", Info.LastName);
                com.Parameters.AddWithValue("@FirstName ", Info.FirstName);
                com.Parameters.AddWithValue("@DivisionCode", Info.DivisionCode);
                com.Parameters.AddWithValue("@DepartmentCode", Info.DepartmentCode);
                com.Parameters.AddWithValue("@Position", Info.Position);
                com.Parameters.AddWithValue("@Email", Info.Email);
                com.Parameters.AddWithValue("@UserAccessCode", Info.UserAccessCode);
                com.Parameters.AddWithValue("@Usercode", Info.Usercode);
                com.ExecuteNonQuery();
                result = true;
            }
            return result;
        }


        public List<ListItem> GetAllDept()
        {

            List<ListItem> DepartmentList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_ALL_Department]", con);
                com.CommandType = CommandType.StoredProcedure;
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

        public List<UserAccess> ListAllStatus()
        {
            List<UserAccess> Userlist = new List<UserAccess>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                SqlCommand com = new SqlCommand("[TicketingSystem_Select_UserAccess]", con);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Userlist.Add(new UserAccess
                    {
                        UserAccessCode = Convert.ToInt32(rdr["UserAccessCode"]),
                        Access = rdr["UserAccess"].ToString(),
                        TicketStatusCode = rdr["TicketStatusCode"].ToString(),
                        StatusList = rdr["StatusList"].ToString()
                    });

                }
                con.Close();
            }


            return Userlist;

        }
        public List<ListItem> getMasterStatus()
        {

            List<ListItem> AccessList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("[TicketingSystem_Select_Master_Status]", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    {
                        AccessList.Add(new ListItem()
                        {
                            Value = dr["TicketStatusCode"].ToString(),
                            Text = dr["TicketStatus"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return AccessList;
        }

        public Boolean UpdateUserAccess(UserAccess info)
        {
            Boolean result = false;
                using(SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Update_UserAccess]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserAccessCode", info.UserAccessCode);
                com.Parameters.AddWithValue("@UserAccess", info.Access);
                com.Parameters.AddWithValue("@TicketStatusCode", info.StatusList);
                com.ExecuteNonQuery();
                con.Close();
                result = true;

            }




            return result;

        }
        public Boolean AddUserAccess(UserAccess info)
        {
            Boolean result;
            String Userexist="";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Insert_UserAccess]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserAccess", info.Access);
                com.Parameters.AddWithValue("@TicketStatusCode", info.StatusList);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    Userexist = rdr["Userexist"].ToString();
                   
                }

                con.Close();
            }
            if (Userexist.ToUpper() == "EXIST")
            {
                result = false;
            }else
            {
                result = true;
            }

            return result ;


        }


        public Boolean UpdateUserGroup(UserAccess info)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("[TicketingSystem_Update_UserGroup]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserAccessCode", info.UserAccessCode);
                com.Parameters.AddWithValue("@UserAccess", info.Access);
                com.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }

        



    }


}