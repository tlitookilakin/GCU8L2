using Microsoft.AspNetCore.Mvc;
using TacoStand.Models;

namespace TacoStand.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CombosController(ITacoDb context) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAll()
			=> Ok(context.Combos.ToList().Select(c => c.WithExternal(context)));

		[HttpGet("{id}")]
		public IActionResult Get(int id)
			=> context.Combos.Find(id) is Combo combo ? Ok(combo.WithExternal(context)) : NotFound();

		[HttpPost]
		public IActionResult AddCombo([FromBody] Combo? combo)
		{
			if (combo is null)
				return BadRequest("No data provided");

			combo.Id = 0;
			combo.Drink = null;
			combo.Taco = null;
			context.Combos.Add(combo);
			context.SaveChanges();
			return Created($"Combos/{combo.Id}", combo.WithExternal(context));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (context.Combos.Find(id) is Combo combo)
			{
				context.Combos.Remove(combo);
				context.SaveChanges();
				return NoContent();
			}
			return NotFound();
		}
	}
}
