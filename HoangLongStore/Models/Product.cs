using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name Product cannot be null ...")]
		[StringLength(255)]
		public string NameProduct { get; set; }
		public int QuantityProduct { get; set; }	
		public int PriceProduct { get; set; }
		public string DescriptionProduct { get; set; }


	}
}
