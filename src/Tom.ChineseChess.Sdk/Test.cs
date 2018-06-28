using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.Api.Request;

namespace Tom.ChineseChess.Sdk
{
    class Test
    {
        public static void Main()
        {
            IClient client = new Tom.Api.Request.DefaultClient(null, null, null, null, null, null, null);
        }
    }
}
