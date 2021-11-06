using System;

namespace D2Calculator.D2Calculator
{
	public class Calculator
	{
		private readonly double _bodyDamage;
		private readonly double _critDamage;

		public Calculator(double baselineBodyDamage, double baselineCritDamage, params double[] multipliers)
		{
			if (multipliers.Length > 2)
			{
				throw new ArgumentException("Cannot apply more than two multipliers", nameof(multipliers));
			}

			this._bodyDamage = baselineBodyDamage;
			this._critDamage = baselineCritDamage;

			if (multipliers?.Length != 0)
			{
				foreach (var multiplier in multipliers)
				{
					this._bodyDamage *= multiplier;
					this._critDamage *= multiplier;
				}
			}
		}

		public int GetTotalShots(int health)
			=> (int)Math.Round(health / this._critDamage, 0, MidpointRounding.ToPositiveInfinity);

		public int GetBodyShots(int health)
			=> GetBodyShots(GetTotalShots(health), health);

		public int GetBodyShots(int totalShots, int health)
		{
			if (this._bodyDamage * totalShots >= health)
			{
				return totalShots;
			}
			else
			{
				var critsToKill = (int)Math.Round(health / this._critDamage, MidpointRounding.ToPositiveInfinity);
				return (int)Math.Round(((critsToKill * this._critDamage) - health) / (this._critDamage - this._bodyDamage), 0, MidpointRounding.ToNegativeInfinity);
			}
		}

		public int GetCritShots(int health)
			=> GetCritShots(GetTotalShots(health), GetBodyShots(health));

		public int GetCritShots(int totalShots, int bodyShots)
			=> totalShots - bodyShots;
	}
}
