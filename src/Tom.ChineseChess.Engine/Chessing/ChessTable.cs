using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class ChessTable: ITable
    {
        private Dictionary<IChessPoint, IChess> dict = new Dictionary<IChessPoint, IChess>();

        #region ITable
        IChess ITable.this[IChessPoint chessPoint]
        {
            get { return dict.ContainsKey(chessPoint) ? dict[chessPoint] : null; }
            set { dict[chessPoint] = value; }
        }

        List<IChess> ITable.this[int a, int b]
        {
            get
            {
                var list = new List<IChess>();
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

        void ITable.Clear()
        {
            //throw new NotImplementedException();
        }

        void ITable.Report()
        {
            throw new NotImplementedException();
        }

        void ITable.Start()
        {
            //throw new NotImplementedException();
        }

        void ITable.Stop()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
