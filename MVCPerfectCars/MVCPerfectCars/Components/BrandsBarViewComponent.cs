using Microsoft.AspNetCore.Mvc;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars.Components
{
    public class BrandsBarViewComponent : ViewComponent
    {
        private readonly MVCPerfectCarsDbContext context;

        public BrandsBarViewComponent(
            MVCPerfectCarsDbContext context
            )
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            var model = context.Brands.Where(p => p.Enabled).OrderBy(p => p.Name).ToList();
            return View(model);
        }
    }
}
