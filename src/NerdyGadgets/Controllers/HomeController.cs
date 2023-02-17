using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Data;
using NerdyGadgets.Models;
using System.Diagnostics;


namespace NerdyGadgets.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}