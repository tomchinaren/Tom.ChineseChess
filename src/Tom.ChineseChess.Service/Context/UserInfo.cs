using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Service.Context
{
    public class UserInfo : IUserInfo
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
    }
}
