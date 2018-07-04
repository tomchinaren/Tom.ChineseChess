using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;
using Tom.ChineseChess.Engine.Exceptions;

namespace Tom.ChineseChess.Engine
{
    public abstract class Chess : IChess
    {
        private ISquare _square;
        protected ITable Table { get { return _square.Table; } }
        protected ChessColor _color;
        protected IChessPoint _lastPoint;
        protected IChessPoint _currentPoint;
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
        public IChessPoint CurrentPoint
        {
            get { return _currentPoint; }
        }
        public Chess(ISquare square, IChessPoint point)
        {
            _square = square;
            this._color = square.Color;
            this._currentPoint = point;
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

        public abstract ChessType ChessType { get; }

        bool IChess.MoveTo(IChessPoint targetPoint)
        {
            var flag = PureMoveTo(targetPoint);
            Log(_lastPoint, targetPoint, flag);
            return flag;
        }
        private void Log(IChessPoint lastPoint, IChessPoint targetPoint, bool flag)
        {
            if (_square.Logger == null)
            {
                return;
            }
            var msg = GetMoveMsg(this.Square.Camp, this.ChessType, lastPoint.RelativeX, lastPoint.RelativeY, targetPoint.RelativeX, targetPoint.RelativeY, flag);
            _square.Logger.LogInfo(msg);
        }
        bool PureMoveTo(IChessPoint targetPoint)
        {
            //目标棋子和当前棋子颜色不能一致 
            IChess targetChess = Table[targetPoint];
            if (targetChess != null && targetChess.Square == this.Square) return false;

            //是否满足规则 
            if (!CanMoveTo(targetPoint)) return false;

            //吃掉对方老王 
            if (Table[targetPoint] is King)
            {
                Log(_currentPoint, targetPoint, true);
                throw new GameLoseException(this.Color == ChessColor.Red ? "红方胜" : "黑方胜");
            }

            //移动 
            Table[_currentPoint] = null; //吃掉棋子或移动棋子 
            Table[targetPoint] = this;

            _lastPoint = _currentPoint;
            _currentPoint = targetPoint;
            return true;
        }
        #endregion

        private string GetMoveMsg(Camp camp, ChessType chessType, int relativeX, int relativeY, int tRelativeX, int tRelativeY, bool flag)
        {
            var action = "";
            if(tRelativeY == relativeY)
            {
                action = string.Format("{0}平{1} {2}", relativeX+1, tRelativeX + 1, flag);
            }
            else if(tRelativeY > relativeY)
            {
                if (chessType == ChessType.Knights || chessType == ChessType.Mandarins || chessType == ChessType.Elephants)
                {
                    action = string.Format("{0}进{1} {2}", relativeX + 1, tRelativeX +1, flag);
                }
                else
                {
                    action = string.Format("{0}进{1} {2}", relativeX + 1, tRelativeY - relativeY, flag);
                }
            }
            else
            {
                if (chessType == ChessType.Knights || chessType == ChessType.Mandarins || chessType == ChessType.Elephants)
                {
                    action = string.Format("{0}退{1} {2}", relativeX + 1, tRelativeX + 1, flag);
                }
                else
                {
                    action = string.Format("{0}退{1} {2}", relativeX + 1, relativeY - tRelativeY, flag);
                }
            }

            var msg = string.Format("{0} {1} {2}", camp, chessType, action);
            return msg;
        }

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
                    if (Table[i, start.Y] != null)
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
                    if (Table[start.X, i] != null)
                        count++;
                }
                return count;
            }
        }
    }
}
