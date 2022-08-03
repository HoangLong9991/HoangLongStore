using HoangLongStore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoangLongStore.ViewModels
{
	public class ProductBrandsViewModel
	{
		public Product product { get; set; }
		[Display(Name = "File")]
		public List<IFormFile> FilesImage { get; set; }
	}
}
