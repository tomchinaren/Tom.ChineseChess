using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    /// <summary>
    /// 一盘棋
    /// </summary>
    public class Chessing
    {
        private string _name;

        private ChessBoard _board;
        /// <summary>
        /// 红方
        /// </summary>
        private Square _redSquare;
        /// <summary>
        /// 黑方
        /// </summary>
        private Square _blackSquare;

        public Chessing(string name)
        {
            _name = name;
        }

        public void Init(Square red, Square black)
        {
            _redSquare = red;
            _blackSquare = black;

            Clear();
        }

        public void Start(bool bRed = true)
        {
            TurnTo(bRed ? 1 : 2);
        }

        public void Finish(int flag)
        {
            Report();

            Clear();
        }

        private void TurnTo(int squre)
        {
        }

        private void Clear()
        {
        }

        private void Report()
        {

        }
    }
}
