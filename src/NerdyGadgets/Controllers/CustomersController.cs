using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Data;
using NerdyGadgets.Dtos;
using NerdyGadgets.Models;
using NerdyGadgets.Services;

namespace NerdyGadgets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Users
                .Include(u => u.Address)
                .Where(u => u.Role == UserRole.Customer || u.Role == UserRole.Unconfirmed)
                .ToListAsync();

            return View(customers);
        }

        // GET: Customers/Promote/5
        public async Task<IActionResult> Promote(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null || user.Role == UserRole.Admin)
            {
                return NotFound();
            }

            switch (user.Role)
            {
                case UserRole.Unconfirmed:
                    user.Role = UserRole.Customer;
                    break;
                case UserRole.Customer:
                    user.Role = UserRole.Admin;
                    break;
            };

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null || user.Role == UserRole.Admin)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,Preposition,LastName,Email,Password,Country,City,Street,ZipCode")] UserAddressDto userAddress)
        {
            if (ModelState.IsValid)
            {
                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    Country = userAddress.Country,
                    City = userAddress.City,
                    Street = userAddress.Street,
                    ZipCode = userAddress.ZipCode
                };

                var user = new User
                {
                    FirstName = userAddress.FirstName,
                    Preposition = userAddress.Preposition,
                    LastName = userAddress.LastName,
                    Email = userAddress.Email,
                    Password = AuthService.Hash(userAddress.Password),
                    AddressId = address.Id,
                    AccountCreatedAt = DateTime.UtcNow,
                    Role = UserRole.Unconfirmed
                };

                _context.Addresses.Add(address);
                _context.Users.Add(user);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userAddress);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(c => c.Address)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new UserAddressDto
            {
                FirstName = user.FirstName,
                Preposition = user.Preposition,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Country = user.Address.Country,
                City = user.Address.City,
                Street = user.Address.Street,
                ZipCode = user.Address.ZipCode
            });
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, FirstName, Preposition, LastName, Email, Password, Country, City, Street, ZipCode")] UserAddressDto userAddress)
        {
            var user = await _context.Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Override with the new info.
                user.FirstName = userAddress.FirstName;
                user.Preposition = userAddress.Preposition;
                user.LastName = userAddress.LastName;

                user.Address.Country = userAddress.Country;
                user.Address.City = userAddress.City;
                user.Address.Street = userAddress.Street;
                user.Address.ZipCode = userAddress.ZipCode;

                // Save the changes in the database.
                _context.Addresses.Update(user.Address);
                _context.Users.Update(user);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userAddress);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users
                .Include(c => c.Address)
                .SingleOrDefaultAsync(c => c.Id == id);
            
            if (user != null)
            {
                _context.Addresses.Remove(user.Address);
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    
    }
}
