using System;
using System.Collections.Generic;
using Tom.Api.Enum;
using Tom.Api.Response;
using Tom.ChineseChess.Engine.Exceptions;

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

        public static TOut TryGetResponse<TIn,TOut>(TIn request, Func<TIn,TOut> func) where TOut: IResponse
        {
            var res = Activator.CreateInstance<TOut>();
            try
            {
                res = func(request);
            }
            catch(ChessException ex)
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
    }
}