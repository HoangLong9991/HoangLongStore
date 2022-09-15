using HoangLongStore_Web_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace HoangLongStore_Web_API_.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}
		public DbSet<Product> Products { get; set; }

	}
}
