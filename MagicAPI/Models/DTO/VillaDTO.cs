using System.ComponentModel.DataAnnotations;

namespace MagicAPI.Models.DTO
{
	public class VillaDTO
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		[MinLength(2)]
		public string Name { get; set; }
		[Required]
		[MaxLength(30)]
		[MinLength(2)]
		public string City { get; set; }
		public double Rate { get; set; }
		public string ImageUrl { get; set; }
	}
}
