using Microsoft.AspNetCore.Mvc;
using TacoStand.Models;

namespace TacoStand.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TacosController(ITacoDb context) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAll(bool? softShell = null)
			=> Ok(softShell.HasValue ? context.Tacos.Where(t => t.SoftShell == softShell.Value) : context.Tacos);

		[HttpGet("{id}")]
		public IActionResult Get(int id)
			=> context.Tacos.Find(id) is Taco taco ? Ok(taco) : NotFound();

		[HttpPost]
		public IActionResult AddTaco([FromBody] Taco? taco)
		{
			if (taco is null)
				return BadRequest("No data provided");

			taco.Id = 0;
			context.Tacos.Add(taco);
			context.SaveChanges();
			return Created($"Tacos/{taco.Id}",taco);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (context.Tacos.Find(id) is Taco taco)
			{
				context.Tacos.Remove(taco);
				context.SaveChanges();
				return NoContent();
			}
			return NotFound();
		}
	}
}
