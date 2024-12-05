using Car_Rental_web_App.Data;

namespace Car_Rental_web_App.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<AppDbContext>();
            if (!context.Cars.Any())
            {
                context.AddRange(GetSeedCars());
            }

            context.SaveChanges();
        }
        public static List<Car> GetSeedCars()
        {
            return new List<Car>
            {
                new Car
                {
                    Model = "Model S",
                    Make = "Tesla",
                    Year = 2021,
                    Location = "Ramallah",
                    IsAvailable = true,
                    Description = "Luxury electric sedan with autopilot.",
                    PricePerDay = 250.00m,
                    ImageUrl = "/images/tesla_car.png"
                },
                new Car
                {
                    Model = "Camry",
                    Make = "Toyota",
                    Year = 2020,
                    Location = "Birzeit",
                    IsAvailable = true,
                    Description = "Reliable and fuel-efficient midsize sedan.",
                    PricePerDay = 100.00m,
                    ImageUrl = "/images/Toyota-Camry.png"
                },
                new Car
                {
                    Model = "Civic",
                    Make = "Honda",
                    Year = 2019,
                    Location = "Jerusalem",
                    IsAvailable = false,
                    Description = "Compact car with excellent fuel economy.",
                    PricePerDay = 80.00m,
                    ImageUrl = "/images/Honda-Civic.png"
                },
                new Car
                {
                    Model = "Mustang",
                    Make = "Ford",
                    Year = 2022,
                    Location = "Hebron",
                    IsAvailable = true,
                    Description = "Sporty coupe with powerful performance.",
                    PricePerDay = 300.00m,
                    ImageUrl = "/images/mustang.png"
                },
                new Car
                {

                    Model = "Model X",
                    Make = "Tesla",
                    Year = 2023,
                    Location = "Ramallah",
                    IsAvailable = true,
                    Description = "Premium electric SUV with falcon-wing doors.",
                    PricePerDay = 400.00m,
                    ImageUrl = "/images/Model-x.png"
                }
            };
        }
    }
}
