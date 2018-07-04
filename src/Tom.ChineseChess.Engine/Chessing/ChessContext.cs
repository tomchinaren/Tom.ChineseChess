using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine.Chessing
{
    public class ChessContext : IChessContext
    {
        private Dictionary<long, ITable> _tables;
        private Dictionary<long, ISquare> _squares;

        public Dictionary<long, ITable> Tables
        {
            get
            {
                return _tables;
            }
        }

        public Dictionary<long, ISquare> Squares
        {
            get
            {
                return _squares;
            }
        }

        public ChessContext(params long[] tableIds)
        {
            Initialize(tableIds);
        }

        private void Initialize(long[] tableIds)
        {
            foreach(var id in tableIds)
            {
                ITable table = new ChessTable();
                _tables.Add(id, table);
            }
        }
    }
}
