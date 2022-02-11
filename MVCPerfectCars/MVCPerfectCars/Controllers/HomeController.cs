using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCPerfectCars.Models;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVCPerfectCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MVCPerfectCarsDbContext context;

        public HomeController(
            ILogger<HomeController> logger,
            MVCPerfectCarsDbContext context
            )
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SelectedPortfolios = await context.Vehicles.Include(p => p.Portfolios).Where(p => p.Enabled).OrderBy(p => p.Name).ToListAsync();
            ViewBag.FeaturedVehicles = await context.Vehicles.Include(p => p.Modul).Include(p => p.Brand).Where(p => p.Enabled).OrderBy(p => Guid.NewGuid()).Take(9).ToListAsync();
            ViewBag.Banners = await context.Banner.Where(p => p.Enabled).ToListAsync();
            return View();
        }
        public async Task<IActionResult> Modul(int id)
        {
            var model = await context.Moduls.Include(p => p.Vehicles).ThenInclude(p => p.Brand).SingleOrDefaultAsync(p => p.Id == id && p.Enabled);
            return View(model);
        }
        public async Task<IActionResult> Brands(int id)
        {
            var model = await context.Brands.Include(p => p.Vehicles).ThenInclude(p => p.Modul).SingleOrDefaultAsync(p => p.Id == id && p.Enabled);
            return View(model);
        }
        public async Task<IActionResult> Search(string keyword)
        {
            var keywords = Regex.Split(keyword, @"\s+").ToList();
            var model = context.Vehicles.Include(p => p.Brand).Include(p => p.Modul).AsEnumerable().Where(p => keywords.Any(q => p.Name.ToLower().Contains(q.ToLower()))).ToList();
            return View(model);
        }
        public async Task<IActionResult> VehicleDetail(int id)
        {
            var model = await context.Vehicles.Include(p => p.Brand).Include(p => p.Modul).Include(p=>p.VehicleImages).SingleOrDefaultAsync(p => p.Id == id && p.Enabled);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
