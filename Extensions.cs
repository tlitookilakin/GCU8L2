using TacoStand.Models;

namespace TacoStand
{
	public static class Extensions
	{
		public static Combo WithExternal(this Combo combo, ITacoDb db)
		{
			combo.Drink = db.Drinks.Find(combo.DrinkId);
			combo.Taco = db.Tacos.Find(combo.TacoId);
			return combo;
		}
	}
}
