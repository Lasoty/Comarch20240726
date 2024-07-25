using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Emit;

namespace BMICalculator.Model.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BmiMeasurement> BmiMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            var appUser = new IdentityUser
            {
                Id = "62e35d88-d80b-4e17-a35a-62e36ccc4c05",
                Email = "lasoty@o2.pl",
                NormalizedEmail = "LASOTY@O2.PL",
                UserName = "lasoty@o2.pl",
                NormalizedUserName = "LASOTY@O2.PL",
                EmailConfirmed = true,
                ConcurrencyStamp = "a2f34e4a-b485-4c05-8145-43e4332c8502",
                SecurityStamp = "PKOTWAXQLE6FM2XMXQOVJ3JZYWRGYXGG"
            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Qwerty.1");

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = appUser.Id
            });

            builder.Entity<IdentityUser>().HasData(appUser);
        }
    }
}