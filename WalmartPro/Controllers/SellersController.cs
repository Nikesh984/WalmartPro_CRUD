using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WalmartPro.Models;

namespace WalmartPro.Controllers
{
    public class SellersController : Controller
    {
        private readonly WalmartProContext _context;

        public SellersController(WalmartProContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
              return _context.Sellers != null ? 
                          View(await _context.Sellers.ToListAsync()) :
                          Problem("Entity set 'WalmartProContext.Sellers'  is null.");
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,SellerFirstName,SellerLastName,SellerEmail,SellerUsername,SellerPassword,SellerMobileNumber,RegistrationDate,LastLoginDate")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(seller);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                // Define parameters for the stored procedure
                var firstNameParam = new SqlParameter("@FirstName", seller.SellerFirstName);
                var lastNameParam = new SqlParameter("@LastName", seller.SellerLastName);
                var emailParam = new SqlParameter("@Email", seller.SellerEmail);
                var usernameParam = new SqlParameter("@Username", seller.SellerUsername);
                var passwordParam = new SqlParameter("@Password", seller.SellerPassword);
                var mobileNumberParam = new SqlParameter("@Phone", seller.SellerMobileNumber);

                // Define the output parameter
                var newUserIdParam = new SqlParameter
                {
                    ParameterName = "@NewUserID1",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                await _context.Database.ExecuteSqlRawAsync("EXEC RegisterSeller @FirstName, @LastName, @Email, @Username, @Password, @Phone, @NewUserID1 OUTPUT",
                    firstNameParam, lastNameParam, emailParam, usernameParam, passwordParam, mobileNumberParam, newUserIdParam);

                // Optionally use the output parameter value
                var newUserId = (int)newUserIdParam.Value;
                Console.WriteLine($"New user ID: {newUserId}");

                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerId,SellerFirstName,SellerLastName,SellerEmail,SellerUsername,SellerPassword,SellerMobileNumber")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userInDb = await _context.Sellers.AsNoTracking().FirstOrDefaultAsync(s => s.SellerId == id);
                if (userInDb == null)
                {
                    return NotFound();
                }

                userInDb.SellerFirstName = seller.SellerFirstName;
                userInDb.SellerLastName = seller.SellerLastName;
                userInDb.SellerEmail = seller.SellerEmail;
                userInDb.SellerUsername = seller.SellerUsername;
                userInDb.SellerMobileNumber = seller.SellerMobileNumber;

                try
                {
                    _context.Update(userInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.SellerId))
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

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        Console.WriteLine($"Error in {key}: {errorMessage}");
                    }
                }
                // Keep this return statement here to return to the form and show validation messages
                return View(seller);
            }

            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'WalmartProContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return (_context.Sellers?.Any(e => e.SellerId == id)).GetValueOrDefault();
        }
    }
}
