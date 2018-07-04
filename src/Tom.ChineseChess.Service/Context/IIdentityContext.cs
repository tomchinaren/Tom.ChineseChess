using Tom.ChineseChess.Engine;

namespace Tom.ChineseChess.Service.Context
{
    public interface IIdentityContext
    {
        IUserInfo UserInfo { get; }
        ISquare Square { get; }
    }
}
