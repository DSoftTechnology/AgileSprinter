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
    public class ElmahErrorsController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public ElmahErrorsController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: ElmahErrors
        public async Task<IActionResult> Index()
        {
            return View(await _context.ElmahError.ToListAsync());
        }

        // GET: ElmahErrors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmahError = await _context.ElmahError
                .SingleOrDefaultAsync(m => m.ErrorId == id);
            if (elmahError == null)
            {
                return NotFound();
            }

            return View(elmahError);
        }

        // GET: ElmahErrors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahErrors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahError elmahError)
        {
            if (ModelState.IsValid)
            {
                elmahError.ErrorId = Guid.NewGuid();
                _context.Add(elmahError);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(elmahError);
        }

        // GET: ElmahErrors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmahError = await _context.ElmahError.SingleOrDefaultAsync(m => m.ErrorId == id);
            if (elmahError == null)
            {
                return NotFound();
            }
            return View(elmahError);
        }

        // POST: ElmahErrors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahError elmahError)
        {
            if (id != elmahError.ErrorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elmahError);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElmahErrorExists(elmahError.ErrorId))
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
            return View(elmahError);
        }

        // GET: ElmahErrors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmahError = await _context.ElmahError
                .SingleOrDefaultAsync(m => m.ErrorId == id);
            if (elmahError == null)
            {
                return NotFound();
            }

            return View(elmahError);
        }

        // POST: ElmahErrors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var elmahError = await _context.ElmahError.SingleOrDefaultAsync(m => m.ErrorId == id);
            _context.ElmahError.Remove(elmahError);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElmahErrorExists(Guid id)
        {
            return _context.ElmahError.Any(e => e.ErrorId == id);
        }
    }
}
