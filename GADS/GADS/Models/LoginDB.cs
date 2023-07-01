using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GADS.Models
{
       
    public class LoginDB
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
   
        //public string[] LoginUser(Login emp)
        public string[] LoginUser(Login emp)
        {


            string[] loginCredentials = new string[5];
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                try
                {
                    SqlCommand com = new SqlCommand("Login_Check_User", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmailAddress", emp.inputEmail);
                com.Parameters.AddWithValue("@Password", emp.inputPassword);
                con.Open();
              
                SqlDataReader dr = com.ExecuteReader();
             
                while (dr.Read())
                {
                    {
                        HttpContext.Current.Session["Usercode"] = dr["Usercode"].ToString();
                        HttpContext.Current.Session["LastName"] = dr["LastName"].ToString();
                        HttpContext.Current.Session["FirstName"] = dr["FirstName"].ToString();
                        HttpContext.Current.Session["DivisionCode"] = dr["DivisionCode"].ToString();
                        HttpContext.Current.Session["DepartmentCode"] = dr["DepartmentCode"].ToString();
                        HttpContext.Current.Session["Position"] = dr["Position"].ToString();
                        HttpContext.Current.Session["Email"] = dr["Email"].ToString();
                        HttpContext.Current.Session["Password"] = dr["Password"].ToString();
                        HttpContext.Current.Session["VerifiedDate"] = dr["VerifiedDate"].ToString();
                        HttpContext.Current.Session["CreateDateTime"] = dr["CreateDateTime"].ToString();
                        HttpContext.Current.Session["UserAccessCode"] = dr["UserAccessCode"].ToString();
                        }
                        if (HttpContext.Current.Session["VerifiedDate"].ToString() == "")
                        {
                            loginCredentials[0] ="0";
                            loginCredentials[1] = "NotVerified";
                        }
                        else
                        {
                            loginCredentials[0] = "0";
                            loginCredentials[2] = "Verified";
                       
                        }

                         
                    }
                }catch (Exception ex)
                {
                    loginCredentials[0] = "";
                }
              
                con.Close();
           



            }
                return loginCredentials;
        }



    }


  
}