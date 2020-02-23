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
    public class MeetingModelManger
    {
        string Cns = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;

        public List<string> GetNumber(string send_to)
        {
            List<string> lstnumber = new List<string>();
            using (SqlConnection cn = new SqlConnection(Cns))
            {
                using (SqlCommand cmd = new SqlCommand("getnumber", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@send_to", send_to);
                    try
                    {
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                lstnumber.Add(dr["cell_no"].ToString());
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
             return lstnumber;
        }

        public List<string> GetSendToList()
        {
            List<string> lstsendmsgto = new List<string>();
            lstsendmsgto.Add(Role.ASSOC_MEMBER);
            lstsendmsgto.Add(Role.OWNER);
            lstsendmsgto.Add(Role.TENANT);
            return lstsendmsgto;
        }

        public List<string> GetMeetingLocation()
        {
            List<string> lstlocation = new List<string>();
            lstlocation.Add("PartyHall");
            lstlocation.Add("ManagerRoom");
            return lstlocation;
        }
    }
}