using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;

namespace DevIO.App.Controllers
{
    public class AddressViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddressViewModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.AddressViewModel.ToListAsync());
        }

        // GET: AddressViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AddressViewModel == null)
            {
                return NotFound();
            }

            var addressViewModel = await _context.AddressViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressViewModel == null)
            {
                return NotFound();
            }

            return View(addressViewModel);
        }

        // GET: AddressViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Street,Number,Complement,Neighborhood,ZipCode,City,State,SupplierId")] AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                addressViewModel.Id = Guid.NewGuid();
                _context.Add(addressViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressViewModel);
        }

        // GET: AddressViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AddressViewModel == null)
            {
                return NotFound();
            }

            var addressViewModel = await _context.AddressViewModel.FindAsync(id);
            if (addressViewModel == null)
            {
                return NotFound();
            }
            return View(addressViewModel);
        }

        // POST: AddressViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Street,Number,Complement,Neighborhood,ZipCode,City,State,SupplierId")] AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressViewModelExists(addressViewModel.Id))
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
            return View(addressViewModel);
        }

        // GET: AddressViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AddressViewModel == null)
            {
                return NotFound();
            }

            var addressViewModel = await _context.AddressViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressViewModel == null)
            {
                return NotFound();
            }

            return View(addressViewModel);
        }

        // POST: AddressViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AddressViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddressViewModel'  is null.");
            }
            var addressViewModel = await _context.AddressViewModel.FindAsync(id);
            if (addressViewModel != null)
            {
                _context.AddressViewModel.Remove(addressViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressViewModelExists(Guid id)
        {
          return _context.AddressViewModel.Any(e => e.Id == id);
        }
    }
}
