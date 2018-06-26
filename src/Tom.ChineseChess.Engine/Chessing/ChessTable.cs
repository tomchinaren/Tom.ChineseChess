using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class ChessTable: ITable
    {
        private Dictionary<IChessPoint, IChess> _dict = new Dictionary<IChessPoint, IChess>();

        #region ITable
        IChess ITable.this[IChessPoint chessPoint]
        {
            get { return _dict.ContainsKey(chessPoint) ? _dict[chessPoint] : null; }
            set { _dict[chessPoint] = value; }
        }

        List<IChess> ITable.this[int a, int b]
        {
            get
            {
                var list = new List<IChess>();
                var keys = _dict.Keys.Where(k => k.X == a && k.Y == b).ToArray();
                foreach (var k in keys)
                {
                    list.Add(_dict[k]);
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
