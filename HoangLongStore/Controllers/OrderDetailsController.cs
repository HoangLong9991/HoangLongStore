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
			DetailOrderViewModel cart = new DetailOrderViewModel();
			cart.FullName = currentUser.FullName;
			cart.Address = currentUser.Address;

			
			if (id != 0)
			{
				cart.OrderDetails = context.OrderDetails.Include(t => t.Product)
				.Include(t => t.Order).Where(t => t.OrderId == id).ToList();

				cart.Order = context.Orders.SingleOrDefault(t => t.Id == id);
			}

				cart.OrderDetails = context.OrderDetails.Include(t => t.Product)
				.Include(t => t.Order).Where(t => t.Order.StatusOrder == Enums.OrderStatus.Unconfirmed).ToList();

				cart.Order = context.Orders.SingleOrDefault(t => t.StatusOrder == Enums.OrderStatus.Unconfirmed);
			
			
			return View(cart);
		}



		[HttpGet]
		public IActionResult Delete (int id)
		{
			var orderDetail = context.OrderDetails.Include(t => t.Order)
				.SingleOrDefault(t => t.Id == id && t.Order.StatusOrder == Enums.OrderStatus.Unconfirmed);
			context.Remove(orderDetail);
			context.SaveChanges();

			var order = context.Orders.SingleOrDefault(t => t.Id == orderDetail.OrderId);
			var orderDetails = context.OrderDetails.Where(t => t.OrderId == orderDetail.OrderId);
			int totalPrice = 0;
			foreach (var item in orderDetails)
			{
				totalPrice += item.Price;
			}
			order.PriceOrder = totalPrice;

			context.SaveChanges();

			return RedirectToAction("Index");
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

		[HttpPost]
		public IActionResult Edit(DetailOrderViewModel a)
		{
			foreach (var item in a.OrderDetails)
			{
				var b = context.OrderDetails.Include(t => t.Product).SingleOrDefault(t => t.Id == item.Id);
				b.Quantity = item.Quantity;
				b.Price = b.Quantity * b.Product.PriceProduct;

			}
			context.SaveChanges();
			return RedirectToAction("Purchase", "Orders");

		}
	}
}
