using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSoft.AgileSprinter.Web.Models;
using DSoft.AgileSprinter.Data.Models;

namespace DSoft.AgileSprinter.Web.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public UserRolesController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index()
        {
            var dSoft_AgileSprinterContext = _context.UserRoles.Include(u => u.Role).Include(u => u.UserNameNavigation);
            return View(await dSoft_AgileSprinterContext.ToListAsync());
        }

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoles = await _context.UserRoles
                .Include(u => u.Role)
                .Include(u => u.UserNameNavigation)
                .SingleOrDefaultAsync(m => m.UserRoleId == id);
            if (userRoles == null)
            {
                return NotFound();
            }

            return View(userRoles);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserName"] = new SelectList(_context.Users, "Name", "Name");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRoleId,UserName,RoleId")] UserRoles userRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoles.RoleId);
            ViewData["UserName"] = new SelectList(_context.Users, "Name", "Name", userRoles.UserName);
            return View(userRoles);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoles = await _context.UserRoles.SingleOrDefaultAsync(m => m.UserRoleId == id);
            if (userRoles == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoles.RoleId);
            ViewData["UserName"] = new SelectList(_context.Users, "Name", "Name", userRoles.UserName);
            return View(userRoles);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserRoleId,UserName,RoleId")] UserRoles userRoles)
        {
            if (id != userRoles.UserRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRolesExists(userRoles.UserRoleId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoles.RoleId);
            ViewData["UserName"] = new SelectList(_context.Users, "Name", "Name", userRoles.UserName);
            return View(userRoles);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoles = await _context.UserRoles
                .Include(u => u.Role)
                .Include(u => u.UserNameNavigation)
                .SingleOrDefaultAsync(m => m.UserRoleId == id);
            if (userRoles == null)
            {
                return NotFound();
            }

            return View(userRoles);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRoles = await _context.UserRoles.SingleOrDefaultAsync(m => m.UserRoleId == id);
            _context.UserRoles.Remove(userRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRolesExists(int id)
        {
            return _context.UserRoles.Any(e => e.UserRoleId == id);
        }
    }
}
