using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;

namespace GADS.Models
{
    public class RegistrationDB
    {
        EmailClass email = new EmailClass();
        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        String strPathAndQuery = HttpContext.Current.Request.Url.Authority;
        public Boolean AddUser(Registration user)
        {
            Boolean checkEmail = CheckEmailExist(user.Email);
            Boolean result = false;
            if (checkEmail == true)
                        {
                result = true;
                        }
            else {
              
                using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("Registration_Add_User", con);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@LastName", user.LastName);
                        com.Parameters.AddWithValue("@FirstName", user.FirstName);
                        com.Parameters.AddWithValue("@DivisionCode", user.DivisionCode);
                        com.Parameters.AddWithValue("@DepartmentCode", user.DepartmentCode);
                        com.Parameters.AddWithValue("@Position", user.Position);
                        com.Parameters.AddWithValue("@Email", user.Email);
                        com.Parameters.AddWithValue("@Password", user.Password);
                        com.ExecuteNonQuery();
                        result = false;
                        con.Close();
                        email.SendEmail( "GADS Verification link", "  <h5>Please click <a href='http://"+ strPathAndQuery + "/UserVerification/UserVerification?id=" + user.Email + "' >here</a> to verify your account. </h5>", user.Email);
                }
            }

            return result;
        }

        public static bool CheckEmailExist(string Email)
        {
            Boolean result = false;
            string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Registration_Check_EmailExist", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmailAdd", Email);
                SqlDataReader dr = com.ExecuteReader();
                while(dr.Read()){
                    if (Email == dr["Email"].ToString())
                    {
                        result = true;
                    }
                }
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

        public List<ListItem> getDepartMent(Registration user )
        {

            List<ListItem> DepartmentList = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("Registration_Select_DepartMent", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DivisionCode", user.DivisionCode);
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



        public List<ListItem> getPosition()
        {

            List<ListItem> Positionlist = new List<ListItem>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand com = new SqlCommand("Registration_Select_Position", con);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    {
                        Positionlist.Add(new ListItem()
                        {
                            Value = dr["DesignationCode"].ToString(),
                            Text = dr["DesignationName"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return Positionlist;
        }

    }
}