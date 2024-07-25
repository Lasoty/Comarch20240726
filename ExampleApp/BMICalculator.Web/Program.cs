using BMICalculator.Model.Repositories;
using BMICalculator.Services.Interfaces;
using BMICalculator.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BMICalculator.Model.Data;

namespace BMICalculator.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<MetricBmiCalculator>();
            builder.Services.AddScoped<ImperialBmiCalculator>();
            builder.Services.AddScoped<IResultRepository, ResultRepository>();
            builder.Services.AddScoped<IBmiDeterminator, BmiDeterminator>();
            builder.Services.AddScoped<IBmiCalculatorFacade, BmiCalculatorFacade>();
            builder.Services.AddScoped<IBmiCalculatorFactory, BmiCalculatorFactory>();

            // Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<ApplicationDbContext>(opt => 
                opt.UseInMemoryDatabase("BloggingControllerTest")
                   .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            var dbContext = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            //_ = dbContext.Database.EnsureDeleted();
            _ = dbContext.Database.EnsureCreated();
            app.Run();

        }
    }
}