using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine.Enums;
using Tom.ChineseChess.Engine.Exceptions;

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
            tom.Logger = new LogDemo();
            jerry.Logger = tom.Logger;
            ITable table = new ChessTable();

            tom.Sit(table);
            jerry.Sit(table);

            var i = 0;
            Camp lastCamp = 0;
            foreach(var t in table.ChessList)
            {
                if(lastCamp != t.Value.Square.Camp)
                {
                    i = 0;
                    Console.WriteLine("-----------");
                }
                lastCamp = t.Value.Square.Camp;
                Console.WriteLine("{0}) {1} {2} {3} {4} {5} {6}",i++, lastCamp, t.Key.RelativeX, t.Key.RelativeY, t.Key.X, t.Key.Y, t.Value.ChessType);
            }
            Console.WriteLine("-----------");

            tom.Ready();
            //table.Clear();
            jerry.Ready();
            //table.Start();
            IChess cannon = tom.GetChess(ChessType.Cannons);
            IChess cannon2 = jerry.GetChess(ChessType.Cannons);
            var flag = false;
            try
            {
                flag = cannon.MoveTo(new ChessPoint(cannon.Square.Camp, 4, 2));//2 5
                flag = cannon2.MoveTo(new ChessPoint(cannon2.Square.Camp, 4, 2));// 2 5
                flag = cannon.MoveTo(new ChessPoint(cannon.Square.Camp, 4, 6));//5 j 4
                flag = cannon2.MoveTo(new ChessPoint(cannon2.Square.Camp, 4, 1));// 2 5
                flag = cannon.MoveTo(new ChessPoint(cannon.Square.Camp, 4, 9));//5 j 4
            }
            catch (GameLoseException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //table.Stop();
            //table.Report();
        }

        public class LogDemo : ILog
        {
            public void LogInfo(string msg)
            {
                Console.WriteLine(msg);
            }
        }

    }

    public interface IChessContext
    {
        Dictionary<long, ITable> Tables { get; }
        Dictionary<long, ISquare> Squares { get; }
    }

    public interface ISquare
    {
        Camp Camp { get; }
        ChessColor Color { get; }
        ITable Table { get; }
        void Sit(ITable table);
        void Ready();
        IChess GetChess(ChessType chessType, int index = 0);
        ILog Logger { get; set; }
    }

    public interface IChess
    {
        ChessType ChessType { get; }
        ISquare Square { get; }
        IChessPoint CurrentPoint { get; }
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
        Dictionary<IChessPoint, IChess> ChessList { get; }
    }

    public interface ILog
    {
        void LogInfo(string msg);
    }

}
