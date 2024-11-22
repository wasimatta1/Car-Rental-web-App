using Car_Rental_web_App.Data;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.Services.Interfaces;

namespace Car_Rental_web_App.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        public ReservationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

        }
    }
}
