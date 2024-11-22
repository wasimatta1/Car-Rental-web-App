using Car_Rental_web_App.Models;

namespace Car_Rental_web_App.Services.Interfaces
{
    public interface ICarService
    {
        public Task<IEnumerable<Car>> GetAllCarsAsync();

        public Task<Car?> GetCarByIdAsync(int id);
        public Task UpdateCarAvailability(int carId, bool newState);
    }
}
