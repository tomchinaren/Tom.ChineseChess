//2.车(Rooks)

//学习车移动算法CanMoveTo.移动规则: 1.两点在一条直线上 ,2.中间不能有棋子

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 车 
    /// </summary> 
    public class Rooks : Chess
    {
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Rooks(ISquare square, ChessColor color, IChessPoint tragPoint, ChessBoard board)
        : base(square, color, tragPoint, board)
        {
            // 
        }

        /// <summary> 
        /// 重写图片的属性 
        /// </summary> 
        public override Image ChessImage
        {
            get
            {
                //红车 
                if (_color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 1);
                else //黑车
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 8);
            }
        }

        /// <summary> 
        /// 棋子是否能够移动到目标点 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint targetPoint)
        {
            //两点在一条直线上 
            if (targetPoint.X != _currentPoint.X && targetPoint.Y != _currentPoint.Y)
                return false;

            //中间不能有棋子 
            if (GetChessCount(_currentPoint, targetPoint) > 0)
                return false;

            return true;
        }

    }
}
