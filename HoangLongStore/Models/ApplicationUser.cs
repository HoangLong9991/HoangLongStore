using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HoangLongStore.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public string Address { get; set; }
		List<Order> Orders { get; set; }
	}
}
