using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HoangLongStore.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}
    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      this.SeedRoles(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
      builder.Entity<IdentityRole>().HasData(
        new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711",
          Name = "user", ConcurrencyStamp = "1", NormalizedName = "user"},
        new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
          Name = "admin", ConcurrencyStamp = "2", NormalizedName = "admin" }
          );
    }
  }
}
