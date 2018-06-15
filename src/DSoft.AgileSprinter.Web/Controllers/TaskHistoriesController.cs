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
    public class TaskHistoriesController : Controller
    {
        private readonly DSoft_AgileSprinterContext _context;

        public TaskHistoriesController(DSoft_AgileSprinterContext context)
        {
            _context = context;
        }

        // GET: TaskHistories
        public async Task<IActionResult> Index()
        {
            var dSoft_AgileSprinterContext = _context.TaskHistories.Include(t => t.Task).Include(t => t.UserStory);
            return View(await dSoft_AgileSprinterContext.ToListAsync());
        }

        // GET: TaskHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskHistories = await _context.TaskHistories
                .Include(t => t.Task)
                .Include(t => t.UserStory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskHistories == null)
            {
                return NotFound();
            }

            return View(taskHistories);
        }

        // GET: TaskHistories/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description");
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description");
            return View();
        }

        // POST: TaskHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskId,UserStoryId,Description,Estimated,Actual,Remaining,AssignedTo,Status,DateChanged,Notes")] TaskHistories taskHistories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskHistories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", taskHistories.TaskId);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", taskHistories.UserStoryId);
            return View(taskHistories);
        }

        // GET: TaskHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskHistories = await _context.TaskHistories.SingleOrDefaultAsync(m => m.Id == id);
            if (taskHistories == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", taskHistories.TaskId);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", taskHistories.UserStoryId);
            return View(taskHistories);
        }

        // POST: TaskHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskId,UserStoryId,Description,Estimated,Actual,Remaining,AssignedTo,Status,DateChanged,Notes")] TaskHistories taskHistories)
        {
            if (id != taskHistories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskHistories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskHistoriesExists(taskHistories.Id))
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
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", taskHistories.TaskId);
            ViewData["UserStoryId"] = new SelectList(_context.UserStories, "Id", "Description", taskHistories.UserStoryId);
            return View(taskHistories);
        }

        // GET: TaskHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskHistories = await _context.TaskHistories
                .Include(t => t.Task)
                .Include(t => t.UserStory)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskHistories == null)
            {
                return NotFound();
            }

            return View(taskHistories);
        }

        // POST: TaskHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskHistories = await _context.TaskHistories.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaskHistories.Remove(taskHistories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskHistoriesExists(int id)
        {
            return _context.TaskHistories.Any(e => e.Id == id);
        }
    }
}
