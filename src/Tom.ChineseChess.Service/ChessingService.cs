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
    public class ChessingService: ISetIdentity
    {
        #region 初始化
        private IIdentityContext identity;
        private ISquare square { get { return identity.Square; } }
        private IChessContext chessContext;
        public IChessContext ChessContext { get { return chessContext; } }
        public ChessingService()
        {
            chessContext = new ChessContext(1, 2, 3);
        }
        /// <summary>
        /// 在业务操作前调用
        /// </summary>
        /// <param name="identity"></param>
        public void SetIdentity(IIdentityContext identity)
        {
            this.identity = identity;
        }
        #endregion

        public SquareSitResponse Sit(SquareSitRequest request)
        {
            var res = new SquareSitResponse();

            //var userID = Utils.GetUserIDByToken(request.Token);
            //var square = chessContext.Squares[userID];

            var tableId = Utils.GetRequestParam<int>(request.BizContent, "table_id");
            var table = chessContext.Tables[tableId];
            square.Sit(table);

            Utils.SetDebugInfo(request, res, square);
            return res;
        }

        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            var res = new SquareReadyResponse();
            square.Ready();
            Utils.SetDebugInfo(request, res, square);
            return res;
        }

        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            var res = new ChessMoveResponse();

            var dict = Utils.GetRequestDict(request.BizContent);
            var chessType = Utils.GetRequestParam<ChessType>(dict, "chesstype");
            int index = Utils.GetRequestParam<int>(dict, "index");
            int relativeX = Utils.GetRequestParam<int>(dict, "relativex");
            int relativeY = Utils.GetRequestParam<int>(dict, "relativey");

            var chess = square.GetChess(chessType, index);
            chess.MoveTo(new ChessPoint(relativeX, relativeY));

            Utils.SetDebugInfo(request, res, square);
            return res;
        }
    }
}
