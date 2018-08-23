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
    public class ApplicationSettingsController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public ApplicationSettingsController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: ApplicationSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationSettings.ToListAsync());
        }

        // GET: ApplicationSettings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSettings = await _context.ApplicationSettings
                .SingleOrDefaultAsync(m => m.Key == id);
            if (applicationSettings == null)
            {
                return NotFound();
            }

            return View(applicationSettings);
        }

        // GET: ApplicationSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Value,LastUpdated")] ApplicationSettings applicationSettings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationSettings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationSettings);
        }

        // GET: ApplicationSettings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSettings = await _context.ApplicationSettings.SingleOrDefaultAsync(m => m.Key == id);
            if (applicationSettings == null)
            {
                return NotFound();
            }
            return View(applicationSettings);
        }

        // POST: ApplicationSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Key,Value,LastUpdated")] ApplicationSettings applicationSettings)
        {
            if (id != applicationSettings.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationSettings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationSettingsExists(applicationSettings.Key))
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
            return View(applicationSettings);
        }

        // GET: ApplicationSettings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSettings = await _context.ApplicationSettings
                .SingleOrDefaultAsync(m => m.Key == id);
            if (applicationSettings == null)
            {
                return NotFound();
            }

            return View(applicationSettings);
        }

        // POST: ApplicationSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationSettings = await _context.ApplicationSettings.SingleOrDefaultAsync(m => m.Key == id);
            _context.ApplicationSettings.Remove(applicationSettings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationSettingsExists(string id)
        {
            return _context.ApplicationSettings.Any(e => e.Key == id);
        }
    }
}
