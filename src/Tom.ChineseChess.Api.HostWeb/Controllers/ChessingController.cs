using System.Web.Http;
using Tom.ChineseChess.Sdk.Request;
using Tom.ChineseChess.Sdk.Response;
using Tom.ChineseChess.Service;
using Tom.ChineseChess.Service.Util;

namespace Tom.ChineseChess.Api.HostWeb.Controllers
{
    public class ChessingController : BaseController
    {
        #region 初始化
        private static ChessingService service;
        static ChessingController()
        {
            service = new ChessingService();
        }
        #endregion

        [HttpPost]
        public SquareSitResponse Sit(SquareSitRequest request)
        {
            return Utils.TryGetResponse(request, service.Sit);
        }

        [HttpPost]
        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            return Utils.TryGetResponse(request, service.Ready);
        }

        [HttpPost]
        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            return Utils.TryGetResponse(request, service.Move);
        }
    }
}
