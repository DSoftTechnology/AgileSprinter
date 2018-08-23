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
    public class UserStoriesController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public UserStoriesController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: UserStories
        public async Task<IActionResult> Index()
        {
            var dSoft_AgileSprinterContext = _context.UserStories.Include(u => u.Project).Include(u => u.Sprint);
            return View(await dSoft_AgileSprinterContext.ToListAsync());
        }

        // GET: UserStories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.Project)
                .Include(u => u.Sprint)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        // GET: UserStories/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Description");
            ViewData["SprintId"] = new SelectList(_context.Sprints, "Id", "Description");
            return View();
        }

        // POST: UserStories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,SprintId,Description,Status,Estimated,Comments,Priority,Color")] UserStories userStories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userStories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Description", userStories.ProjectId);
            ViewData["SprintId"] = new SelectList(_context.Sprints, "Id", "Description", userStories.SprintId);
            return View(userStories);
        }

        // GET: UserStories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories.SingleOrDefaultAsync(m => m.Id == id);
            if (userStories == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Description", userStories.ProjectId);
            ViewData["SprintId"] = new SelectList(_context.Sprints, "Id", "Description", userStories.SprintId);
            return View(userStories);
        }

        // POST: UserStories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,SprintId,Description,Status,Estimated,Comments,Priority,Color")] UserStories userStories)
        {
            if (id != userStories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoriesExists(userStories.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Description", userStories.ProjectId);
            ViewData["SprintId"] = new SelectList(_context.Sprints, "Id", "Description", userStories.SprintId);
            return View(userStories);
        }

        // GET: UserStories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.Project)
                .Include(u => u.Sprint)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        // POST: UserStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userStories = await _context.UserStories.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserStories.Remove(userStories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStoriesExists(int id)
        {
            return _context.UserStories.Any(e => e.Id == id);
        }
    }
}
