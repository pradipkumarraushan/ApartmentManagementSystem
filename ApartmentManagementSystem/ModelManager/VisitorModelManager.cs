using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ApartmentManagementSystem.Models;

namespace ApartmentManagementSystem.ModelManager
{
    public class VisitorModelManager
    {
        string cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
        public void VisitorIn(VisitorModel model)
        {
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("visitorin",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cell",model.Visitor_Cellno);
                    cmd.Parameters.AddWithValue("@visitor_name", model.VisitorName);
                    cmd.Parameters.AddWithValue("@age", model.Age);
                    cmd.Parameters.AddWithValue("@gender", model.Gender);
                    cmd.Parameters.AddWithValue("@address", model.Address);
                    cmd.Parameters.AddWithValue("@relation", model.Relation);
                    cmd.Parameters.AddWithValue("@date_time_in", model.In_Time);
                    cmd.Parameters.AddWithValue("@member_name", model.MemberName);
                    cmd.Parameters.AddWithValue("@block", model.Block);
                    cmd.Parameters.AddWithValue("@flat_no", model.Flatno);
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

        public VisitorModel GetVisitor(VisitorModel model)
        {
            VisitorModel visitor = new VisitorModel();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("searchvisitor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cell", model.Visitor_Cellno);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                visitor.Visitor_Cellno = dr["visitor_cell"].ToString();
                                visitor.VisitorName = dr["visitor_name"].ToString();
                                visitor.Age =(int) dr["age"];
                                visitor.Gender = dr["gender"].ToString();
                                visitor.Address = dr["address"].ToString();
                                visitor.Relation = dr["relation"].ToString();
                                visitor.MemberName = dr["member_name"].ToString();
                                visitor.Block = dr["block"].ToString();
                                visitor.Flatno =(int) dr["flat_no"];
                                string in_time = dr["date_time_in"].ToString();
                                string out_time = dr["date_time_out"].ToString();
                                if (in_time!=string.Empty && out_time!=string.Empty)
                                {

                                }
                                else if(in_time != string.Empty)
                                {
                                    visitor.In_Time = dr["date_time_in"].ToString();
                                }
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
            return visitor;
        }

        public void VisitorOut(VisitorModel model)
        {
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("visitorout", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cell", model.Visitor_Cellno);
                    cmd.Parameters.AddWithValue("@date_time_out", model.Out_Time);
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

        public List<VisitorModel> GetAllVisitor()
        {
            List<VisitorModel> visitorlist = new List<VisitorModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getallvisitor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                VisitorModel vm = new VisitorModel();
                                vm.Visitor_Id = (int)dr["visitor_id"];
                                vm.VisitorName = dr["visitor_name"].ToString();
                                vm.Visitor_Cellno = dr["visitor_cell"].ToString();
                                vm.Age = (int)dr["age"];
                                vm.Gender = dr["gender"].ToString();
                                vm.Address = dr["address"].ToString();
                                vm.Relation = dr["relation"].ToString();
                                vm.MemberName = dr["member_name"].ToString();
                                vm.Block = dr["block"].ToString();
                                vm.Flatno =(int) dr["flat_no"];
                                vm.In_Time = dr["date_time_in"].ToString();
                                vm.Out_Time = dr["date_time_out"].ToString();
                                visitorlist.Add(vm);
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
            return visitorlist;
        }

        public List<FamilyModel> GetAllRegularVisitor()
        {
            List<FamilyModel> lstmodel = new List<FamilyModel>();
            using (SqlConnection cn = new SqlConnection(cns))
            {
                using (SqlCommand cmd = new SqlCommand("getallregularvisitor", cn))
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
                                fm.Name = dr["name"].ToString();
                                fm.Age =(int) dr["age"];
                                fm.Gender = dr["gender"].ToString();
                                fm.Cell = dr["cell_no"].ToString();
                                fm.Relation = dr["relation"].ToString();
                                fm.Address = dr["address"].ToString();
                                fm.Block = dr["block"].ToString();
                                fm.Flat_no =(int) dr["flat_no"];
                                fm.Member_Name = dr["member_name"].ToString();
                                lstmodel.Add(fm);
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
            return lstmodel;
        }
    }
}