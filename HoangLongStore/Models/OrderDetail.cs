using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }
		[Required]
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }
		[Required]
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		[Required(ErrorMessage = "Quantity can not be null")]
		public int Quantity { get; set; }
		public int Price { get; set; }
	}
}