//1.棋子基类
//抽象类, 是所有棋子的基类, 注意几个重要的方法:如CanMoveTo移动算法及MoveTo移动棋子。
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tom.ChineseChess.Engine;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 棋子类 
    /// </summary> 
    public abstract class Chess
    {
        protected ChessColor _color;
        protected ChessPoint _currentPoint;
        protected ChessBoard _chessboard;

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Chess(ChessColor color, ChessPoint tragpoint, ChessBoard board)
        {
            this._color = color;
            this._currentPoint = tragpoint;
            this._chessboard = board;
        }

        /// <summary> 
        /// 棋子颜色 
        /// </summary> 
        public ChessColor Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary> 
        /// 当前坐标 
        /// </summary> 
        public ChessPoint CurrentPoint
        {
            get { return _currentPoint; }
            set { _currentPoint = value; }
        }

        /// <summary> 
        /// 棋盘 
        /// </summary> 
        public ChessBoard Chessboard
        {
            get { return _chessboard; }
            set { _chessboard = value; }
        }

        /// <summary> 
        /// 棋子图片:抽象的属性 
        /// </summary> 
        public abstract Image ChessImage { get; }

        /// <summary> 
        /// 是否能够移动 
        /// </summary> 
        protected abstract bool CanMoveTo(ChessPoint p);

        /// <summary> 
        /// 移动方法 
        /// </summary> 
        public void MoveTo(ChessPoint targetPoint)
        {
            //目标棋子和当前棋子颜色不能一致 
            Chess targetChess = _chessboard[targetPoint];

            if (targetChess != null && targetChess.Color == this._color) return;

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

        /// <summary> 
        /// 获取两点之间的棋子数 
        /// </summary> 
        public int GetChessCount(ChessPoint start, ChessPoint end)
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