using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine.Exceptions
{
    public class ChessException : Exception
    {
        private string code;

        /// <summary>
        /// 错误码
        /// 对应 ErrCode
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public ChessException(string code, string message)
            : base(message)
        {

        }
    }

}
