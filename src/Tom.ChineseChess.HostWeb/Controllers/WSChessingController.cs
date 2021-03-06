﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using Tom.Api.Request;
using Tom.Api.Response;
using Tom.ChineseChess.HostWeb.Models;

namespace Tom.ChineseChess.HostWeb.Controllers
{
    public class WSChessingController : ApiController
    {
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        static WebSocket socketFirst;
        [HttpGet]
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        private async Task ProcessWSChat(AspNetWebSocketContext arg)
        {
            WebSocket socket = arg.WebSocket;
            if (socketFirst == null)
            {
                socketFirst = socket;
            }
            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    string returnMessage = "You send :" + message + ". at" + DateTime.Now.ToLongTimeString();
                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                    var response = TryRevokeBiz(message);

                    await SendToFirst(socket, returnMessage + " to first");
                }
                else
                {
                    break;
                }
            }
        }

        private async Task SendToFirst(WebSocket socket, string msg)
        {
            if (socket == socketFirst)
            {
                return;
            }

            if (socketFirst.State == WebSocketState.Open)
            {
                var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                await socketFirst.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private IResponse TryRevokeBiz(string msg)
        {
            IResponse response = null;
            try
            {
                response = RevokeBiz(msg);
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        private IResponse RevokeBiz(string msg)
        {
            IResponse response = null;

            var serverUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"].ToString();
            IClient client = new DefaultClient(serverUrl, null, null);

            var dtoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UIMessageModel>(msg);

            switch (dtoModel.ActionType)
            {
                case ActionType.Sit:
                    {
                        var req = new Sdk.Request.SquareSitRequest() {
                             Biz_Content = Newtonsoft.Json.JsonConvert.SerializeObject(dtoModel.Data)
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