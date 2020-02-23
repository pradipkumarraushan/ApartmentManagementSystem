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
       public  string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
        public List<string> GetBlockList()
        {
            List<string> lstblock = new List<string>();
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
                                lstblock.Add(dr["block"].ToString());
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
            }
            return lstblock;
        }

        public bool CreateOwnerTenant(OwnerTenantRegistrationModel createmodel)
        {
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
                }
                return is_success;
            }
        }

        public bool UpdateOwnerTenant(OwnerTenantRegistrationModel updatemodel)
        {
            bool is_success = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("updateownertenant",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", updatemodel.Id);
                    cmd.Parameters.AddWithValue("@fname", updatemodel.Fname);
                    cmd.Parameters.AddWithValue("@lname", updatemodel.Lname);
                    cmd.Parameters.AddWithValue("@age", updatemodel.Age);
                    cmd.Parameters.AddWithValue("@gender", updatemodel.Gender);
                    cmd.Parameters.AddWithValue("@cell_no", updatemodel.Cell);
                    cmd.Parameters.AddWithValue("@email", updatemodel.Email);
                    cmd.Parameters.AddWithValue("@maritalstatus", updatemodel.Marital_status);
                    cmd.Parameters.AddWithValue("@occupation", updatemodel.Occupation);
                    cmd.Parameters.AddWithValue("@company", updatemodel.Company);
                    cmd.Parameters.AddWithValue("@office_address", updatemodel.Office_address);
                    cmd.Parameters.AddWithValue("@block", updatemodel.Block);
                    cmd.Parameters.AddWithValue("@flat_no", updatemodel.Flat_no);
                    cmd.Parameters.AddWithValue("@membertype", updatemodel.Member_type);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_success = true;
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
                return is_success;
            }
        }

        public bool DeleteOwnerTenant(int id)
        {
            bool is_success = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("deleteownertenant", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_success = true;
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
                return is_success;
            }
        }

        public List<OwnerTenantRegistrationModel> GetAllOwnerTenant()
        {
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
                                model.Id =Convert.ToInt32(dr["member_id"]);
                                model.Fname = dr["fname"].ToString();
                                model.Lname = dr["lname"].ToString();
                                model.Age = (int)dr["age"];
                                model.Cell = dr["cell_no"].ToString();
                                model.Email = dr["email"].ToString();
                                model.Occupation = dr["occupation"].ToString();
                                model.Company = dr["company"].ToString();
                                model.Office_address = dr["office_address"].ToString();
                                 DateTime date =Convert.ToDateTime (dr["on_date"]);
                                model.On_date = date.ToShortDateString();
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
            }
            return getalllist;
        }


        public OwnerTenantRegistrationModel SearchMember(OwnerTenantRegistrationModel model)
        {
            OwnerTenantRegistrationModel getmember = new OwnerTenantRegistrationModel();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("searchmember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                getmember.Fname = dr["fname"].ToString();
                                getmember.Lname = dr["lname"].ToString();
                                getmember.Age = (int)dr["age"];
                                getmember.Gender = dr["gender"].ToString();
                                getmember.Cell = dr["cell_no"].ToString();
                                getmember.Email = dr["email"].ToString();
                                getmember.Password = dr["password"].ToString();
                                getmember.Block = dr["block"].ToString();
                                getmember.Flat_no = (int)dr["flat_no"];
                            }
                            dr.Close();
                        }
                    }
                    catch(SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return getmember;
        }
         
        public OwnerTenantRegistrationModel GetDesigination()
        {
            OwnerTenantRegistrationModel desigination = new OwnerTenantRegistrationModel();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getdesigination", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                desigination.lstdesigination.Add(dr["role"].ToString());
                            }
                            dr.Close();
                        }
                    }
                    catch(SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return desigination;
        }

        public bool AddAssociationMember(OwnerTenantRegistrationModel model)
        {
            bool is_added = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("addassociationmember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fname", model.Fname);
                    cmd.Parameters.AddWithValue("@lname", model.Lname);
                    cmd.Parameters.AddWithValue("@age", model.Age);
                    cmd.Parameters.AddWithValue("@gender", model.Gender);
                    cmd.Parameters.AddWithValue("@cell_no", model.Cell);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    cmd.Parameters.AddWithValue("@password", model.Password);
                    cmd.Parameters.AddWithValue("@block", model.Block);
                    cmd.Parameters.AddWithValue("@flat_no", model.Flat_no);
                    cmd.Parameters.AddWithValue("@role", model.Desigination);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count>0)
                        {
                            is_added = true;
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return is_added;
        }

        public bool RemoveAssociationMember(int id)
        {
            bool is_added = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("removeassociationmember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                   
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_added = true;
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return is_added;
        }

        public List<OwnerTenantRegistrationModel> GetAllAssociationMember()
        {
            List<OwnerTenantRegistrationModel> getalllist = new List<OwnerTenantRegistrationModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getassociationmember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                OwnerTenantRegistrationModel model = new OwnerTenantRegistrationModel();
                                model.Id = Convert.ToInt32(dr["id"]);
                                model.Fname = dr["fname"].ToString();
                                model.Lname = dr["lname"].ToString();
                                model.Age = (int)dr["age"];
                                model.Gender = dr["gender"].ToString();
                                model.Cell = dr["cell_no"].ToString();
                                model.Email = dr["email"].ToString();
                                model.Block = dr["block"].ToString();
                                model.Flat_no = (int)dr["flat_no"];
                                model.Desigination = dr["role"].ToString();
                                getalllist.Add(model);
                            }
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return getalllist;
        }

        public List<FamilyModel> GetComplaints()
        {
            List<FamilyModel> lstcomplaint = new List<FamilyModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getallcomplaints", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                FamilyModel fm = new FamilyModel();
                                fm.Id = Convert.ToInt32(dr["id"]);
                                fm.Name = dr["name"].ToString();
                                fm.Cell = dr["cell_no"].ToString();
                                fm.Block = dr["block"].ToString();
                                fm.Flat_no = Convert.ToInt32(dr["flat_no"]);
                                fm.ComplainType = dr["complain_type"].ToString();
                                fm.ComplainDescription = dr["cdescription"].ToString();
                                fm.RequestDate = dr["request_date"].ToString();
                                fm.Service_Provider = dr["service_provider"].ToString();
                                lstcomplaint.Add(fm);
                            }
                            dr.Close();
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
            }
            return lstcomplaint;
        }

        public bool EditRequest(FamilyModel edit)
        {
            bool is_success = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("editcomplaints", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", edit.Id);
                    cmd.Parameters.AddWithValue("@service_provider", edit.Service_Provider);
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
                }
            }
            return is_success;
        }

        public List<FamilyModel> GetBookedAmenities()
        {
            List<FamilyModel> bookedlst = new List<FamilyModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getallbookedamenities", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                FamilyModel fm = new FamilyModel();
                                fm.Id =(int) dr["Booking_id"];
                                fm.Name = dr["name"].ToString();
                                fm.Cell = dr["cell"].ToString();
                                fm.Block = dr["block"].ToString();
                                fm.Flat_no =(int) dr["flat_no"];
                                fm.Booking_for = dr["amenitie"].ToString();
                                fm.On_date = dr["on_date"].ToString();
                                fm.On_time = dr["booktime"].ToString();
                                fm.Status = dr["status"].ToString();
                                bookedlst.Add(fm);
                            }
                            dr.Close();
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
            }
            return bookedlst;
        }

        public bool EditBooking(FamilyModel edit)
        {
            bool is_success = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("editbooking", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", edit.Id);
                    cmd.Parameters.AddWithValue("@status", edit.Status);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_success = true;
                        }
                    }
                    catch (SqlException ex) { throw ex; }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return is_success;
        }

        public void DeleteBooking(int id)
        {
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("deleteamenitie", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
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
        }
    }
}