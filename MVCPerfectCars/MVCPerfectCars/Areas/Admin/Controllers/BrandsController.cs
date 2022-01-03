using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPerfectCars.Areas.Admin.Models;
using MVCPerfectCarsData;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace MVCPerfectCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly MVCPerfectCarsDbContext _context;

        public BrandsController(
            MVCPerfectCarsDbContext context
           
            )
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create( Brand brand)
        {

            if(brand.ImageFile is null)
            {
                TempData["error"] = ErrorDescriber.NoImageError();
                return View(brand);
                
            }

            using var image = await Image.LoadAsync(brand.ImageFile.OpenReadStream());
            image.Mutate(p => p.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(120, 120)
            }));


            brand.Image = image.ToBase64String(PngFormat.Instance);
                _context.Brands.Add(brand);
            try {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = ErrorDescriber.ConstraintError(brand.Name);
                return View(brand);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            return View(brand);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Remove(brand);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                TempData["error"] = ErrorDescriber.ConcurrencyError(brand.Name);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }

    


}

