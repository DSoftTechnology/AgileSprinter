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
    public class TasksController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public TasksController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var dSoft_AgileSprinterContext = _context.Tasks.Include(t => t.AssignedToNavigation).Include(t => t.UserStory);
            return View(await dSoft_AgileSprinterContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.AssignedToNavigation)
                .Include(t => t.UserStory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["AssignedTo"] = new SelectList(_context.Users, "Name", "Name");
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserStoryId,Description,Estimated,Actual,Remaining,AssignedTo,Status,Roadblocked,Notes,SortOrder")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedTo"] = new SelectList(_context.Users, "Name", "Name", tasks.AssignedTo);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", tasks.UserStoryId);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["AssignedTo"] = new SelectList(_context.Users, "Name", "Name", tasks.AssignedTo);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", tasks.UserStoryId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserStoryId,Description,Estimated,Actual,Remaining,AssignedTo,Status,Roadblocked,Notes,SortOrder")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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
            ViewData["AssignedTo"] = new SelectList(_context.Users, "Name", "Name", tasks.AssignedTo);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", tasks.UserStoryId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.AssignedToNavigation)
                .Include(t => t.UserStory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
