using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MoreLinq;
namespace Gleason_WebApi
{
    public class DataAccess
    {
        public long UserValidation(string UserName, string PassWord)
        {
            long value = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_Gleason_User_Validate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", UserName);
                        cmd.Parameters.AddWithValue("@password", PassWord);
                        value = Convert.ToInt64(cmd.ExecuteScalar());
                    }
                    con.Close();
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long AddUsers(UserInfo objUser)
        {
            long value = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Gleason_UserInfo_Insert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", objUser.Email);
                        cmd.Parameters.AddWithValue("@firstname", objUser.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", objUser.LastName);
                        cmd.Parameters.AddWithValue("@username", objUser.UserName);
                        cmd.Parameters.AddWithValue("@udt_roles", objUser.lstRoles.ToDataTable());
                        value =Convert.ToInt64(cmd.ExecuteScalar());
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long UpdateUsers(UserInfo objUser)
        {
            long value = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Gleason_User_Update", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", objUser.UserID);
                        cmd.Parameters.AddWithValue("@email", objUser.Email);
                        cmd.Parameters.AddWithValue("@firstname", objUser.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", objUser.LastName);
                        cmd.Parameters.AddWithValue("@username", objUser.UserName);
                        cmd.Parameters.AddWithValue("@udt_roles", objUser.lstRoles.ToDataTable());
                        value = Convert.ToInt64(cmd.ExecuteScalar());
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet getUserList()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_Gleason_UserInfo_Get", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(ds);
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}