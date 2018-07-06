using System;
using System.Web.Http;
using Tom.ChineseChess.Api.HostWeb.Filter;
using Tom.ChineseChess.Engine;
using Tom.ChineseChess.Sdk.Request;
using Tom.ChineseChess.Sdk.Response;
using Tom.ChineseChess.Service;
using Tom.ChineseChess.Service.Context;
using Tom.ChineseChess.Service.Util;

namespace Tom.ChineseChess.Api.HostWeb.Controllers
{
    public class ChessingController : BaseController
    {
        #region 初始化
        private static ChessingService service;

        public ChessingController()
        {
            if (service == null)
            {
                if (Identity == null)
                {
                    Identity = new IdentityContext(null, null);
                }
                service = new ChessingService(Identity);
            }
        }
        #endregion

        [HttpPost]
        public SquareSitResponse Sit(SquareSitRequest request)
        {
            return Utils.TryGetResponse(request, service.Sit, UserID);
        }

        [HttpPost]
        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            return Utils.TryGetResponse(request, service.Ready, UserID);
        }

        [HttpPost]
        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            return Utils.TryGetResponse(request, service.Move, UserID);
        }

        [HttpGet]
        public string Test()
        {
            return "test111";
        }
    }
}
