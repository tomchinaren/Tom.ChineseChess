using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tom.ChineseChess.HostWeb.Models
{
    public class CommandModel
    {
        public CommandType CommandType { get; set; }
        public string ApiName { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}