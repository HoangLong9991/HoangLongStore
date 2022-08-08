using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HoangLongStore.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

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
		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{
			var currentUser = await userManager.GetUserAsync(User);

			if (id != 0)
			{
				DetailOrderViewModel orderDetailsByIdOrder = new DetailOrderViewModel();
				orderDetailsByIdOrder.OrderDetails = context.OrderDetails.Include(t => t.Product)
					.Include(t =>  t.Order).Where(t => t.OrderId == id).ToList();

				orderDetailsByIdOrder.FullName = currentUser.FullName;

				orderDetailsByIdOrder.Address = currentUser.Address;
				orderDetailsByIdOrder.Order = context.Orders.SingleOrDefault(t => t.Id == id);
				return View(orderDetailsByIdOrder);
			}
			DetailOrderViewModel cart = new DetailOrderViewModel();

				cart.OrderDetails = context.OrderDetails.Include(t => t.Product)
				.Include(t => t.Order).Where(t => t.Order.StatusOrder == Enums.OrderStatus.Unconfirmed).ToList();
			cart.Order = context.Orders.SingleOrDefault(t => t.StatusOrder == Enums.OrderStatus.Unconfirmed);
			cart.FullName = currentUser.FullName;

			cart.Address = currentUser.Address;
			return View(cart);
		}
	}
}
