using HoangLongStore.Data;
using HoangLongStore.Enums;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HoangLongStore.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;
		public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}


		public IActionResult Index()
		{
			IEnumerable<Order> orders = context
				.Orders.Where(t => t.UserId == userManager.GetUserId(User)).ToList();
			return View(orders);
		}


		[HttpGet]
		public IActionResult Create(int id)
		{

			var orderInDb = context.Orders.SingleOrDefault(t => t.StatusOrder == OrderStatus.Unconfirmed &&
			t.UserId == userManager.GetUserId(User));

			if (orderInDb == null)
			{
				var order = new Order
				{
					UserId = userManager.GetUserId(User),
					StatusOrder = OrderStatus.Unconfirmed,
				};

				context.Add(order);
				context.SaveChanges();

				var orderDetail = CreateOrderDetails(id, order.Id);
				if (orderDetail is null) { return NotFound(); }

				order.PriceOrder = GetPriceOfOrder(order.Id);

			}

			else
			{
				var orderDetail = CreateOrderDetails(id, orderInDb.Id);
				if (orderDetail is null) return NotFound();
				orderInDb.PriceOrder = GetPriceOfOrder(orderInDb.Id);

			}
			context.SaveChanges();
			return RedirectToAction("Index", "Orders");
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

		[NonAction]
		internal OrderDetail CreateOrderDetails(int idProduct, int idOrder)
		{
			var productInDb = context.Products.SingleOrDefault(t => t.Id == idProduct);
			if (productInDb.QuantityProduct > 0)
			{
				var orderDetailsInDb = context.OrderDetails.SingleOrDefault(t => t.OrderId == idOrder && t.ProductId == idProduct);
				if (orderDetailsInDb is null)
				{
					OrderDetail newOrderDetail = new OrderDetail();

					newOrderDetail.OrderId = idOrder;
					newOrderDetail.ProductId = idProduct;
					newOrderDetail.Quantity = 1;
					newOrderDetail.Price = productInDb.PriceProduct;
					context.OrderDetails.Add(newOrderDetail);
					context.SaveChanges();
					return newOrderDetail;
				}
				else
				{
					orderDetailsInDb.Quantity += 1;
					orderDetailsInDb.Price = orderDetailsInDb.Quantity * productInDb.PriceProduct;
					context.SaveChanges();

					return orderDetailsInDb;
				}
			}
			else return null;

		}
		[HttpGet]
		public IActionResult Purchase()
		{
			var orderToBuy = context.Orders.SingleOrDefault(t => t.UserId == userManager.GetUserId(User) && t.StatusOrder == OrderStatus.Unconfirmed);
			
			orderToBuy.StatusOrder = OrderStatus.InProgress;
			//foreach(var item in orderToBuy.OrderDetails)
			//{
			//	var productToBuy = context.Products.SingleOrDefault(t => t.)
			//}
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}