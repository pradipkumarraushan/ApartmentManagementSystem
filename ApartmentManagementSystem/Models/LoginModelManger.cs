using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class LoginModelManger
    {
        public LoginModel Login(LoginModel login)
        {
            string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            LoginModel model = new LoginModel();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("login_check",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", login.Email);
                    cmd.Parameters.AddWithValue("@password", login.Password);
                    try
                    { 
                        cn.Open();
                        model.role = (string)cmd.ExecuteScalar();
                    }
                    catch(SqlException ex) { }
                    finally
                    {
                        if(cn.State==ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return model;
        }
    }
}