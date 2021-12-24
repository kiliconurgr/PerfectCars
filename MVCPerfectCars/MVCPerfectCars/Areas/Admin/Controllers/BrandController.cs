using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly MVCPerfectCarsDbContext context;

        public BrandsController(
            MVCPerfectCarsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await context.Brands.ToListAsync();
            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            
            return View();
        }


    }
}
