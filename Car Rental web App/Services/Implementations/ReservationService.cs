using Car_Rental_web_App.Data;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_web_App.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            _context.Reservations.Remove(reservation!);
            await _context.SaveChangesAsync();
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

        }


        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId)
        {
            return await _context.Reservations.Include(r => r.Car).Include(r => r.User)
                .Where(r => r.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetActiveReservationsByUserIdAsync(string userId)
        {
            return await _context.Reservations.Include(r => r.Car).Include(r => r.User)
                .Where(r => r.UserId == userId).Where(r => r.Status == "Active" || r.Status == "Pending").ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservations()
        {
            return await _context.Reservations.Include(r => r.Car).Include(r => r.User)
                 .Where(r => r.Status == "Active" || r.Status == "Pending").ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Car).Include(r => r.User).ToListAsync();
        }
    }
}
