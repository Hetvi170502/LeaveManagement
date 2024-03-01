using LeaveManagement_Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.DataContext
{
    public class LeaveManagementDataContext : IdentityDbContext<ApplicationUser>
    {
        public LeaveManagementDataContext(DbContextOptions<LeaveManagementDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "681c4050-f8bf-4e70-abd2-1e222e124976",
                Name = "Manager",
                NormalizedName = "Manager"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "e05fc12c-e4b1-4a7d-9180-d60e2781fdcd",
                Name = "Employe",
                NormalizedName = "Employe"
            });
        }

        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set;}
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
