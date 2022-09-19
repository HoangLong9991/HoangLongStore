using HoangLongStore.Data;
using HoangLongStore.DTOs.Responses;
using HoangLongStore.Models;
using HoangLongStore.ViewModels;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
			foreach (var item in result)
			{
				item.ImageProduct = "//" + Request.Host.Value + "/images/" + item.ImageProduct;
			}
			
			return View(result);
		}

		[Authorize(Roles = "admin")]

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new ProductBrandsViewModel
			{
				Brands = context.Brands.ToList()
			};
			return View(viewModel);
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		public IActionResult Create(ProductBrandsViewModel productViewModel)
		{

			if (!ModelState.IsValid) return View(productViewModel);
			if (productViewModel.FilesImage.Count > 0)
			{			int i = 1;

				foreach (var file in productViewModel.FilesImage)
				{
					string namefile = i++ + productViewModel.Product.NameProduct + ".jpg";
					string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
					string fileNameWithPath = Path.Combine(path, namefile);
					using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}
				var newProduct = new Product
				{
					NameProduct = productViewModel.Product.NameProduct,
					QuantityProduct = productViewModel.Product.QuantityProduct,
					PriceProduct = productViewModel.Product.PriceProduct,
					DescriptionProduct = productViewModel.Product.DescriptionProduct,
					ImageProduct = "1" + productViewModel.Product.NameProduct + ".jpg",
					BrandId = productViewModel.BrandId
				};
				context.Add(newProduct);
				context.SaveChanges();
			} 
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
			var productInDb = context.Products.SingleOrDefault(t => t.Id == id);
			if (productInDb == null)
			{
				return NotFound();
			}
			var product = new DetailsProductResponses
			{
				NameProduct = productInDb.NameProduct,
				QuantityProduct = productInDb.QuantityProduct,
				PriceProduct = productInDb.PriceProduct,
				DescriptionProduct = productInDb.DescriptionProduct,
				ImageProductUrl = GetImage(productInDb.NameProduct)
			};
			
			return View(product);

		}
		[NonAction]
		private List<string> GetImage(string nameProduct)
		{
			List<string> image = new List<string>();

			string path = "wwwroot/images/";

			var allfiles = Directory.GetFiles(path);
			foreach (var file in allfiles)
			{
				string stringToReplace = file.Substring(0, 16);

				string fileName = nameProduct + ".jpg";

				if (file.Replace(stringToReplace, "") == fileName)
				{
					string host = Request.Host.Value;
					string domain = "//" + host;
					image.Add(file.Replace("wwwroot", domain));

				}
			}
			return image;
		}
	}
}
