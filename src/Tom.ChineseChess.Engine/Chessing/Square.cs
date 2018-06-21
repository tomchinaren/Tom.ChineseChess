using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine.Chessing
{
    public class Square : ISquare
    {
        ChessColor _color;
        ChessBoard _board;
        private List<IChess> _chessList;
        private void GetInitChessList()
        {
            _chessList = new List<Engine.IChess>();

            _chessList.Add(new King(this,_color, new ChessPoint() { X = 0, Y = 5 }, _board));
        }
        public Square(ChessColor color)
        {
            _color = color;

            GetInitChessList();
        }

        #region ISquare
        IChess ISquare.GetChess(ChessType chessType, int index)
        {
            throw new NotImplementedException();
        }

        void ISquare.Ready()
        {
            throw new NotImplementedException();
        }

        void ISquare.Sit(ITable table)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
