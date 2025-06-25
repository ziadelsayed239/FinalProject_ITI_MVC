using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Services.IServices;
using ITI_MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ITI_MVC.Repository.IRepository;
using ITI_MVC.Repository;
using System.Threading.Tasks;

namespace ITI_MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            builder.Services.AddDbContext<ApplicationDbContext>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            builder.Services.AddIdentity<ApplicationUser, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            


            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByEmailAsync("test@user.com");
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "testuser",
                        Email = "test@user.com",
                        Address = "Test Address",
                        PhoneNumber = "01000000000"
                    };

                    var result = await userManager.CreateAsync(user, "Somaya@123");
                    if (result.Succeeded)
                    {
                        Console.WriteLine("? User created successfully");
                    }
                    else
                    {
                        Console.WriteLine("? Failed to create user:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("?? User already exists.");
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
      
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
