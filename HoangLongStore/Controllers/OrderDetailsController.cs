using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult Index(int id)
		{
			if (id != 0)
			{
				IEnumerable<OrderDetail> orderDetailsByIdOrder = context.OrderDetails
					.Include(t => t.Product).Where(t => t.OrderId == id).ToList();
				return View(orderDetailsByIdOrder);
			}
			IEnumerable<OrderDetail> orderDetails = context.OrderDetails.Include(t => t.Product)
				.Where(t => t.Order.StatusOrder == Enums.OrderStatus.Unconfirmed).ToList();
			return View(orderDetails);
		}
	}
}
