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
        private ChessColor _color;
        private ITable _table;
        private SquareState CurState { get { return _flow.CurState; } }
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

        ChessColor ISquare.Color
        {
            get
            {
                return _color;
            }
        }

        ITable ISquare.Table
        {
            get
            {
                return _table;
            }
        }

        ILog _logger;

        ILog ISquare.Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
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
            list.Add(new Rooks(this, new ChessPoint(camp, 0, 0)));
            list.Add(new Rooks(this, new ChessPoint(camp, 8, 0)));
            list.Add(new Knights(this, new ChessPoint(camp, 1, 0)));
            list.Add(new Knights(this, new ChessPoint(camp, 7, 0)));
            list.Add(new Elephants(this, new ChessPoint(camp, 2, 0)));
            list.Add(new Elephants(this, new ChessPoint(camp, 6, 0)));
            list.Add(new Mandarins(this, new ChessPoint(camp, 3, 0)));
            list.Add(new Mandarins(this, new ChessPoint(camp, 5, 0)));
            list.Add(new King(this, new ChessPoint(camp, 4, 0)));

            list.Add(new Cannons(this, new ChessPoint(camp, 1, 2)));
            list.Add(new Cannons(this, new ChessPoint(camp, 7, 2)));

            list.Add(new Pawns(this, new ChessPoint(camp, 0, 3)));
            list.Add(new Pawns(this, new ChessPoint(camp, 2, 3)));
            list.Add(new Pawns(this, new ChessPoint(camp, 4, 3)));
            list.Add(new Pawns(this, new ChessPoint(camp, 6, 3)));
            list.Add(new Pawns(this, new ChessPoint(camp, 8, 3)));

            return list;
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
            _flow.Next(SquareState.Sited);
            _table = table;
            _table.AddSquare(this);

            foreach (var chess in _chessList)
            {
                table[chess.CurrentPoint] = chess;
            }
        }

        #endregion
    }
}
