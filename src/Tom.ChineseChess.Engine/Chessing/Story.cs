using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;

namespace Tom.ChineseChess.Engine
{

    public class StoryDemo
    {
        public void Run()
        {
            //stories:square do event, then table response
            //tom and jerry is square
            //tom and jerry sit at table 1
            //tom ready
            //jerry ready
            //a chessing start
            //begin while
            //tom move
            //jerry move
            //if kill king or give up then chessing stop and report
            //end while

            ISquare tom = new Square(Camp.RedCamp, ChessColor.Red);
            ISquare jerry = new Square(Camp.BlackCamp, ChessColor.Black);
            ITable table = new ChessTable();

            tom.Sit(table);
            jerry.Sit(table);
            tom.Ready();
            table.Clear();
            jerry.Ready();
            table.Start();
            IChess cannon = tom.GetChess(ChessType.Cannons);
            IChess cannon2 = jerry.GetChess(ChessType.Cannons);
            var flag = false;
            flag = cannon.MoveTo(new ChessPoint(2, 5) { X = 2, Y = 5 });
            flag = cannon2.MoveTo(new ChessPoint(2, 5) { X = 2, Y = 5 });

            table.Stop();
            table.Report();
        }

    }

    public interface ISquare
    {
        Camp Camp { get; }
        ChessColor Color { get; }
        ITable Table { get; }
        void Sit(ITable table);
        void Ready();
        IChess GetChess(ChessType chessType, int index = 0);
    }

    public interface IChess
    {
        ChessType ChessType { get; }
        ISquare Square { get; }
        bool MoveTo(IChessPoint chessPoint);
    }
    public interface IChessPoint
    {
        int X { get; set; }
        int Y { get; set; }
        int RelativeX { get; set; }
        int RelativeY { get; set; }
    }
    public interface ITable
    {
        void Start();
        void Stop();
        void Report();
        void Clear();

        IChess this[IChessPoint chessPoint] { get; set; }
        List<IChess> this[int a, int b] { get;}

    }

}
