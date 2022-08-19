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
    public class BookListController : Controller
    {
        private readonly TrackingContext _context;

        public BookListController(TrackingContext context)
        {
            _context = context;
        }

        // GET: BookList
        public async Task<IActionResult> Index()
        {
              return _context.BookModels != null ? 
                          View(await _context.BookModels.ToListAsync()) :
                          Problem("Entity set 'TrackingContext.BookModels'  is null.");
        }

        // GET: BookList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels
                .FirstOrDefaultAsync(m => m.BookModelId == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: BookList/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = _context.CategoryModels!.Select(b => new SelectListItem(b.Description, b.CategoryModelId.ToString())).ToList();
            return View();
        }

        // POST: BookList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BookModel bookModel)
        {
            var localCategory = _context.CategoryModels!.Include("CategoryTypeModel").Where(b => b.CategoryModelId == bookModel.CategoryModel!.CategoryModelId).FirstOrDefault();
            if (localCategory != null)
            {
                bookModel.CategoryModel = localCategory;
                ModelState.ClearValidationState("CategoryModel");
                this.TryValidateModel(bookModel);
            }
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        // GET: BookList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }

        // POST: BookList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookModelId,Title,Author")] BookModel bookModel)
        {
            if (id != bookModel.BookModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.BookModelId))
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
            return View(bookModel);
        }

        // GET: BookList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels
                .FirstOrDefaultAsync(m => m.BookModelId == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: BookList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookModels == null)
            {
                return Problem("Entity set 'TrackingContext.BookModels'  is null.");
            }
            var bookModel = await _context.BookModels.FindAsync(id);
            if (bookModel != null)
            {
                _context.BookModels.Remove(bookModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
          return (_context.BookModels?.Any(e => e.BookModelId == id)).GetValueOrDefault();
        }
    }
}
