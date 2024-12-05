using Car_Rental_web_App.Data;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.Services.Implementations;
using Car_Rental_web_App.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_web_App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextConnection"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.Name = "CarRental.Session";
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthorization();

            app.UseSession();

            //Custom middleware to Logout the user if the session is gone
            app.Use(async (context, next) =>
            {
                var userId = context.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId) && context.User.Identity.IsAuthenticated)
                {
                    var signInManager = context.RequestServices.GetRequiredService<SignInManager<User>>();
                    await signInManager.SignOutAsync();
                }
                await next.Invoke();
            });

            app.MapDefaultControllerRoute();

            using (var scope = app.Services.CreateScope())
            {
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new string[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManger.RoleExistsAsync(role))
                    {
                        await roleManger.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            //create the admin user
            using (var scope = app.Services.CreateScope())
            {
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var email = "admin@admin.com";
                var password = "Admin123";

                if (await userManger.FindByEmailAsync(email) == null)
                {
                    var user = new User
                    {
                        UserName = email,
                        Email = email,
                        FirstName = "Admin",
                        LastName = "User",
                        AddressLine1 = "123 Main St",
                        City = "CityName",
                        Country = "CountryName",
                        DriversLicenseNumber = "D123456789"
                    };

                    await userManger.CreateAsync(user, password);

                    await userManger.AddToRoleAsync(user, "Admin");
                }
            }

            DbInitializer.Seed(app);

            app.Run();
        }
    }
}
