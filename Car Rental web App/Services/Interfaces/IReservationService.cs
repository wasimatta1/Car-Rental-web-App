using Car_Rental_web_App.Models;

namespace Car_Rental_web_App.Services.Interfaces
{
    public interface IReservationService
    {
        public Task CreateReservationAsync(Reservation reservation);

    }
}
