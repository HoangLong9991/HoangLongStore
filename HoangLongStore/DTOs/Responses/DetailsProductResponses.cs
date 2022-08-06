using HoangLongStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HoangLongStore.DTOs.Responses
{
	public class DetailsProductResponses
	{

		public string NameProduct { get; set; }
		public int QuantityProduct { get; set; }
		public int PriceProduct { get; set; }
		public string DescriptionProduct { get; set; }
		public List<string> ImageProductUrl { get; set; }
		public string BrandName { get; set; }
	}
}
