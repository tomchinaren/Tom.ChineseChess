//3.马(Knights)

//学习马移动算法
//移动规则:蹩脚算法,直线一侧不能有棋子

using System;
using System.Collections.Generic;
using System.Text;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 马 
    /// </summary> 
    public class Knights : Chess
    {

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Knights(ISquare square, ChessColor color, IChessPoint tragPoint, ChessBoard board)
        : base(square, color, tragPoint, board)
        {
            // 
        }

        /// <summary> 
        /// 象棋图片 
        /// </summary> 
        public override System.Drawing.Image ChessImage
        {
            get
            {
                //红马 
                if (this._color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 2);
                //黑马 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 9);
            }
        }

        /// <summary> 
        /// 马移动算法 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint p)
        {
            //蹩脚算法 
            //横向移动 
            if (Math.Abs(_currentPoint.X - p.X) == 2 && Math.Abs(_currentPoint.Y - p.Y) == 1)
            {
                if (_chessboard[(_currentPoint.X + p.X) / 2, _currentPoint.Y] == null)
                    return true;
            }
            if (Math.Abs(_currentPoint.X - p.X) == 1 && Math.Abs(_currentPoint.Y - p.Y) == 2)
            {
                if (_chessboard[_currentPoint.X, (_currentPoint.Y + p.Y) / 2] == null)
                    return true;
            }
            return false;
        }
    }
}