using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;
using Tom.ChineseChess.Engine.Util;

namespace Tom.ChineseChess.Engine.Chessing
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

        public Square(Camp camp,ChessColor color)
        {
            _camp = camp;
            _color = color;
            InitFlow();
            GetInitChessList();

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

        private void GetInitChessList()
        {
            _chessList = new List<Engine.IChess>();

            _chessList.Add(new King(this, _color, _table));
        }

        #region ISquare
        IChess ISquare.GetChess(ChessType chessType, int index)
        {
            throw new NotImplementedException();
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
