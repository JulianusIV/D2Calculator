namespace D2CalculatorDataProvider.Models
{
	public class Archetype
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double BaselineBodyDamage { get; set; }
		public bool CanCrit { get; set; }
		public double BaselineCritDamage { get; set; }
	}
}