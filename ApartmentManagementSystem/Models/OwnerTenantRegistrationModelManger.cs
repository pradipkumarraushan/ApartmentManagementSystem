using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class OwnerTenantRegistrationModelManger
    {
        public OwnerTenantRegistrationModel GetDropdownList()
        {
            string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            OwnerTenantRegistrationModel getlist = new OwnerTenantRegistrationModel();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getblock",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                getlist.lstblock.Add(dr["block"].ToString());
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
            }return getlist;
        }
        public bool CreateOwnerTenant(OwnerTenantRegistrationModel createmodel)
        {
            string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            bool is_success = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("insertownertenant",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fname", createmodel.Fname);
                    cmd.Parameters.AddWithValue("@lname", createmodel.Lname);
                    cmd.Parameters.AddWithValue("@age", createmodel.Age);
                    cmd.Parameters.AddWithValue("@gender", createmodel.Gender);
                    cmd.Parameters.AddWithValue("@cell_no", createmodel.Cell);
                    cmd.Parameters.AddWithValue("@email", createmodel.Email);
                    cmd.Parameters.AddWithValue("@password", createmodel.Password);
                    cmd.Parameters.AddWithValue("@maritalstatus", createmodel.Marital_status);
                    cmd.Parameters.AddWithValue("@occupation", createmodel.Occupation);
                    cmd.Parameters.AddWithValue("@company", createmodel.Company);
                    cmd.Parameters.AddWithValue("@office_address", createmodel.Office_address);
                    cmd.Parameters.AddWithValue("@On_date", createmodel.On_date);
                    cmd.Parameters.AddWithValue("@block", createmodel.Block);
                    cmd.Parameters.AddWithValue("@flat_no", createmodel.Flat_no);
                    cmd.Parameters.AddWithValue("@membertype", createmodel.Member_type);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count>0)
                        {
                            is_success = true;
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
                }return is_success;
            }
        }
        public void UpdateOwnerTenant(OwnerTenantRegistrationModel updatemodel)
        {
            string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                }
            }
        }
        public void DeleteOwnerTenant(OwnerTenantRegistrationModel deletemodel)
        {

        }
        public List<OwnerTenantRegistrationModel> GetAllOwnerTenant()
        {
            string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            List<OwnerTenantRegistrationModel> getalllist = new List<OwnerTenantRegistrationModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getallownertenant",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                OwnerTenantRegistrationModel model = new OwnerTenantRegistrationModel();
                                model.Fname = dr["fname"].ToString();
                                model.Lname = dr["lname"].ToString();
                                model.Age = (int)dr["age"];
                                model.Cell = dr["cell_no"].ToString();
                                model.Email = dr["email"].ToString();
                                model.Occupation = dr["occupation"].ToString();
                                model.Company = dr["company"].ToString();
                                model.Office_address = dr["office_address"].ToString();
                                model.On_date = dr["on-date"].ToString();
                                model.Block = dr["block"].ToString();
                                model.Flat_no =(int)dr["flat_no"];
                                model.Member_type = dr["member_type"].ToString();
                                getalllist.Add(model);
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
            }return getalllist;
        }
    }
}