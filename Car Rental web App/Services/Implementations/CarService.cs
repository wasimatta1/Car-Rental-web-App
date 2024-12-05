using Car_Rental_web_App.Data;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_web_App.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;
        public CarService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task<IEnumerable<Car>> SearchCarsAsync(string query)
        {
            return await _context.Cars.Where(c => c.Location.Contains(query)
            || c.Model.Contains(query)
            || c.Make.Contains(query)
            || c.PricePerDay.ToString().Contains(query)
            || c.Year.ToString().Contains(query))
                .ToListAsync();
        }

        public async Task UpdateCarAvailability(int carId, bool newState)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carId);
            car.IsAvailable = newState;
            await _context.SaveChangesAsync();
        }
    }
}
