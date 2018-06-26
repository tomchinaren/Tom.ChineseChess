using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
