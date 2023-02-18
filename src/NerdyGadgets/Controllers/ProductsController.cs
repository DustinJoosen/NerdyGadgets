using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Data;

namespace NerdyGadgets.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Where(p => p.Stock >= 1)
                .ToListAsync();

            return View(products);
        }

    }
}
