using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthTipsPortal.Models;
using Microsoft.AspNetCore.Authorization;
using HealthTipsPortal.Data;

namespace HealthTipsPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var todayTip = _context.HealthTips.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
            var categories = _context.HealthTips.Select(t => t.Category).Distinct().ToList();
            var popular = categories.FirstOrDefault() ?? "General";
            var total = _context.HealthTips.Count();

            ViewData["DailyTip"] = todayTip?.TipText;
            ViewData["PopularCategory"] = popular;
            ViewData["TotalTips"] = total;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Categories()
        {
            var cats = _context.HealthTips.Select(t => t.Category).Distinct().ToList();
            return View(cats);
        }

        public IActionResult Daily()
        {
            var tip = _context.HealthTips.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
            return tip == null ? RedirectToAction("Index") : View(tip);
        }

        public IActionResult Category(string name)
        {
            var tips = _context.HealthTips.Where(t => t.Category == name).ToList();
            return View(tips);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
