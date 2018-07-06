using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Engine;

namespace Tom.ChineseChess.Service.Context
{
    public class IdentityContext : IIdentityContext
    {
        private ISquare square;
        private IUserInfo userInfo;
        public IdentityContext(ISquare square, IUserInfo userInfo)
        {
            this.square = square;
            this.userInfo = userInfo;
        }
        public ISquare Square
        {
            get
            {
                return square;
            }
        }

        public IUserInfo UserInfo
        {
            get
            {
                return userInfo;
            }
        }

        public void SetUserInfo(IUserInfo userInfo)
        {
            this.userInfo = userInfo;
        }
    }
}
