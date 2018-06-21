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
            //stories
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


            ISquare tom = null;
            ISquare jerry = null;
            ITable table = null;
            IChessing chessing = null;

            tom.Sit(table);
            jerry.Sit(table);
            tom.Ready();
            chessing.Clear();
            jerry.Ready();
            chessing.Start();
            IChess cannon = tom.GetChess(ChessType.Cannons) ;
            IChess cannon2 = jerry.GetChess(ChessType.Cannons);
            IChessPoint chessPoint = null;
            cannon.MoveTo(chessPoint);
            cannon2.MoveTo(chessPoint);

            chessing.Stop();
            chessing.Report();
        }

        class SquareDemo : ISquare
        {
            public IChess GetChess(ChessType chessType, int index = 0)
            {
                throw new NotImplementedException();
            }

            public void MoveTo(IChess chess, IChessPoint chessPoint)
            {
                Console.WriteLine("{0} {1} {2}", "MoveTo", chess.ToString(), chessPoint.ToString());
            }

            public void Ready()
            {
                throw new NotImplementedException();
            }

            public void Sit(ITable table)
            {
                throw new NotImplementedException();
            }
        }
    }

    public interface ISquare
    {
        void Sit(ITable table);
        void Ready();
        IChess GetChess(ChessType chessType, int index = 0);
    }

    public interface ITable
    {

    }
    public interface IChess
    {
        ISquare Square { get; }
        void MoveTo(IChessPoint chessPoint);
    }
    public interface IChessPoint
    {
        int X { get; set; }
        int Y { get; set; }
    }
    public interface IChessing
    {
        void Start();
        void Stop();
        void Report();
        void Clear();
    }

}
