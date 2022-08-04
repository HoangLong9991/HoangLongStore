using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HoangLongStore.Controllers
{
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


	}
}
