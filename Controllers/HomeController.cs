using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDFSv4.Models;

namespace TDFSv4.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var x = db.Clients.Include(x => x.Type).Include(x => x.Founders).ToList();
            return View(x);
        }
    }
}
