using MagicAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Villa> GetVillas()
		{
			return new List<Villa>
			{
				new Villa {Id = 1, Name = "Zosia"},
				new Villa {Id = 2, Name = "Summer"}
			};
		}
	}
}
