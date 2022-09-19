using HoangLongStore_Web_API_.Data;
using HoangLongStore_Web_API_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoangLongStore_Web_API_.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public BrandsController(ApplicationDbContext DbContext)
		{
			_context = DbContext;
		}	

		[HttpGet]
		public ActionResult Get()
		{
			var brands = _context.Brands;
			return Ok(brands);
		}
		[HttpGet("{id}")]

		public ActionResult Get(int id)
		{
			var brand = _context.Brands.Include(t => t.Products).SingleOrDefault(b => b.Id == id);
			if (brand == null) { return NotFound(); }

			//var detailBrand = new DetailBrand
			//{
			//	Name = brand.Name
			//};
			
//var productOfBrand = new DetailProduct();
//			foreach (var item in brand.Products)
//			{
				
//				productOfBrand.NameProduct = item.NameProduct;
//				productOfBrand.QuantityProduct = item.QuantityProduct;
//				productOfBrand.PriceProduct = item.PriceProduct;
//				productOfBrand.DescriptionProduct = item.DescriptionProduct;

//				detailBrand.Products.Add(productOfBrand);
			//}
			return Ok(brand);
		}

	}
}
