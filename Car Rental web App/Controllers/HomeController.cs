using Car_Rental_web_App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();

            return View(cars);
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
