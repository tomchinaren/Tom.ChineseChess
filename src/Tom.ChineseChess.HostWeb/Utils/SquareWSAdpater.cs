using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tom.Api.Request;
using Tom.Api.Response;
using Tom.ChineseChess.HostWeb.Models;

namespace Tom.ChineseChess.HostWeb.Utils
{
    public class SquareWSAdpater
    {

        public async Task Running(WebSocket socket)
        {
            if (socket.State != WebSocketState.Open)
            {
                return;
            }

            var message = await ReceiveMessage(socket);
            var messageModel = Analyze(message);
            var response = TryAction(messageModel);
            var notifyMessages = ProduceMessages(response);
            await Notify(notifyMessages);
        }

        private async Task Notify(Dictionary<WebSocket, string> notifyMessages)
        {
            foreach (var sk in notifyMessages.Keys)
            {
                var msg = notifyMessages[sk];
                await SendMessage(sk, msg);
            }
        }

        private Dictionary<WebSocket, string> ProduceMessages(IResponse response)
        {
            var dict = new Dictionary<WebSocket, string>();
            return dict;
        }

        private async Task<string> ReceiveMessage(WebSocket socket)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
            string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            return message;
        }

        private UIMessageModel Analyze(string message)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<UIMessageModel>(message);
            if (model == null)
            {
                throw new Exception("error message fomat!");
            }
            return model;
        }

        private Task SendMessage(WebSocket socket, string message)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            return socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }


        private IResponse TryAction(UIMessageModel messageModel)
        {
            IResponse response = null;
            try
            {
                response = Action(messageModel);
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        private IResponse Action(UIMessageModel messageModel)
        {
            IResponse response = null;

            var serverUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"].ToString();
            IClient client = new DefaultClient(serverUrl, null, null);

            switch (messageModel.ActionType)
            {
                case ActionType.Sit:
                    {
                        var req = new Sdk.Request.SquareSitRequest()
                        {
                            Biz_Content = Newtonsoft.Json.JsonConvert.SerializeObject(messageModel.Data)
                        };
                        response = client.Execute(req);
                    }
                    break;

                case ActionType.Ready:
                    break;

                case ActionType.Move:
                    break;

                default:
                    break;
            }

            return response;
        }

    }
}