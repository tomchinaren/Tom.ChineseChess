
//6.将/帅(King)

//移动规则: 1.“将”不能出城 2.“将”每次只能走一步 

using System;
using System.Collections.Generic;
using System.Text;
using Tom.ChineseChess.Engine;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 将/帅 
    /// </summary> 
    public class King : Chess
    {
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public King(ISquare square, ChessColor color, ITable board)
        : base(square, color, new ChessPoint() { X = square.Camp== Enums.Camp.RedCamp? 0:9, Y = 5 }, board)
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
                //红将 
                if (this._color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 4);
                //黑将 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 11);
            }
        }

        /// <summary> 
        /// 将移动算法 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint tragPoint)
        {
            //“将”不能出城 
            if (!((tragPoint.X >= 3 && tragPoint.X <= 5) && (tragPoint.Y <= 2 || tragPoint.Y >= 7)))
                return false;

            //“将”每次只能走一步 
            if ((Math.Abs(tragPoint.X - this._currentPoint.X) + Math.Abs(tragPoint.Y - this._currentPoint.Y)) != 1)
                return false;

            return true;
        }

    }
}
