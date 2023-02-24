using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Data;
using NerdyGadgets.Models;

namespace NerdyGadgets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductSpecs)
                .FirstOrDefaultAsync(m => m.ProductNumber == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> DeleteSpec(int prod, string spec)
        {
            if (prod == 0 || spec == null)
            {
                return NotFound();
            }

            var productspec = await _context.ProductSpecs
                .SingleOrDefaultAsync(p => p.ProductNumber == prod && p.SpecName == spec);

            if (productspec == null)
            {
                return NotFound();
            }

            _context.ProductSpecs.Remove(productspec);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = prod });
        }

        [HttpPost]
        public async Task<IActionResult> AddSpec(int prodNum, string specName, string specVal)
        {
            if (prodNum == 0 || specName == null || specVal == null)
            {
                return NotFound();
            }

            var spec = new ProductSpec
            {
                ProductNumber = prodNum,
                SpecName = specName,
                SpecVal = specVal
            };

            _context.ProductSpecs.Add(spec);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = prodNum });
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryCode,UnitPrice,Media,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Name", product.CategoryCode);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Name", product.CategoryCode);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductNumber,Name,Description,CategoryCode,UnitPrice,Media,Stock")] Product product)
        {
            if (id != product.ProductNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Code", product.CategoryCode);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductNumber == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductNumber == id);
        }
    }
}
