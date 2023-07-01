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
    public class UserVerificationDB
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public Boolean verifyAccount(UserVerification emp)
        {
            Boolean result = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Login_VerifyUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", emp.verifyEmail);
                com.ExecuteNonQuery();
                result = true;
                con.Close();
            }
            return result;
        }
   
    }


}