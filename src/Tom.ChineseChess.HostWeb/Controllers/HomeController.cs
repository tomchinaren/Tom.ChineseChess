using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tom.ChineseChess.HostWeb.Models;

namespace Tom.ChineseChess.HostWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Im()
        {
            ViewBag.commandListJson = GetCommandListJson();

            return View();
        }

        private string GetCommandListJson()
        {
            var list = new Dictionary<string, string> {
                { ActionType.Sit.ToString().ToLower(),new Sdk.Request.SquareSitRequest().GetApiName()},
                {  ActionType.Ready.ToString().ToLower(),new Sdk.Request.SquareReadyRequest().GetApiName()},
                {  ActionType.Move.ToString().ToLower(),new Sdk.Request.ChessMoveRequest().GetApiName()},
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}