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
			return Ok(brand);
		}


		[HttpPost]
		public ActionResult Post(Brand brand)
		{
			var brands = _context.Brands;
			foreach (var item in brands)
			{
				if(item.Name.ToLower() == brand.Name.ToLower())
				{
					return BadRequest("brand already exists");
				}
			}	
			_context.Brands.Add(brand);
			_context.SaveChanges();
			return Ok(brand); 
		}


		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var brandInDb = _context.Brands.SingleOrDefault(b => b.Id == id);
			if(brandInDb == null)
			{
				return NotFound();
			}
			_context.Brands.Remove(brandInDb);
			_context.SaveChanges();
			return Ok("Delete Brand Success");
		}

		[HttpPut]
		public ActionResult Put(Brand brand)
		{
			var brandInDb = _context.Brands.SingleOrDefault(b => b.Id == brand.Id);
			if (brandInDb == null)
			{
				return NotFound();
			}
			brandInDb.Name = brand.Name;
			_context.SaveChanges();
			return Ok(brandInDb);
		}
	}
}
