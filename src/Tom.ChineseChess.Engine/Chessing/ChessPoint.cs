using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine
{
    public class ChessPoint: IChessPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int RelativeX { get; set; }
        public int RelativeY { get; set; }

        public ChessPoint(int relativeX, int relativeY)
        {
            RelativeX = relativeX;
            RelativeY = relativeY;
        }

        public ChessPoint(int relativeX, int relativeY, int x, int y)
        {
            RelativeX = relativeX;
            RelativeY = relativeY;
            X = x;
            Y = y;
        }

        public ChessPoint(Camp camp, int relativeX, int relativeY)
        {
            RelativeX = relativeX;
            RelativeY = relativeY;
            X = camp == Camp.RedCamp ? RelativeX : 8 - RelativeX;
            Y = camp == Camp.RedCamp ? RelativeY : 9 - RelativeY;
        }

    }
}
