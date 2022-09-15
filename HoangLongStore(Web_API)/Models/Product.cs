using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore_Web_API_.Models
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
		public string ImageProduct { get; set; }
		//[Required]
		//[ForeignKey("Brand")]
		//public int BrandId { get; set; }
		//public Brand Brand { get; set; }


	}
}
