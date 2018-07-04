using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.Api.Response;
using Tom.ChineseChess.Engine;
using Tom.ChineseChess.Engine.Chessing;
using Tom.ChineseChess.Engine.Enums;
using Tom.ChineseChess.Sdk.Request;
using Tom.ChineseChess.Sdk.Response;
using Tom.ChineseChess.Service.Context;
using Tom.ChineseChess.Service.Util;

namespace Tom.ChineseChess.Service
{
    public class ChessingService
    {
        #region 初始化
        private IIdentityContext identity;
        private ISquare square { get { return this.identity.Square; } }
        private IChessContext chessContext;
        public ChessingService()
        {
            chessContext = new ChessContext(1, 2, 3);
        }
        #endregion

        public SquareSitResponse Sit(SquareSitRequest request)
        {
            var res = new SquareSitResponse();

            var tableId = Utils.GetRequestParam<int>(request.BizContent, "table_id");
            var table = chessContext.Tables[tableId];
            square.Sit(table);

            return res;
        }

        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            var res = new SquareReadyResponse();
            square.Ready();
            return res;
        }

        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            var res = new ChessMoveResponse();

            var dict = Utils.GetRequestDict(request.BizContent);
            var chessType = Utils.GetRequestParam<ChessType>(dict, "chesstype");
            int relativeX = Utils.GetRequestParam<int>(dict, "relativex");
            int relativeY = Utils.GetRequestParam<int>(dict, "relativey");

            var chess = square.GetChess(chessType);
            chess.MoveTo(new ChessPoint(relativeX, relativeY));

            return res;
        }
    }
}
