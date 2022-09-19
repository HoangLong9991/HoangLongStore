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
	public class ProductsController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductsController(ApplicationDbContext DbContext)
		{
			_dbContext = DbContext;
		}
		[HttpGet]
		public async Task<ActionResult<List<Product>>> Get()
		{
			return Ok(await _dbContext.Products.ToListAsync());
		}

		[HttpGet("{id}")]
		public ActionResult<List<Product>> Get(int id)
		{
			var product =  _dbContext.Products.Include(b => b.Brand).SingleOrDefault(p => p.Id == id);
			if(product == null) { return NotFound(); }

			return Ok(product);
		}

		[HttpPost]
		public ActionResult Post(Product product)
		{
			_dbContext.Products.Add(product);
			_dbContext.SaveChanges();
			return Ok(_dbContext.Products);
		}
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var product = _dbContext.Products.FirstOrDefault(a => a.Id == id);
			if(product == null)
			{
				return NotFound();
			}
			_dbContext.Products.Remove(product);
			_dbContext.SaveChanges();
			return Ok("Delete Success");
		}

		[HttpPut]
		public ActionResult Put(Product product)
		{
			var productInDb = _dbContext.Products.SingleOrDefault(a => a.Id == product.Id);
			if(productInDb == null)
			{
				return NotFound();
			}
			productInDb.NameProduct = product.NameProduct;
			productInDb.QuantityProduct = product.QuantityProduct;
			productInDb.PriceProduct = product.PriceProduct;
			productInDb.DescriptionProduct = product.DescriptionProduct;
			_dbContext.SaveChanges();
			return Ok(productInDb);
		}
	}
}
