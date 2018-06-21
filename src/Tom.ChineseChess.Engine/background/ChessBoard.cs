using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class ChessBoard
    {
        private Dictionary<IChessPoint, Chess> dict = new Dictionary<IChessPoint, Chess>();

        public Chess this[IChessPoint chessPoint]
        {
            get { return dict[chessPoint]; }
            set { dict[chessPoint] = value; }
        }

        public List<Chess> this[int a, int b]
        {
            get
            {
                var list = new List<Chess>();
                var keys = dict.Keys.Where(k => k.X == a && k.Y == b).ToArray();
                foreach (var k in dict.Keys)
                {
                    list.Add(dict[k]);
                }
                if (list.Count == 0)
                {
                    return null;
                }
                return list;
            }
        }
    }
}
