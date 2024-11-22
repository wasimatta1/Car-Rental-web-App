using AutoMapper;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.Services.Interfaces;
using Car_Rental_web_App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_web_App.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, ICarService carService
            , SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            _reservationService = reservationService;
            _carService = carService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        public IActionResult RentCar(int carId)
        {

            ViewBag.CarId = carId;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RentCar(ReservationViewModel reservationView)
        {

            if (!ModelState.IsValid)
            {
                return await RentCar(reservationView);
            }

            if (reservationView.StartDate >= reservationView.EndDate)
            {
                ModelState.AddModelError("", "End date must be after the start date.");
                return View(reservationView);
            }
            if (reservationView.StartDate.DayOfYear < DateTime.Now.DayOfYear || reservationView.EndDate.DayOfYear <= DateTime.Now.DayOfYear)
            {
                ModelState.AddModelError("", "The dates must be in the future.");
                return View(reservationView);
            }
            var reservation = _mapper.Map<Reservation>(reservationView);

            var user = await userManager.GetUserAsync(User);
            var car = await _carService.GetCarByIdAsync(reservation.CarId);



            reservation.UserId = user.Id;
            reservation.ReservationDate = DateTime.Now;
            reservation.Status = reservation.StartDate <= DateTime.Now ? "Active" : "Pending";
            reservation.TotalPrice = (reservation.EndDate - reservation.StartDate).Days * car!.PricePerDay;

            await _reservationService.CreateReservationAsync(reservation);

            if (reservation.Status.Equals("Active"))
            {
                await _carService.UpdateCarAvailability(reservation.CarId, false);
            }

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> History()
        {
            var user = await userManager.GetUserAsync(User);

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var allReservations = await _reservationService.GetReservations();
                return View(allReservations);
            }
            var reservations = await _reservationService.GetReservationsByUserIdAsync(user.Id);

            return View(reservations);
        }
        public async Task<IActionResult> Manage()
        {
            var user = await userManager.GetUserAsync(User);

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var allReservations = await _reservationService.GetActiveReservations();
                return View(allReservations);
            }
            var reservations = await _reservationService.GetActiveReservationsByUserIdAsync(user.Id);

            return View(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            await _reservationService.CancelReservationAsync(reservationId);

            return RedirectToAction("Manage", "Reservation");
        }
        [HttpPost]
        public async Task<IActionResult> ReturnCar(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            reservation!.Status = "Returned";

            await _carService.UpdateCarAvailability(reservation.CarId, true);

            await _reservationService.SaveChangesAsync();

            return RedirectToAction("Manage", "Reservation");
        }


    }
}
