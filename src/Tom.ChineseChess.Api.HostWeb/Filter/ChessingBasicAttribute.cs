using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Tom.Api;
using Tom.ChineseChess.Service.Context;
using Tom.ChineseChess.Service.Util;

namespace Tom.ChineseChess.Api.HostWeb.Filter
{
    public class ChessingBasicAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var basic = actionContext.ControllerContext.Controller as IContextContainer;
            if (basic != null)
            {
                var authToken = GetContent(actionContext);
                var token = authToken?.Auth_Token;

                var userID = 1L;// Utils.GetUserIDByToken(token);
                if (basic.Identity == null)
                {
                    basic.Identity = new IdentityContext(null, new UserInfo());
                }
                basic.Identity.SetUserInfo(new UserInfo() { UserID = userID });
            }

            base.OnActionExecuting(actionContext);
        }

        private AuthToken GetContent(HttpActionContext actionContext)
        {
            AuthToken authToken = null;
            var task = actionContext.Request.Content.ReadAsStreamAsync();
            var content = string.Empty;
            using (System.IO.Stream sm = task.Result)
            {
                if (sm != null)
                {
                    sm.Seek(0, SeekOrigin.Begin);
                    int len = (int)sm.Length;
                    byte[] inputByts = new byte[len];
                    sm.Read(inputByts, 0, len);
                    sm.Close();
                    content = Encoding.UTF8.GetString(inputByts);
                    content = HttpUtility.UrlDecode(content);
                }
            }

            if (!string.IsNullOrEmpty(content))
            {
                authToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthToken>(content);
            }
            
            return authToken;
        }
    }
}