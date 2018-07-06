using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom.ChineseChess.Service.Context;

namespace Tom.ChineseChess.Api.HostWeb.Filter
{
    public interface IContextContainer
    {
        IIdentityContext Identity { get; set; }
    }
}
