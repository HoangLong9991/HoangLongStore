using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HoangLongStore.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }

	
	}
}