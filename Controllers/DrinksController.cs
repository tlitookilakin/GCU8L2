using Microsoft.AspNetCore.Mvc;
using TacoStand.Models;

namespace TacoStand.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class DrinksController(ITacoDb context) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAll(string? sortByCost = null)
			=> Ok(sortByCost switch {
				"ascending" => context.Drinks.OrderBy(d => d.Cost),
				"descending" => context.Drinks.OrderByDescending(d => d.Cost),
				_ => context.Drinks
			});

		[HttpGet("{id}")]
		public IActionResult Get(int id)
			=> context.Drinks.Find(id) is Drink drink ? Ok(drink) : NotFound();

		[HttpPost]
		public IActionResult AddTaco([FromBody] Drink? drink)
		{
			if (drink is null)
				return BadRequest("No data provided");

			drink.Id = 0;
			context.Drinks.Add(drink);
			context.SaveChanges();
			return Created($"Tacos/{drink.Id}", drink);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (context.Drinks.Find(id) is Drink drink)
			{
				context.Drinks.Remove(drink);
				context.SaveChanges();
				return NoContent();
			}
			return NotFound();
		}
	}
}
