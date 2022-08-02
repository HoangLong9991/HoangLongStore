using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace HoangLongStore.Controllers
{
	public class ProductsController : Controller
	{
		private ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IActionResult Index()
		{
			var result = context.Products.ToList();

			return View(result);
		}

		[Authorize (Roles = "admin")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Product product)
		{
			if(!ModelState.IsValid) return View(product);
			
			var result = context.Products.Add(product);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "admin")]

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var product = context.Products.SingleOrDefault(t => t.Id == id);
			if (product is null)
			{
				return NotFound();
			}
			return View(product);
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			var productInDb = context.Products.FirstOrDefault(t => t.Id == product.Id);

				productInDb.NameProduct = product.NameProduct;
				productInDb.QuantityProduct += product.QuantityProduct;
				productInDb.PriceProduct = product.PriceProduct;
				productInDb.DescriptionProduct = product.DescriptionProduct;
				
				context.SaveChanges();
				return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var product = context.Products.SingleOrDefault(t => t.Id == id);

			context.Remove(product);
			context.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}
