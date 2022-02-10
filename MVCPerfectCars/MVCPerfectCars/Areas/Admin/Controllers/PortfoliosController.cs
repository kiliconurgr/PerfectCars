using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPerfectCars.Areas.Admin.Models;
using MVCPerfectCarsData;

namespace MVCPerfectCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfoliosController : Controller
    {
        private readonly MVCPerfectCarsDbContext _context;

        public PortfoliosController(MVCPerfectCarsDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Portfolios.ToListAsync());
        }

      

        
        public IActionResult Create()
        {
            var model = new Portfolio { Enabled = true };
            return View(model);
        }

       
        [HttpPost]
        
        public async Task<IActionResult> Create( Portfolio portfolio)
        {
            portfolio.DateOfCreation = DateTime.Now;
            if (ModelState.IsValid)
            {
                portfolio.DateOfCreation = DateTime.Now;
                _context.Add(portfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolio);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            return View(portfolio);
        }

        
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.Id))
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
            return View(portfolio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            _context.Remove(portfolio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                TempData["error"] = ErrorDescriber.ConcurrencyError(portfolio.Name);
            }
            return RedirectToAction(nameof(Index));
        }

        
       

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }
    }
}
