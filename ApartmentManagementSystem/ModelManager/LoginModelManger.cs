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
        string Cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
        public LoginModel Login(LoginModel login)
        { 
            LoginModel model = new LoginModel();
            using (SqlConnection cn = new SqlConnection(Cns))
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
                    catch(SqlException ex) { throw ex; }
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

        public LoginModel GetPassword(LoginModel model)
        {
            LoginModel getmodel = new LoginModel();
            using (SqlConnection cn = new SqlConnection(Cns))
            {
                using (SqlCommand cmd = new SqlCommand("getpassword", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@cell", model.Cell_no);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                getmodel.Email = dr["email"].ToString();
                                getmodel.Password = dr["password"].ToString();
                            }
                        }
                    }
                    catch(SqlException ex) { throw ex; }
                    finally
                    {
                        if(cn.State==ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }return getmodel;
        }
    }
}