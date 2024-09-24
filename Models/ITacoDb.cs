using Microsoft.EntityFrameworkCore;

namespace TacoStand.Models
{
	public interface ITacoDb
	{
		public DbSet<Drink> Drinks { get; }

		public DbSet<Taco> Tacos { get; }

		public int SaveChanges();
	}
}
