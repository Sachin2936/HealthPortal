using Microsoft.AspNetCore.Mvc;
using HealthTipsPortal.Models;
using HealthTipsPortal.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HealthTipsPortal.Controllers
{
    public class HealthTipsController : Controller
    {
        private readonly AppDbContext _context;

        public HealthTipsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: /HealthTips/Daily
        public IActionResult Daily()
        {
            var tips = _context.HealthTips.ToList(); // First pull all tips from DB

            if (tips.Count == 0)
                return View("Error", "No health tips available.");

            var random = new Random();
            var tip = tips[random.Next(tips.Count)]; // Pick one randomly

            return View(tip); // Pass single tip to view
        }

        // GET: /HealthTips/Categories
        public IActionResult Categories()
        {
            var categories = _context.HealthTips
                                     .Select(t => t.Category)
                                     .Distinct()
                                     .ToList();
            return View(categories);
        }

        // GET: /HealthTips/ByCategory?name=Fitness
        public IActionResult ByCategory(string name)
        {
            var tips = _context.HealthTips
                               .Where(t => t.Category == name)
                               .ToList();

            ViewBag.CategoryName = name;
            return View(tips);
        }

        // GET: /HealthTips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /HealthTips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HealthTip tip)
        {
            if (ModelState.IsValid)
            {
                tip.CreatedAt = DateTime.Now;
                _context.HealthTips.Add(tip);
                _context.SaveChanges();
                return RedirectToAction("Daily");
            }
            return View(tip);
        }
    }
}
