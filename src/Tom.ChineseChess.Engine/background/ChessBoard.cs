using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class ChessBoard
    {
        private Dictionary<ChessPoint, Chess> dict = new Dictionary<ChessPoint, Chess>();

        public Chess this[ChessPoint chessPoint]
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
