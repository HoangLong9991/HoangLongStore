﻿using HoangLongStore.Data;
using HoangLongStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
			int i = 1;
			if(!ModelState.IsValid) return View(product);
			if (product.FilesImage.Count > 0)
			{
					foreach (var file in product.FilesImage)
				{
					string namefile = i++ + product.NameProduct +  ".jpg";
					string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
					string fileNameWithPath = Path.Combine(path,namefile);
					using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}
			}
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

		[HttpGet]
		public IActionResult Detail(int id)
		{
			var product = context.Products.SingleOrDefault(t => t.Id == id);

			string path = "wwwroot/images/";


			var allfiles = Directory.GetFiles(path);
			int i = 1;
			foreach (var file in allfiles)
			{
				string stringToReplace = "wwwroot/images/" + i;
				string fileName = product.NameProduct + ".jpg";
				if (file.Replace(stringToReplace, "") == fileName)
				{
						string host = Request.Host.Value;

						string domain = "https://" + host;
						ViewBag.data = file.Replace("wwwroot", domain);

				}
								

			}




			return View(product);
		}

	}
}


