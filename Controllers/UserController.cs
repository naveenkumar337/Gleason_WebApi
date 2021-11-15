using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Gleason_WebApi.Controllers
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        [UserAutherization]
        [Route("api/user/Validate")]
        [HttpGet]
        public HttpResponseMessage UserValidation()
        {
            Response obj = new Response();
            obj.ResponseCode = 1;
            obj.ResponseMessage = "Login Success";
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
        [Route("api/user/addUser")]
        [HttpPost]
        public HttpResponseMessage AddUser(UserInfo objUser)
        {
            if (LoginUser.UserName != null)
            {
                BlUserInfo obj = new BlUserInfo();
                var data = obj.AddNewUser(objUser);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, data);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Insertion Failed");
            }
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "please Provide Credentials");
        }
        [Route("api/user/updateUser")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(UserInfo objUser)
        {
            if (LoginUser.UserName != null)
            {
                BlUserInfo obj = new BlUserInfo();
                var data = obj.AddNewUser(objUser);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, data);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Insertion Failed");
            }
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "please Provide Credentials");
        }
        [Route("api/user/getAll")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            if (LoginUser.UserName != null)
            {
                BlUserInfo bl = new BlUserInfo();
                var data = bl.UserColectInfo();
                if (data.objUserInfo != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateResponse(HttpStatusCode.NoContent, data.objResponse);
            }
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "please Provide Credentials");
        }
    }
}

