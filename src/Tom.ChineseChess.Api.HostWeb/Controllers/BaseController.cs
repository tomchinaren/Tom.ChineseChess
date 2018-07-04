using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tom.ChineseChess.Api.HostWeb.Filter;
using Tom.ChineseChess.Service.Context;

namespace Tom.ChineseChess.Api.HostWeb.Controllers
{
    /// <summary>
    /// Api基类
    /// 职责：
    /// 1、验证用户Token
    /// 2、获取用户身份
    /// </summary>
    [CheckToken]
    public class BaseController : ApiController, IIdentity
    {
        public IIdentityContext Identity { get; set; }
    }
}
