using MagicAPI.Models;
using MagicAPI.Models.DTO;

namespace MagicAPI.Data
{
	public static class VillaStore
	{
		public static List<VillaDTO> villaList = new List<VillaDTO>
		{
			new VillaDTO {Id = 1, Name = "Zosia", City = "Warsaw"},
			new VillaDTO {Id = 2, Name = "Summer", City = "Lublin"}
		};
	}
}
