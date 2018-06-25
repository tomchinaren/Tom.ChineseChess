using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine.Util
{
    public class FirstPoint
    {
        private static FirstPoint _instance;
        public static FirstPoint GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FirstPoint();
                }
                return _instance;
            }
        }

        private List<Tuple<ChessType, IChessPoint>> _redList;
        private List<Tuple<ChessType, IChessPoint>> _blacklist;
        public List<Tuple<ChessType, IChessPoint>> RedList { get { return _redList; } }
        public List<Tuple<ChessType, IChessPoint>> Blacklist { get { return _blacklist; } }
        private FirstPoint()
        {
            _redList = GetList(Camp.RedCamp);
            _blacklist = GetList(Camp.BlackCamp);
        }

        private void Add(List<Tuple<ChessType, IChessPoint>> list, ChessType chessType, IChessPoint point)
        {
            list.Add(new Tuple<ChessType, IChessPoint>(chessType, point));
        }

        private List<Tuple<ChessType, IChessPoint>> GetList(Camp camp)
        {
            var list = new List<Tuple<ChessType, IChessPoint>>();
            Add(list, ChessType.Rooks,      new ChessPoint(0, 0));
            Add(list, ChessType.Knights,    new ChessPoint(0, 1));
            Add(list, ChessType.Elephants,  new ChessPoint(0, 2));
            Add(list, ChessType.Mandarins,  new ChessPoint(0, 3));
            Add(list, ChessType.King,       new ChessPoint(0, 4));
            Add(list, ChessType.Mandarins,  new ChessPoint(0, 5));
            Add(list, ChessType.Elephants,  new ChessPoint(0, 6));
            Add(list, ChessType.Knights,    new ChessPoint(0, 7));
            Add(list, ChessType.Rooks,      new ChessPoint(0, 8));

            Add(list, ChessType.Cannons, new ChessPoint(2, 1));
            Add(list, ChessType.Cannons, new ChessPoint(2, 7));
            Add(list, ChessType.Pawns, new ChessPoint(5, 0));
            Add(list, ChessType.Pawns, new ChessPoint(5, 2));
            Add(list, ChessType.Pawns, new ChessPoint(5, 4));
            Add(list, ChessType.Pawns, new ChessPoint(5, 6));
            Add(list, ChessType.Pawns, new ChessPoint(5, 8));

            foreach (var t in list)
            {
                SetAbsolutePoint(camp, t.Item2);
            }

            return list;
        }

        private void SetAbsolutePoint(Camp camp, IChessPoint point)
        {
            point.X = camp == Camp.RedCamp ? point.RelativeX : 8 - point.RelativeX;
            point.Y = camp == Camp.RedCamp ? point.RelativeY : 9 - point.RelativeY;
        }


    }
}
