using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;
using Tom.ChineseChess.Engine.Util;

namespace Tom.ChineseChess.Engine
{
    public class Square : ISquare
    {
        ChessColor _color;
        ITable _table;
        SquareState CurState { get { return _flow.CurState; } }
        private List<IChess> _chessList;
        private Flow<SquareState> _flow;
        private Camp _camp;

        public ITable Table
        {
            get { return _table; }
        }

        Camp ISquare.Camp
        {
            get
            {
                return _camp;
            }
        }

        public Square(Camp camp, ChessColor color)
        {
            _camp = camp;
            _color = color;
            InitFlow();
            _chessList = GetInitChessList(camp);
        }

        private void InitFlow()
        {
            _flow = new Flow<SquareState>(SquareState.Init);

            _flow.AddState(SquareState.Init, SquareState.Sited);
            _flow.AddState(SquareState.Sited, SquareState.Left);
            _flow.AddState(SquareState.Sited, SquareState.Ready);
            _flow.AddState(SquareState.Ready, SquareState.Started);
            _flow.AddState(SquareState.Started, SquareState.Stoped);
        }

        private List<Engine.IChess> GetInitChessList(Camp camp)
        {
            var list = new List<Engine.IChess>();
            list.Add(new Rooks(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 0))));
            list.Add(new Rooks(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 8))));
            list.Add(new Knights(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 1))));
            list.Add(new Knights(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 7))));
            list.Add(new Elephants(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 2))));
            list.Add(new Elephants(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 6))));
            list.Add(new Mandarins(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 3))));
            list.Add(new Mandarins(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 5))));
            list.Add(new King(this, _color, GetAbsolutePoint(camp, new ChessPoint(0, 4))));

            list.Add(new Cannons(this, _color, GetAbsolutePoint(camp, new ChessPoint(2, 1))));
            list.Add(new Cannons(this, _color, GetAbsolutePoint(camp, new ChessPoint(2, 7))));

            list.Add(new Pawns(this, _color, GetAbsolutePoint(camp, new ChessPoint(3, 0))));
            list.Add(new Pawns(this, _color, GetAbsolutePoint(camp, new ChessPoint(3, 2))));
            list.Add(new Pawns(this, _color, GetAbsolutePoint(camp, new ChessPoint(3, 4))));
            list.Add(new Pawns(this, _color, GetAbsolutePoint(camp, new ChessPoint(3, 6))));
            list.Add(new Pawns(this, _color, GetAbsolutePoint(camp, new ChessPoint(3, 8))));

            return list;
        }

        private IChessPoint GetAbsolutePoint(Camp camp, IChessPoint point)
        {
            point.X = camp == Camp.RedCamp ? point.RelativeX : 8 - point.RelativeX;
            point.Y = camp == Camp.RedCamp ? point.RelativeY : 9 - point.RelativeY;
            return point;
        }

        #region ISquare
        IChess ISquare.GetChess(ChessType chessType, int index)
        {
            var list = _chessList.Where(t => t.ChessType == chessType).ToList();
            return list[index];
        }

        void ISquare.Ready()
        {
            _flow.Next(SquareState.Ready);
        }

        void ISquare.Sit(ITable table)
        {
            _table = table;
            _flow.Next(SquareState.Sited);
        }
        #endregion
    }
}
