using HoangLongStore_Web_API_.Data;
using HoangLongStore_Web_API_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
	}
}
