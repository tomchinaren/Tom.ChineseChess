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
            get
            {
                var key = _dict.Keys.FirstOrDefault(k => k.X == chessPoint.X && k.Y == chessPoint.Y);
                return key!=null ? _dict[key] : null;
            }
            set
            {
                var key = _dict.Keys.FirstOrDefault(k => k.X == chessPoint.X && k.Y == chessPoint.Y);

                key = key != null ? key : chessPoint;
                _dict[key] = value;

                if (value == null)//吃掉或移动
                {
                    _dict.Remove(key);
                }
            }
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

        Dictionary<IChessPoint, IChess> ITable.ChessList
        {
            get
            {
                return _dict;
            }
        }

        void ITable.Clear()
        {
            throw new NotImplementedException();
        }

        void ITable.Report()
        {
            throw new NotImplementedException();
        }

        void ITable.Start()
        {
            throw new NotImplementedException();
        }

        void ITable.Stop()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
