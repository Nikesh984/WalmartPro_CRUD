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
    public class UsersController : Controller
    {
        private readonly WalmartProContext _context;

        public UsersController(WalmartProContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'WalmartProContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Email,Username,Password,MobileNumber,RegistrationDate,LastLoginDate")] User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                // Define parameters for the stored procedure
                var firstNameParam = new SqlParameter("@FirstName", user.FirstName);
                var lastNameParam = new SqlParameter("@LastName", user.LastName);
                var emailParam = new SqlParameter("@Email", user.Email);
                var usernameParam = new SqlParameter("@Username", user.Username);
                var passwordParam = new SqlParameter("@Password", user.Password);
                var mobileNumberParam = new SqlParameter("@Phone", user.MobileNumber);

                // Define the output parameter
                var newUserIdParam = new SqlParameter
                {
                    ParameterName = "@NewUserID1",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                await _context.Database.ExecuteSqlRawAsync("EXEC RegisterUser @FirstName, @LastName, @Email, @Username, @Password, @Phone, @NewUserID1 OUTPUT",
                    firstNameParam, lastNameParam, emailParam, usernameParam, passwordParam, mobileNumberParam, newUserIdParam);

                // Optionally use the output parameter value
                var newUserId = (int)newUserIdParam.Value;
                Console.WriteLine($"New user ID: {newUserId}");

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,Email,Username,Password,MobileNumber")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var userInDb = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
                if (userInDb == null)
                {
                    return NotFound();
                }

                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.Email = user.Email;
                userInDb.Username = user.Username;
                userInDb.MobileNumber = user.MobileNumber;

                try
                {
                    _context.Update(userInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
                return View(user);
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'WalmartProContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
