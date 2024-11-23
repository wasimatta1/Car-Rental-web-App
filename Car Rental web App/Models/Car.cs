namespace Car_Rental_web_App.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }


}
