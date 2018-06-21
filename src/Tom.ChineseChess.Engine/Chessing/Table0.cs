using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class Table
    {
        private List<Square> _squares;
        private List<Square> _vsSquares;
        private readonly int _startCountCondition;
        private int _state;
        public int State
        {
            get { return _state; }
        }

        private void ComeIn(Square square)
        {
            _squares.Add(square);
        }

        public void Sit(Square square)
        {
            ComeIn(square);

            _vsSquares.Add(square);
        }

        public void View(Square square)
        {
            ComeIn(square);
        }

        public void Ready(Square square)
        {
            square.Ready();
        }

        public void StartChessing(Chessing chessing)
        {

        }
    }
}
