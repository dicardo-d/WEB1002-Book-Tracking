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
    public class CategoryListController : Controller
    {
        private readonly TrackingContext _context;

        public CategoryListController(TrackingContext context)
        {
            _context = context;
        }

        // GET: CategoryList
        public async Task<IActionResult> Index()
        {
              return _context.CategoryModels != null ? 
                          View(await _context.CategoryModels.ToListAsync()) :
                          Problem("Entity set 'TrackingContext.CategoryModels'  is null.");
        }

        // GET: CategoryList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryModels == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModels
                .FirstOrDefaultAsync(m => m.CategoryModelId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: CategoryList/Create
        public IActionResult Create()
        {
            ViewData["CategoryTypeModels"] = _context.CategoryTypeModels!.Select(b => new SelectListItem(b.Name, b.CategoryTypeModelId.ToString())).ToList();
            return View();
        }

        // POST: CategoryList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CategoryModel categoryModel)
        {
            var localCategoryType = _context.CategoryTypeModels!.Where(b => b.CategoryTypeModelId == categoryModel.CategoryTypeModel!.CategoryTypeModelId).FirstOrDefault();
            if (localCategoryType != null)
            {
                categoryModel.CategoryTypeModel = localCategoryType;
                ModelState.ClearValidationState("CategoryTypeModel");
                this.TryValidateModel(categoryModel);
            }
            if (ModelState.IsValid)
            {
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: CategoryList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryModels == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModels.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: CategoryList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryModelId,Description")] CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.CategoryModelId))
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
            return View(categoryModel);
        }

        // GET: CategoryList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryModels == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModels
                .FirstOrDefaultAsync(m => m.CategoryModelId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: CategoryList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryModels == null)
            {
                return Problem("Entity set 'TrackingContext.CategoryModels'  is null.");
            }
            var categoryModel = await _context.CategoryModels.FindAsync(id);
            if (categoryModel != null)
            {
                _context.CategoryModels.Remove(categoryModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
          return (_context.CategoryModels?.Any(e => e.CategoryModelId == id)).GetValueOrDefault();
        }
    }
}
