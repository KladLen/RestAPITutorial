using MagicAPI.Models;
using MagicAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using MagicAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace MagicAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		//private readonly ILogger<VillaAPIController> _logger;

		public VillaAPIController(ApplicationDbContext db)
		{
			_db = db;
		}
		//public VillaAPIController(ILogger<VillaAPIController> logger)
		//{
		//	_logger = logger;
		//}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDTO>> GetVillas()
		{
			//_logger.LogInformation("GetVillas executed");
			return Ok(_db.Villas);
		}

		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDTO> GetVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(villa);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<VillaDTO> Create([FromBody] VillaDTO villaDTO)
		{
			if (_db.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Name of villa already exists");
				return BadRequest(ModelState);
			}
			if (villaDTO == null)
			{
				return BadRequest(villaDTO);
			}
			if (villaDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			//villaDTO.Id = _db.Villas.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;	not needed, because Id is identity column
			Villa modelToAdd = new Villa()
			{
				Name = villaDTO.Name,
				City = villaDTO.City,
				Rate = villaDTO.Rate,
				ImageUrl = villaDTO.ImageUrl
			};
			_db.Villas.Add(modelToAdd);
			_db.SaveChanges();

			return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
		}

		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			else
			{
				_db.Villas.Remove(villa);
				_db.SaveChanges();
				return Ok();
			}
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
		{
			if (villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}
			//var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			//villa.Name = villaDTO.Name;
			//villa.City = villaDTO.City;

			Villa modelToUpadate = new Villa()
			{
				Name = villaDTO.Name,
				City = villaDTO.City,
				Rate = villaDTO.Rate,
				ImageUrl = villaDTO.ImageUrl
			};
			_db.Villas.Update(modelToUpadate);
			_db.SaveChanges();
			return Ok();
		}

		[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);
			
			VillaDTO villaDTO = new VillaDTO()
			{
				Name = villa.Name,
				City = villa.City,
				Rate = villa.Rate,
				ImageUrl = villa.ImageUrl
			};

			if (villa == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(villaDTO, ModelState);
			Villa modelToUpadate = new Villa()
			{
				Name = villaDTO.Name,
				City = villaDTO.City,
				Rate = villaDTO.Rate,
				ImageUrl = villaDTO.ImageUrl
			};
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_db.Villas.Update(modelToUpadate);
			_db.SaveChanges();
			return Ok();
		}
	}
}
