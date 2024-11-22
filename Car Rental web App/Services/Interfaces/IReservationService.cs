using Car_Rental_web_App.Models;

namespace Car_Rental_web_App.Services.Interfaces
{
    public interface IReservationService
    {
        public Task CreateReservationAsync(Reservation reservation);

        public Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId);
        public Task<IEnumerable<Reservation>> GetActiveReservationsByUserIdAsync(string userId);
        public Task CancelReservationAsync(int reservationId);
        public Task<Reservation?> GetReservationByIdAsync(int id);
        public Task<IEnumerable<Reservation>> GetActiveReservations();
        public Task<IEnumerable<Reservation>> GetReservations();
        public Task SaveChangesAsync();
    }
}
