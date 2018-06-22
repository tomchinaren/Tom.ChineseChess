using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine.Enums
{
    /// <summary>
    /// Status用于指示是否可用，有：正常、删除、启用、禁用；而State用于流程状态流转。
    /// </summary>
    public enum Status
    {

    }

    public enum ChessType
    {
        /// <summary>
        /// 将
        /// </summary>
        King,
        /// <summary>
        /// 士
        /// </summary>
        Mandarins,
        /// <summary>
        /// 象
        /// </summary>
        Elephants,
        /// <summary>
        /// 马
        /// </summary>
        Knights,
        /// <summary>
        /// 车
        /// </summary>
        Rooks,
        /// <summary>
        /// 炮
        /// </summary>
        Cannons,
        /// <summary>
        /// 兵
        /// </summary>
        Pawns
    }

    public enum SquareState
    {
        Init = 0,
        Sited,
        Left,
        Ready,
        Started,
        Stoped,
    }

    /// <summary>
    /// 阵营
    /// </summary>
    public enum Camp
    {
        RedCamp,
        BlackCamp

    }
}
