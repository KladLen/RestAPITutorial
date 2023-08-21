using MagicAPI.Models;
using MagicAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using MagicAPI.Data;

namespace MagicAPI.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDTO>> GetVillas()
		{
			return Ok(VillaStore.villaList);
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
			var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
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
		public ActionResult<VillaDTO> Create([FromBody]VillaDTO villaDTO) 
		{
			if (VillaStore.villaList.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
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
			villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
			VillaStore.villaList.Add(villaDTO);

			return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);
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
			var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			else
			{
				VillaStore.villaList.Remove(villa);
				return Ok();
			}
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
		{
			if (villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
			villa.Name = villaDTO.Name;
			villa.City = villaDTO.City;
			return Ok();
		}

	}
}
