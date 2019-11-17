using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tom.Api.Enum;
using Tom.Api.Request;
using Tom.Api.Response;
using Tom.ChineseChess.Engine;
using Tom.ChineseChess.Engine.Chessing;
using Tom.ChineseChess.Engine.Exceptions;
using Tom.ChineseChess.Sdk.Request;
using Tom.ChineseChess.Service.Context;

namespace Tom.ChineseChess.Service.Util
{
    public static class Utils
    {
        public static Dictionary<string, string> GetRequestDict(string bizContent)
        {
            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(bizContent);
            return dict;
        }
        public static string GetRequestParam(string bizContent, string paramName)
        {
            var dict = GetRequestDict(bizContent);
            return GetRequestParam(dict,paramName);
        }

        public static string GetRequestParam(Dictionary<string, string> paramDict, string paramName)
        {
            var dict = paramDict;
            if (dict == null || !dict.ContainsKey(paramName))
            {
                throw new Exception("bizContent error!");
            }
            return dict[paramName];
        }
        public static T GetRequestParam<T>(string bizContent, string paramName) where T : struct
        {
            var dict = GetRequestDict(bizContent);
            return GetRequestParam<T>(dict, paramName);
        }
        public static T GetRequestParam<T>(Dictionary<string, string> paramDict, string paramName) where T : struct
        {
            var dict = paramDict;
            var strVal = GetRequestParam(dict, paramName);

            object obj;
            T val = default(T);
            if (typeof(T) == typeof(int))
            {
                int intVal;
                if (!int.TryParse(strVal, out intVal))
                {
                    throw new Exception(string.Format("error!param {0} is not int!", paramName));
                }
                obj = intVal;
                val = (T)obj;
            }
            else if (typeof(T) == typeof(System.Enum))
            {
                if (!System.Enum.TryParse<T>(strVal, true, out val))
                {
                    throw new Exception(string.Format("error! param {0} is not {1}!", paramName, typeof(T).ToString()));
                }
            }
            else
            {
                throw new Exception(string.Format("error!get param {0} with unsupported type!", paramName));
            }
            return val;
        }

        public static TOut TryGetResponse<TIn,TOut>(TIn request, Func<TIn, TOut> func, long userID, bool check=true)
            where TOut: IResponse 
            where TIn: IRequest<TOut>
        {
            var res = Activator.CreateInstance<TOut>();
            try
            {
                //var userID = GetUserIDByToken(request.Session);
                //var square = ChessingManager.Instance.Squares[userID];
                //var identity = new IdentityContext(square, null);
                //identitySetter.SetIdentity(identity);

                if (check)
                {
                    if (string.IsNullOrEmpty(request.Biz_Content))
                    {
                        throw new Exception("request参数为空");
                    }
                    if (string.IsNullOrEmpty(request.Biz_Content))
                    {
                        throw new Exception("biz_content参数为空");
                    }
                }

                res = func(request);

                var square = ChessingManager.Instance.Squares[userID];
                SetDebugInfo(request, res, square);
            }
            catch (ChessException ex)
            {
                res.Code = ex.Code;
                res.Msg = ex.Message;
            }
            catch (Exception ex)
            {
                res.Code = ((int)ReturnCode.Error).ToString();
                res.Msg = ex.Message;
            }

            return res;
        }

        public static void SetDebugInfo<T>(IRequest<T> request, T response, ISquare square) where T: IResponse
        {
            if (request.Debug)
            {
                response.DebugInfo = GetChessListDebugInfo(square);
            }
        }

        public static string GetChessListDebugInfo(ISquare square)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("-----begin Camp {0}-----\r\n", square.Camp));

            var i = 0;
            var list = square.Table.ChessList.Where(t => t.Value.Square == square);
            foreach (var t in list)
            {
                sb.Append(string.Format("{0}) {1} {2} {3} {4} {5} {6}\r\n", i++, square.Camp, t.Key.RelativeX, t.Key.RelativeY, t.Key.X, t.Key.Y, t.Value.ChessType));
            }
            sb.Append(string.Format("-----end Camp {0}-----\r\n", square.Camp));
            return sb.ToString();
        }

        public static long GetUserIDByToken(string token)
        {
            long userID;
            if (!long.TryParse(token, out userID))
            {
                throw new Exception("token param error!");
            }
            return userID;
        }
    }
}