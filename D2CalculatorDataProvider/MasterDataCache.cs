using System.Linq;
using System.Collections.Generic;

using D2CalculatorDataProvider.Models;

namespace D2CalculatorDataProvider
{
	public class MasterDataCache
	{
		public static List<WeaponType> WeaponTypes { get; set; } = new();

		public static void GenerateSampleData()
		{
			WeaponTypes.Add(new()
			{
				Id = 0,
				Name = "Handcannons"
			});
			WeaponTypes.First(x => x.Id == 0).Archetypes.Add(new()
			{
				Id = 0,
				Name = "Adaptive",
				BaselineBodyDamage = 47,
				BaselineCritDamage = 70
			});
			WeaponTypes.First(x => x.Id == 0).Archetypes.Add(new()
			{
				Id = 1,
				Name = "Aggressive",
				BaselineBodyDamage = 50,
				BaselineCritDamage = 80
			});
			WeaponTypes.Add(new()
			{
				Id = 1,
				Name = "SMGs"
			});
			WeaponTypes.First(x => x.Id == 1).Archetypes.Add(new()
			{
				Id = 0,
				Name = "Precision",
				BaselineBodyDamage = 16,
				BaselineCritDamage = 23
			});
			WeaponTypes.First(x => x.Id == 1).Archetypes.Add(new()
			{
				Id = 1,
				Name = "Adaptive",
				BaselineBodyDamage = 13,
				BaselineCritDamage = 17
			});
		}
	}
}
