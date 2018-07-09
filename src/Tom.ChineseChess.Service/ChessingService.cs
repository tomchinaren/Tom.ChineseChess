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
        private IIdentityContext _identity;
        private long UserID { get { return _identity.UserInfo.UserID; } }
        public ISquare Square
        {
            get
            {
                if (!ChessingManager.Instance.Squares.Keys.Contains(UserID))
                {
                    return null;
                }
                return ChessingManager.Instance.Squares[UserID];
            }
        }
        //传identity而不直接传userID的原因，是userID可能会变
        public ChessingService(IIdentityContext identity)
        {
            this._identity = identity;
            ChessingManager.Init(1, 2, 3);

        }
        #endregion

        public SquareSitResponse Sit(SquareSitRequest request)
        {
            var res = new SquareSitResponse();

            //var userID = Utils.GetUserIDByToken(request.Token);
            //var square = chessContext.Squares[userID];
            var tableId = Utils.GetRequestParam<int>(request.Biz_Content, "table_id");
            var table = ChessingManager.Instance.Tables[tableId];
            ISquare square = Square;
            if (square!=null)
            {
                if(square.Table!=null && square.Table!= table)
                {
                    throw new Exception(string.Format("Error when user {0} Sit at table {1}, square with other table ", UserID, tableId));
                }
            }
            else
            {
                var newCamp = !table.Squares.Keys.Contains(Camp.RedCamp) ? Camp.RedCamp : Camp.BlackCamp;
                square = new Square(newCamp, newCamp == Camp.RedCamp ? ChessColor.Red : ChessColor.Black);
                ChessingManager.Instance.Squares.Add(UserID, square);
            }
            square.Sit(table);

            return res;
        }

        public SquareReadyResponse Ready(SquareReadyRequest request)
        {
            var res = new SquareReadyResponse();
            Square.Ready();
            return res;
        }

        public ChessMoveResponse Move(ChessMoveRequest request)
        {
            var res = new ChessMoveResponse();

            var dict = Utils.GetRequestDict(request.Biz_Content);
            var chessType = Utils.GetRequestParam<ChessType>(dict, "chesstype");
            int index = Utils.GetRequestParam<int>(dict, "index");
            int relativeX = Utils.GetRequestParam<int>(dict, "relativex");
            int relativeY = Utils.GetRequestParam<int>(dict, "relativey");

            var chess = Square.GetChess(chessType, index);
            chess.MoveTo(new ChessPoint(relativeX, relativeY));

            return res;
        }
    }
}
