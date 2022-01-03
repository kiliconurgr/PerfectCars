using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPerfectCarsData;

namespace MVCPerfectCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModulsController : Controller
    {
        private readonly MVCPerfectCarsDbContext _context;

        public ModulsController(MVCPerfectCarsDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Moduls
        public async Task<IActionResult> Index()
        {
            var mVCPerfectCarsDbContext = _context.Moduls.Include(m => m.Brand).Include(m => m.VehicleType);
            return View(await mVCPerfectCarsDbContext.ToListAsync());
        }

        // GET: Admin/Moduls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modul = await _context.Moduls
                .Include(m => m.Brand)
                .Include(m => m.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modul == null)
            {
                return NotFound();
            }

            return View(modul);
        }

        // GET: Admin/Moduls/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Logo");
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/Moduls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BrandId,VehicleTypeId,Id,Enabled,DateOfCreation")] Modul modul)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modul);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Logo", modul.BrandId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", modul.VehicleTypeId);
            return View(modul);
        }

        // GET: Admin/Moduls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modul = await _context.Moduls.FindAsync(id);
            if (modul == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Logo", modul.BrandId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", modul.VehicleTypeId);
            return View(modul);
        }

        // POST: Admin/Moduls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,BrandId,VehicleTypeId,Id,Enabled,DateOfCreation")] Modul modul)
        {
            if (id != modul.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modul);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModulExists(modul.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Logo", modul.BrandId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", modul.VehicleTypeId);
            return View(modul);
        }

        // GET: Admin/Moduls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modul = await _context.Moduls
                .Include(m => m.Brand)
                .Include(m => m.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modul == null)
            {
                return NotFound();
            }

            return View(modul);
        }

        // POST: Admin/Moduls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modul = await _context.Moduls.FindAsync(id);
            _context.Moduls.Remove(modul);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModulExists(int id)
        {
            return _context.Moduls.Any(e => e.Id == id);
        }
    }
}
