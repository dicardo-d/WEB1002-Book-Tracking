using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_1002_BookTracking.Models;

namespace WEB_1002_BookTracking
{
    public class CategoryTypeListController : Controller
    {
        private readonly TrackingContext _context;

        public CategoryTypeListController(TrackingContext context)
        {
            _context = context;
        }

        // GET: CategoryTypeList
        public async Task<IActionResult> Index()
        {
              return _context.CategoryTypeModels != null ? 
                          View(await _context.CategoryTypeModels.ToListAsync()) :
                          Problem("Entity set 'TrackingContext.CategoryTypeModels'  is null.");
        }

        // GET: CategoryTypeList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryTypeModels == null)
            {
                return NotFound();
            }

            var categoryTypeModel = await _context.CategoryTypeModels
                .FirstOrDefaultAsync(m => m.CategoryTypeModelId == id);
            if (categoryTypeModel == null)
            {
                return NotFound();
            }

            return View(categoryTypeModel);
        }

        // GET: CategoryTypeList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryTypeList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryTypeModelId,Name")] CategoryTypeModel categoryTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryTypeModel);
        }

        // GET: CategoryTypeList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryTypeModels == null)
            {
                return NotFound();
            }

            var categoryTypeModel = await _context.CategoryTypeModels.FindAsync(id);
            if (categoryTypeModel == null)
            {
                return NotFound();
            }
            return View(categoryTypeModel);
        }

        // POST: CategoryTypeList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryTypeModelId,Name")] CategoryTypeModel categoryTypeModel)
        {
            if (id != categoryTypeModel.CategoryTypeModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryTypeModelExists(categoryTypeModel.CategoryTypeModelId))
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
            return View(categoryTypeModel);
        }

        // GET: CategoryTypeList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryTypeModels == null)
            {
                return NotFound();
            }

            var categoryTypeModel = await _context.CategoryTypeModels
                .FirstOrDefaultAsync(m => m.CategoryTypeModelId == id);
            if (categoryTypeModel == null)
            {
                return NotFound();
            }

            return View(categoryTypeModel);
        }

        // POST: CategoryTypeList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryTypeModels == null)
            {
                return Problem("Entity set 'TrackingContext.CategoryTypeModels'  is null.");
            }
            var categoryTypeModel = await _context.CategoryTypeModels.FindAsync(id);
            if (categoryTypeModel != null)
            {
                _context.CategoryTypeModels.Remove(categoryTypeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryTypeModelExists(int id)
        {
          return (_context.CategoryTypeModels?.Any(e => e.CategoryTypeModelId == id)).GetValueOrDefault();
        }
    }
}
