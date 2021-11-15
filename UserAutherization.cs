using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Gleason_WebApi
{
    public class UserAutherization : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authTocken = actionContext.Request.Headers.Authorization.Parameter;
                var decodeAuthTocken = Encoding.UTF8.GetString(Convert.FromBase64String(authTocken));
                var Credentials = decodeAuthTocken.Split(':');
                var username = Credentials[0];
                var Password = Credentials[1];
                BlUserInfo obj = new BlUserInfo();
                if (obj.UserValidation(username, Password) > 0)
                {
                    LoginUser.UserName = username;
                }
                else if (obj.UserValidation(username, Password) == -2) {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Your Not Admin");
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Provide valid Credentials");
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Credentials");
            }
        }
    }
}