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
        public ChessingController()
        {
            service = new ChessingService();
        }
        #endregion

        [HttpPost]
        public SquareSitResponse Sit(SquareSitRequest request=null)
        {
            return Utils.TryGetResponse(request, service.Sit, service, service.ChessContext);
        }

        [HttpPost]
        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            return Utils.TryGetResponse(request, service.Ready, service, service.ChessContext);
        }

        [HttpPost]
        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            return Utils.TryGetResponse(request, service.Move, service, service.ChessContext);
        }

    }
}
