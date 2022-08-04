using HoangLongStore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.ViewModels
{
	public class ProductBrandsViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Brand> Brands { get; set; }
		[Display(Name = "File")]
		public List<IFormFile> FilesImage { get; set; }
	}
}
