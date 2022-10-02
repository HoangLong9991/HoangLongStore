using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HoangLongStore.Controllers
{
	[Authorize(Roles = "admin")]
	public class BrandsController : Controller
	{
		private ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public BrandsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IActionResult Index()
		{
				return View(context.Brands.ToList());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Brand brand)
		{
			if (ModelState.IsValid)
			{
				context.Brands.Add(brand);
				context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(brand);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Brand brand = context.Brands.SingleOrDefault(t => t.Id == id);
			return View(brand);
		}

		[HttpPost]
		public IActionResult Edit(Brand brand)
		{
			Brand brandInDb = context.Brands.SingleOrDefault(t => t.Id == brand.Id);

			brandInDb.Name = brand.Name;
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			Brand brandInDb = context.Brands.SingleOrDefault(t => t.Id == id);
			List<Product> productsOfBrand = context.Products.Where(p => p.BrandId == brandInDb.Id).ToList();
			context.Products.RemoveRange(productsOfBrand);
			context.Remove(brandInDb);	
			context.SaveChanges();

			return RedirectToAction("Index");
		}
		
		//[HttpGet]
		//public IActionResult Detail(int id)
		//{
		//	Brand brandInDb = context.Brands.Include(p => p.Products).SingleOrDefault(t => t.Id == id);


		//	//List<Product> productsInDb = context.Products.ToList();

		//	//List<Product> productsOfBrand = productsInDb.Where(p => p.BrandId == brandInDb.Id).ToList();		
		//	ViewBag.BrandName = brandInDb.Name;

		//	return View(brandInDb.Products);
		//}

	}
}
