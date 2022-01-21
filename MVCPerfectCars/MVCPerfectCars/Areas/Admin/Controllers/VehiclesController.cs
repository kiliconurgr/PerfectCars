using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class VehiclesController : Controller
    {
        private readonly MVCPerfectCarsDbContext _context;
        private readonly UtilsService utilsService;

        public VehiclesController(
            MVCPerfectCarsDbContext context,
            UtilsService utilsService
            )
        {
            _context = context;
            this.utilsService = utilsService;
        }


        private async Task PopulateDropdowns()
        {
            ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");
            ViewBag.Portfolios = new SelectList(await _context.Portfolios.ToListAsync(), "Id", "Name");
        }


        // GET: Admin/Vehicles
        public async Task<IActionResult> Index()
        {
            var mVCPerfectCarsDbContext = _context.Vehicles.Include(v => v.Brand).Include(v => v.Modul);
            return View(await mVCPerfectCarsDbContext.ToListAsync());
        }

       

        // GET: Admin/Vehicles/Create
        public async Task<IActionResult> Create()
        {
           
            

            await PopulateDropdowns();
            var model = new Vehicle { Enabled = true };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetModuls(int id) 
        {
            return Json(await _context.Moduls.Where(p => p.BrandId == id).ToListAsync());

        }
       
        [HttpPost]
        
        public async Task<IActionResult> Create( Vehicle vehicle)
        {
            utilsService.AddImage(vehicle, new ResizeImageOptions { Width = 800, Height = 800, Watermark = false });

            if (vehicle.ImageFiles is not null)
                foreach (var imageFile in vehicle.ImageFiles)
                    vehicle.VehicleImages.Add(
                        new VehicleImage { Image = await utilsService.PrepareImage(imageFile.OpenReadStream(), 800, 800, false) }
                        );
            vehicle.Price= decimal.Parse(vehicle.PriceText, CultureInfo.CreateSpecificCulture("tr-TR"));
            vehicle.DateOfCreation = DateTime.Now;
            _context.Vehicles.Add(vehicle);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception) { 


            await PopulateDropdowns();
            TempData["error"] = ErrorDescriber.ConstraintError(vehicle.Name);
                return View(vehicle);
            }
        }
      
        
        public async Task<IActionResult> Edit(int? id)
        {
            await PopulateDropdowns();
            var vehicle = await _context.Vehicles.Include(p => p.VehicleImages).SingleAsync(p => p.Id == id);
            vehicle.PriceText = vehicle.Price.ToString("n2", CultureInfo.CreateSpecificCulture("tr-TR"));
            return View(vehicle);
        }

      
        [HttpPost]
      
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            utilsService.AddImage(vehicle, new ResizeImageOptions { Width = 800, Height = 800, Watermark = false });
            if (vehicle.ImageFiles is not null)
                foreach (var imageFile in vehicle.ImageFiles)
                    vehicle.VehicleImages.Add(
                        new VehicleImage { Image = await utilsService.PrepareImage(imageFile.OpenReadStream(), 800, 800, false) }
                        );
            
            if (vehicle.ImagesToDeleted is not null)
            {
                foreach (var item in vehicle.ImagesToDeleted)
                {
                    var vehicleImage = await _context.VehicleImages.FindAsync(item);
                    _context.Remove(vehicleImage);
                }
            }

            vehicle.Price = decimal.Parse(vehicle.PriceText, CultureInfo.CreateSpecificCulture("tr-TR"));
            _context.Update(vehicle);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await PopulateDropdowns();
                TempData["error"] = ErrorDescriber.ConstraintError(vehicle.Name);
                return View(vehicle);
            }
        }

        // GET: Admin/Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Remove(vehicle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                TempData["error"] = ErrorDescriber.ConcurrencyError(vehicle.Name);
            }
            return RedirectToAction(nameof(Index));
        }

       
        

        
    }
}
