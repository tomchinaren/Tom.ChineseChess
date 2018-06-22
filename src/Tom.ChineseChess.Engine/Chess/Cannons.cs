
//7.炮/砲(Connons)
//移动规则: 1.两点在一条直线上 2.目标点不为空,中间只能由一个棋子  3.目标点为空,中间不能有棋子 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine
{
    /// <summary> 
    /// 炮 
    /// </summary> 
    public class Cannons : Chess
    {


        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public Cannons(ISquare square, ChessColor color, ChessBoard board, int index)
        : base(square, color, new ChessPoint(), board)
        {
            // 
            if(index!=0 && index != 1)
            {
                throw new Exception("index error!");
            }
            _firstPoint = new ChessPoint() { X = 2, Y = 2 + index * 5 };
        }

        /// <summary> 
        /// 重写图片的属性 
        /// </summary> 
        public override Image ChessImage
        {
            get
            {
                //红炮 
                if (_color == ChessColor.Red)
                    return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 3);
                //黑炮 
                return ImageHelper.GetImageByAverageIndex(ChineseChess.Res.Properties.Resources.xchess, 14, 10);
            }
        }

        private IChessPoint _firstPoint;
        public override IChessPoint FirstPoint
        {
            get
            {
                return _firstPoint;
            }
        }

        /// <summary> 
        /// 棋子是否能够移动到目标点 
        /// </summary> 
        protected override bool CanMoveTo(IChessPoint targPoint)
        {
            //两点在一条直线上 
            if (targPoint.X != _currentPoint.X && targPoint.Y != _currentPoint.Y)
                return false;

            //目标点不为空,中间只能由一个棋子 
            if (_chessboard[targPoint] != null && GetChessCount(_currentPoint, targPoint) != 1)
                return false;

            //目标点为空,中间不能有棋子 
            if (_chessboard[targPoint] == null && GetChessCount(_currentPoint, targPoint) > 0)
                return false;

            return true;

        }
    }
}
