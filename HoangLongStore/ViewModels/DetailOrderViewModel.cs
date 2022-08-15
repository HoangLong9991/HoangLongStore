using HoangLongStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.ViewModels
{
	public class DetailOrderViewModel
	{
		public string FullName { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public Order Order { get; set; }
		[BindProperty]
		public List<OrderDetail> OrderDetails { get; set; }

	}
}
