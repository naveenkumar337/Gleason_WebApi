using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Gleason_WebApi
{

    public class BlUserInfo
    {
        DataAccess dlObj = new DataAccess();

        public long UserValidation(string userName, string passWord)
        {
            long dbResponse = 0;
            try
            {
                dbResponse = dlObj.UserValidation(userName, passWord);
            }
            catch (Exception ex)
            {
                dbResponse = -1;
            }
            return dbResponse;
        }

        public Response AddNewUser(UserInfo objUser)
        {
            Response objreq = new Response();
            try
            {
                var id = dlObj.AddUsers(objUser);
                if (id > 0)
                {
                    objreq.ResponseCode = 1;
                    objreq.ResponseMessage = "Success";
                }
                else
                {
                    objreq.ResponseCode = 0;
                    objreq.ResponseMessage = "Failed";
                }
                return objreq;
            }
            catch (Exception ex)
            {
                objreq.ResponseCode = -11;
                objreq.ResponseMessage = "Exception Occured";
                return objreq;
            }
        }

        public Response UpdateUser(UserInfo objUser)
        {
            Response objreq = new Response();
            try
            {
                var id = dlObj.UpdateUsers(objUser);
                if (id > 0)
                {
                    objreq.ResponseCode = 1;
                    objreq.ResponseMessage = "Success";
                }
                else
                {
                    objreq.ResponseCode = 0;
                    objreq.ResponseMessage = "Failed";
                }
                return objreq;
            }
            catch (Exception ex)
            {
                objreq.ResponseCode = -11;
                objreq.ResponseMessage = "Exception Occured";
                return objreq;
            }
        }

        public UserRespose UserColectInfo()
        {
            UserRespose userRespose = new UserRespose();
            try
            {
                DataSet ds = dlObj.getUserList();
                var userTable = ds.Tables[0];
                var rolesTable = ds.Tables[1];
                userRespose.objUserInfo = userTable.AsEnumerable().Select(userrow =>
                 {
                     return new UserInfo()
                     {
                         FirstName = userrow.Field<string>("FirstName"),
                         LastName = userrow.Field<string>("LastName"),
                         UserID = userrow.Field<long>("UserID"),
                         UserName = userrow.Field<string>("UserName"),
                         Email = userrow.Field<string>("Email"),
                         lstRoles =
                     rolesTable.AsEnumerable().Select(row =>
                     {
                         return new UserRoles()
                         {
                             UserID = row.Field<long>("UserID"),
                             Role = row.Field<string>("Roles"),
                             RoleType = row.Field<int>("RoleType")
                         };
                     }).ToList().Where(e => e.UserID == userrow.Field<long>("UserID")).ToList()
                     };
                 }).ToList();
                userRespose.objResponse = new Response()
                {
                    ResponseCode = 1,
                    ResponseMessage = "Success"
                };
                return userRespose;
            }
            catch (Exception ex)
            {
                return new UserRespose()
                {
                    objResponse = new Response()
                    {
                        ResponseCode = -1,
                        ResponseMessage = "Error Ocuured"
                    }
                };
            }
        }
    }
}