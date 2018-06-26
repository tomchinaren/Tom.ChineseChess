using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine.Util
{
    public class FirstPoints
    {
        #region singleton
        private static FirstPoints _instance;
        public static FirstPoints GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FirstPoints();
                }
                return _instance;
            }
        }
        private FirstPoints()
        {
            Init();
        }
        #endregion

        private Dictionary<ChessType, List<IChessPoint>> _redList;
        private Dictionary<ChessType, List<IChessPoint>> _blacklist;
        private Dictionary<ChessType, List<IChessPoint>> RedList { get { return _redList; } }
        private Dictionary<ChessType, List<IChessPoint>> Blacklist { get { return _blacklist; } }
        public Dictionary<ChessType, List<IChessPoint>> GetList(Camp camp)
        {
            var list = (camp == Enums.Camp.RedCamp) ? FirstPoints.GetInstance.RedList : FirstPoints.GetInstance.Blacklist;
            return list;
        }

        private void Init()
        {
            _redList = GetInitList(Camp.RedCamp);
            _blacklist = GetInitList(Camp.BlackCamp);
        }

        private Dictionary<ChessType, List<IChessPoint>> GetInitList(Camp camp)
        {
            var dict = new Dictionary<ChessType, List<IChessPoint>>();
            dict.Add(ChessType.Rooks,       new List<IChessPoint>() { new ChessPoint(0, 0), new ChessPoint(0, 8) });
            dict.Add(ChessType.Knights,     new List<IChessPoint>() { new ChessPoint(0, 1), new ChessPoint(0, 7) });
            dict.Add(ChessType.Elephants,   new List<IChessPoint>() { new ChessPoint(0, 2), new ChessPoint(0, 6) });
            dict.Add(ChessType.Mandarins,   new List<IChessPoint>() { new ChessPoint(0, 3), new ChessPoint(0, 5) });
            dict.Add(ChessType.King,        new List<IChessPoint>() { new ChessPoint(0, 4) });
            dict.Add(ChessType.Cannons,     new List<IChessPoint>() { new ChessPoint(2, 1), new ChessPoint(2, 7) });
            dict.Add(ChessType.Pawns,       new List<IChessPoint>() { new ChessPoint(3, 0), new ChessPoint(3, 2), new ChessPoint(3, 4), new ChessPoint(3, 6), new ChessPoint(3, 8) });

            if(camp== Camp.RedCamp) {
                return dict;
            }

            //black camp
            foreach (var t in dict.Values)
            {
                foreach(var sub in t)
                {
                    SetAbsolutePoint(camp, sub);
                }
            }
            return dict;
        }

        private void SetAbsolutePoint(Camp camp, IChessPoint point)
        {
            point.X = camp == Camp.RedCamp ? point.RelativeX : 8 - point.RelativeX;
            point.Y = camp == Camp.RedCamp ? point.RelativeY : 9 - point.RelativeY;
        }

    }
}
