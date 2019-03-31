using System;
using Microsoft.AspNetCore.Mvc;

namespace TasksBoard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
