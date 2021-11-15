using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace Gleason_WebApi
{
    public class UserInfo
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<UserRoles> lstRoles { get; set; }
    }
    public class UserRoles
    {
        public long UserID { get; set; }
        public string Role { get; set; }
        public int RoleType { get; set; }
    }

    public class Response
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class ConnectionString
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ToString();
    }
    internal static class LoginUser
    {
        internal static string UserName { get; set; }
    }

    public class UserRespose
    {
        public List<UserInfo> objUserInfo { get; set; }
        public Response objResponse { get; set; }
    }

    public class UserValidate
    {
        public string UserName { get; set; }
    }
}