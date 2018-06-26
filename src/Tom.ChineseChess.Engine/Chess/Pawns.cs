
//8.兵/卒(Pawns)

//移动规则:1.兵不能往后走 2.兵一步一步走 3.过河前只能向前走

using System;
using System.Collections.Generic;
using System.Text;
using Tom.ChineseChess.Engine;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 兵 
    /// </summary> 
    public class Pawns : Chess
    {
        //是否过河 
        private bool _isRiverd = false;
        private int _step = 0; //记录步数 

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Pawns(ISquare square, IChessPoint point)
        : base(square, point)
        {
            if (_currentPoint.Y > 4)
                _step = -1;
            else
                _step = 1;
        }

        /// <summary> 
        /// 象棋图片 
        /// </summary> 
        public override System.Drawing.Image ChessImage
        {
            get
            {
                //红兵 
                if (this._color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 7);
                //黑卒 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 14);
            }
        }

        public override ChessType ChessType
        {
            get
            {
                return ChessType.Mandarins;
            }
        }

        /// <summary> 
        /// 兵移动算法 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint p)
        {
            //兵不能往后走 
            if (p.Y - _currentPoint.Y == -_step)
                return false;

            //兵一步一步走 
            if (Math.Abs(_currentPoint.X - p.X) + Math.Abs(_currentPoint.Y - p.Y) != 1)
                return false;

            //过河 
            if (!_isRiverd)
            {
                if (_currentPoint.Y == 4 && p.Y == 5) _isRiverd = true;
                if (_currentPoint.Y == 5 && p.Y == 4) _isRiverd = true;
            }

            //过河前只能向前走 
            if (!_isRiverd)
            {
                if (p.Y - _currentPoint.Y != _step)
                    return false;
            }

            return true;
        }
    }
}