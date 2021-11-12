using System.Linq;
using System.Collections.Generic;

using D2CalculatorDataProvider.Models;

namespace D2CalculatorDataProvider
{
	public class MasterDataCache
	{
		public static List<WeaponType> WeaponTypes { get; set; } = new();

		public static void UpdateCache()
		{
			var lines = Resources.WeaponDamages.Split('\n');
			int addedWeapons = 0,
				addedArchetypes = 0;
			foreach (var line in lines)
			{
				var columns = line.Trim().Split(',');
				if (!WeaponTypes.Any(x => x.Name == columns[0]))
				{
					WeaponTypes.Add(new()
					{
						Id = addedWeapons,
						Name = columns[0]
					});
					addedWeapons++;
				}
				WeaponTypes.Find(x => x.Id == addedWeapons - 1).Archetypes.Add(new()
				{
					Id = addedArchetypes,
					Name = columns[1],
					BaselineBodyDamage = double.Parse(columns[2]),
					CanCrit = bool.Parse(columns[3]),
					BaselineCritDamage = bool.Parse(columns[3]) ? double.Parse(columns[4]) : 0
				});
				addedArchetypes++;
			}
		}
	}
}
