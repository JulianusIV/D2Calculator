using System.Collections.Generic;

namespace D2CalculatorDataProvider.Models
{
	public class WeaponType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Archetype> Archetypes { get; set; } = new();
	}
}