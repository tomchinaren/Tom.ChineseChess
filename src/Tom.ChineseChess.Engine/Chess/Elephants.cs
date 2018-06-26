
//4.象/相(Elephants) 学习移动算法

//移动规则:1.田子中间不能有棋子 2.不能过河

using System;
using System.Collections.Generic;
using System.Text;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 象--相 
    /// </summary> 
    public class Elephants : Chess
    {
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Elephants(ISquare square, IChessPoint point)
        : base(square, point)
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
                //红相 
                if (this._color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 6);
                //黑象 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 13);
            }
        }

        public override ChessType ChessType
        {
            get
            {
                return ChessType.Elephants;
            }
        }

        /// <summary> 
        /// 象移动算法 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint p)
        {
            //绝对值法 
            if (Math.Abs(_currentPoint.X - p.X) != 2 || Math.Abs(_currentPoint.Y - p.Y) != 2)
                return false;

            //中间不能有棋子 
            if (Table[(_currentPoint.X + p.X) / 2, (_currentPoint.Y + p.Y) / 2] != null)
                return false;

            //越界算法 
            if (_currentPoint.Y <= 4 && p.Y > 4) return false;
            if (_currentPoint.Y >= 5 && p.Y < 5) return false;

            return true;
        }
    }
}