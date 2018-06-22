using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public abstract class Chess : IChess
    {
        protected ITable _chessboard;
        private ISquare _square;
        protected ChessColor _color;
        protected IChessPoint _currentPoint;
        /// <summary> 
        /// 棋子颜色 
        /// </summary> 
        public ChessColor Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public abstract IChessPoint FirstPoint
        {
            get;
        }
        /// <summary> 
        /// 当前坐标 
        /// </summary> 
        public IChessPoint CurrentPoint
        {
            get { return _currentPoint; }
            set { _currentPoint = value; }
        }
        public Chess(ISquare square, ChessColor color, IChessPoint tragPoint, ITable board)
        {
            _square = square;
            this._color = color;
            this._currentPoint = FirstPoint;
            this._chessboard = board;
        }


        /// <summary> 
        /// 是否能够移动 
        /// </summary> 
        protected abstract bool CanMoveTo(IChessPoint p);
        /// <summary> 
        /// 棋子图片:抽象的属性 
        /// </summary> 
        public abstract Image ChessImage { get; }

        #region IChess
        public ISquare Square { get { return _square; } }
        void IChess.MoveTo(IChessPoint targetPoint)
        {
            //目标棋子和当前棋子颜色不能一致 
            IChess targetChess = _chessboard[targetPoint];
            if (targetChess != null && targetChess.Square == this.Square) return;

            //是否满足规则 
            if (!CanMoveTo(targetPoint)) return;

            //吃掉对方老王 
            if (_chessboard[targetPoint] is King)
                throw new GameLoseException(this.Color == ChessColor.Red ? "红方胜" : "黑方胜");

            //移动 
            _chessboard[_currentPoint] = null; //吃掉棋子或移动棋子 
            _chessboard[targetPoint] = this;

            this._currentPoint = targetPoint;
        }
        #endregion

        /// <summary> 
        /// 获取两点之间的棋子数 
        /// </summary> 
        public int GetChessCount(IChessPoint start, IChessPoint end)
        {
            //如果Y相同 
            if (start.Y == end.Y)
            {
                //获取最大X和最小X值 
                int min = Math.Min(start.X, end.X);
                int max = Math.Max(start.X, end.X);

                //棋子计数器 
                int count = 0;
                for (int i = min + 1; i < max; i++)
                {
                    if (_chessboard[i, start.Y] != null)
                        count++;
                }
                return count;
            }
            else
            {
                int min = Math.Min(start.Y, end.Y);
                int max = Math.Max(start.Y, end.Y);

                int count = 0;
                for (int i = min + 1; i < max; i++)
                {
                    if (_chessboard[start.X, i] != null)
                        count++;
                }
                return count;
            }
        }
    }
}
