using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.Models
{
	public class Brand
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Description cannot be null ...")]
		public string Name { get; set; }
		List<Product> Products { get; set; }
	}
}
