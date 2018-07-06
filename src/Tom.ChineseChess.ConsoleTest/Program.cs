using System.Collections.Generic;
using Tom.Api.Request;
using Tom.ChineseChess.Engine;
using Tom.ChineseChess.Sdk.Enum;
using Tom.ChineseChess.Sdk.Request;

namespace Tom.ChineseChess.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var demo = new StoryDemo();
            //demo.Run();

            Demo();
        }

        static void Demo()
        {
            var serverUrl = "http://localhost:28310/";
            IClient client = new DefaultClient(serverUrl, null, null);

            var sitReq = new SquareSitRequest()
            {
                BizContent = Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "table_id","1"}
                })
            };
            var sitRes = client.Execute(sitReq,"1");

            var readyReq = new SquareReadyRequest();
            var readyRes = client.Execute(readyReq);

            var moveReq1 = new ChessMoveRequest()
            {
                BizContent = Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "chesstype", ChessType.Cannons.ToString().ToLower()},
                    { "index","0"},
                    { "relativex","4"},
                    { "relativey","2"},
                })
            };
            var moveRes1 = client.Execute(moveReq1);

            //stories
            //tom and jerry sit at board 1
            //tom ready
            //jerry ready
            //board start


            //var red = new Square("tom");
            //var black = new Square("jerry");
            //var table = new Table();

            //var ing = new Chessing("tom vs jerry at 2018.6.20 17:40");

            //table.StartChessing(ing);

            //ing.Start(true);//at red


        }
    }
}
