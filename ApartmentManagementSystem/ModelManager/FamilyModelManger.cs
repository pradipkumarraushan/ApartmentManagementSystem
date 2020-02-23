using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class FamilyModelManger
    {
         string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;

        public List<FamilyModel> GetFamilyMember(string email)
        {
            List<FamilyModel> lst = new List<FamilyModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getfamilymember",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email",email);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                FamilyModel family = new FamilyModel();
                                family.Id =Convert.ToInt32(dr["familymember_id"]);
                                family.Name = dr["name"].ToString();
                                family.Age = Convert.ToInt32(dr["age"]);
                                family.Gender = dr["gender"].ToString();
                                family.Cell = dr["cell"].ToString();
                                family.Relation = dr["relation"].ToString();
                                lst.Add(family);
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
            }return lst;
        }

        public List<string> GetRelation()
        {
            List<string> lstrelation = new List<string>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getrelation", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                lstrelation.Add(dr["relation"].ToString());
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
            return lstrelation;
        }

        public bool CreateFamilyMember(FamilyModel model)
        {
            if(model.Cell==null)
            {
                model.Cell = "null";
            }
            bool is_added = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("addfamilymember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", model.Name);
                    cmd.Parameters.AddWithValue("@age", model.Age);
                    cmd.Parameters.AddWithValue("@gender", model.Gender);
                    cmd.Parameters.AddWithValue("@cell", model.Cell);
                    cmd.Parameters.AddWithValue("@relation", model.Relation);
                    cmd.Parameters.AddWithValue("@email", model.Email);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count>0)
                        {
                            is_added = true;
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
            return is_added;
        }

        public bool DeleteFamilyMember(int id)
        {
            bool is_deleted = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("Deletefamilymember", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count>0)
                        {
                            is_deleted = true;
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
            return is_deleted;
        }

        public List<FamilyModel> GetRegularVisitor(string email)
        {
            List<FamilyModel> lst = new List<FamilyModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getregularvisitor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                FamilyModel family = new FamilyModel();
                                family.Id = Convert.ToInt32(dr["familymember_id"]);
                                family.Name = dr["name"].ToString();
                                family.Age = Convert.ToInt32(dr["age"]);
                                family.Gender = dr["gender"].ToString();
                                family.Cell = dr["cell"].ToString();
                                family.Relation = dr["relation"].ToString();
                                lst.Add(family);
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
            return lst;
        }

        public bool CreateRegularVisitor(FamilyModel model)
        {
            if (model.Cell == null)
            {
                model.Cell = "null";
            }
            bool is_added = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("addregularvisitor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", model.Name);
                    cmd.Parameters.AddWithValue("@age", model.Age);
                    cmd.Parameters.AddWithValue("@gender", model.Gender);
                    cmd.Parameters.AddWithValue("@cell", model.Cell);
                    cmd.Parameters.AddWithValue("@relation", model.Relation);
                    cmd.Parameters.AddWithValue("@email", model.Email);
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

        public bool DeleteRegularVisitor(int id)
        {
            bool is_deleted = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("Deleteregularvisitor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_deleted = true;
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
            return is_deleted;
        }

        public List<string> GetComplain()
        {
            List<string> lstcomplaint = new List<string>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getcomplaintype", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lstcomplaint.Add(dr["complain_type"].ToString());
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

        public DataSet GetComplaintlist(string email)
        {
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getuserrequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public bool RegisterComplaint(FamilyModel request)
        {
            bool is_registered = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("registercomplaint", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", request.Name);
                    cmd.Parameters.AddWithValue("@cell", request.Cell);
                    cmd.Parameters.AddWithValue("@block", request.Block);
                    cmd.Parameters.AddWithValue("@flat_no", request.Flat_no);
                    cmd.Parameters.AddWithValue("@complain_type", request.ComplainType);
                    cmd.Parameters.AddWithValue("@complain_description", request.ComplainDescription);
                    cmd.Parameters.AddWithValue("@request_date", request.RequestDate);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count>0)
                        {
                            is_registered = true;
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
            return is_registered;
        }

        public void DeleteServiceRequest(int id)
        {
            //bool is_deleted = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("deletecomplaint", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        //if(count>0)
                        //{
                        //    is_deleted = true;
                        //}
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
            //return is_deleted;
        }

        public DataSet GetBookedDetail(string email)
        {
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getuserbooked", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public List<string> GetAmenities()
        {
            List<string> lstamenities = new List<string>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getamenities", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lstamenities.Add(dr["amenitie"].ToString());
                            }
                            dr.Close();
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
            return lstamenities;
        }

        public List<string> GetTiming(string book_for)
        {
            List<string> lsttiming = new List<string>();
            lsttiming.Add("--select--");
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("gettiming", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@amenitie", book_for);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                lsttiming.Add(dr["booktime"].ToString());
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
            return lsttiming;
        }

        public bool BookAmenitie(FamilyModel book)
        {
            bool is_registered = false;
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("bookamenitie", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", book.Name);
                    cmd.Parameters.AddWithValue("@cell", book.Cell);
                    cmd.Parameters.AddWithValue("@block", book.Block);
                    cmd.Parameters.AddWithValue("@flat_no", book.Flat_no);
                    cmd.Parameters.AddWithValue("@booking_for", book.Booking_for);
                    cmd.Parameters.AddWithValue("@on_date", book.On_date);
                    cmd.Parameters.AddWithValue("@on_time", book.On_time);
                    try
                    {
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            is_registered = true;
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
            return is_registered;
        }
    }
}