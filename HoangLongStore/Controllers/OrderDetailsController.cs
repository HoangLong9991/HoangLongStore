using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace HoangLongStore.Controllers
{
	public class OrderDetailsController : Controller
	{
		private ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public OrderDetailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}
		public IActionResult Index()
		{
			return View();
		}


		[NonAction]
		internal int GetPriceOfOrder(int idOrder)
		{
			var orderDetails = context.OrderDetails.Where(t => t.OrderId == idOrder);
			int totalPrice = 0;
			foreach (var item in orderDetails)
			{
				totalPrice += item.Price;
			}
			return totalPrice;
		}
	}
}
