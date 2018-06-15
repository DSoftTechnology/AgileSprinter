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
    public class UserSettingsController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public UserSettingsController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: UserSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserSettings.ToListAsync());
        }

        // GET: UserSettings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSettings = await _context.UserSettings
                .SingleOrDefaultAsync(m => m.Username == id);
            if (userSettings == null)
            {
                return NotFound();
            }

            return View(userSettings);
        }

        // GET: UserSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,SettingKey,SettingValue")] UserSettings userSettings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSettings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userSettings);
        }

        // GET: UserSettings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSettings = await _context.UserSettings.SingleOrDefaultAsync(m => m.Username == id);
            if (userSettings == null)
            {
                return NotFound();
            }
            return View(userSettings);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,SettingKey,SettingValue")] UserSettings userSettings)
        {
            if (id != userSettings.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSettings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSettingsExists(userSettings.Username))
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
            return View(userSettings);
        }

        // GET: UserSettings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSettings = await _context.UserSettings
                .SingleOrDefaultAsync(m => m.Username == id);
            if (userSettings == null)
            {
                return NotFound();
            }

            return View(userSettings);
        }

        // POST: UserSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userSettings = await _context.UserSettings.SingleOrDefaultAsync(m => m.Username == id);
            _context.UserSettings.Remove(userSettings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSettingsExists(string id)
        {
            return _context.UserSettings.Any(e => e.Username == id);
        }
    }
}
