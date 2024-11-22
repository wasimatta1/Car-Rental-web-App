using Car_Rental_web_App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        public HomeController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {

            var cars = await _carService.GetAllCarsAsync();

            return View(cars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult RentCar()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var cars = await _carService.SearchCarsAsync(query);

            return View(cars);
        }
    }
}
