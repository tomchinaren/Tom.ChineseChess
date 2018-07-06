using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine.Chessing
{
    public class ChessingManager : IChessContext
    {
        private Dictionary<long, ITable> _tables = new Dictionary<long, ITable>();
        private Dictionary<long, ISquare> _squares = new Dictionary<long, ISquare>();

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

        #region singleton and init
        private static ChessingManager instance;
        public static ChessingManager Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("must init before using!");
                }
                return instance;
            }
        }
        private ChessingManager() { }
        public static void Init(params long[] tableIds)
        {
            if (instance == null)
            {
                instance = new ChessingManager();
            }

            instance.InitTables(tableIds);
        }
        private void InitTables(long[] tableIds)
        {
            foreach (var id in tableIds)
            {
                ITable table = new ChessTable();
                _tables.Add(id, table);
            }
        }
        #endregion

        public void AddSquare(long userID, ISquare square)
        {
            if (!_squares.Keys.Contains(userID))
            {
                _squares.Add(userID, square);
            }
        }
    }
}
