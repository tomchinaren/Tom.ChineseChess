//5.士/仕(Mandarins)

//移动规则:1.“士”不能出城 2.“士”每次只能走一步且只能是斜线

using System;
using System.Collections.Generic;
using System.Text;
using Tom.ChineseChess.Engine;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 士 
    /// </summary> 
    public class Mandarins : Chess
    {
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Mandarins(ISquare square, ChessColor color, IChessPoint tragPoint, ChessBoard board)
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
                //红士 
                if (this._color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 5);
                //黑士 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 12);
            }
        }

        /// <summary> 
        /// 兵移动算法 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint tragPoint)
        {
            //“士”不能出城 
            if (!((tragPoint.X >= 3 && tragPoint.X <= 5) && (tragPoint.Y <= 2 || tragPoint.Y >= 7)))
                return false;

            //“士”每次只能走一步且只能是斜线 
            if (!(Math.Abs(tragPoint.X - this._currentPoint.X) == 1 && Math.Abs(tragPoint.Y - this._currentPoint.Y) == 1))
                return false;

            return true;
        }
    }
}
