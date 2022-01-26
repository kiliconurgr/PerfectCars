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
using X.PagedList;
using X.PagedList.Mvc;

namespace MVCPerfectCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly MVCPerfectCarsDbContext _context;
        private readonly UtilsService utilsService;

        public BrandsController(
            MVCPerfectCarsDbContext context,
            UtilsService utilsService
           
            )
        {
            _context = context;
            this.utilsService = utilsService;
        }

        public IActionResult Index(int? page)
        {
            var model = (_context.Brands.ToList()).ToPagedList(page ?? 1, 10);
            return View(model);
        }


        public IActionResult Create()
        {
            var model = new Brand { Enabled = true };
            return View(model);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create( Brand brand)
        {

            if(brand.ImageFile is null)
            {
                TempData["error"] = ErrorDescriber.NoImageError();
                return View(brand);
                
            }

            utilsService.AddImage(brand, new ResizeImageOptions { Width = 120, Height = 120, Watermark = false });
            brand.DateOfCreation = DateTime.Now;
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

        public async Task<IActionResult> Edit(Brand brand)
        {
            if (brand.ImageFile is not null)
                utilsService.AddImage(brand, new ResizeImageOptions { Width = 120, Height = 120, Watermark = false });
            _context.Update(brand);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = ErrorDescriber.ConstraintError(brand.Name);
                return View(brand);
            }

        }

       
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

