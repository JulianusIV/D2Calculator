using System;

namespace D2Calculator
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

		public int GetOptimalTotalShots(int health)
			=> (int)Math.Round(health / this._critDamage, 0, MidpointRounding.ToPositiveInfinity);

		public int GetOptimalBodyShots(int health)
			=> GetOptimalBodyShots(GetOptimalTotalShots(health), health);

		public int GetOptimalBodyShots(int totalShots, int health)
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

		public int GetOptimalCritShots(int health)
			=> GetOptimalCritShots(GetOptimalTotalShots(health), GetOptimalBodyShots(health));

		public int GetOptimalCritShots(int totalShots, int bodyShots)
			=> totalShots - bodyShots;

		public int GetAccuracyAdjTotalShots(int health, int accuracy)
			=> GetAccuracyAdjBodyShots(health, accuracy) + GetAccuracyAdjCritShots(health, accuracy);

		public int GetAccuracyAdjBodyShots(int health, int accuracy)
		{
			//should set accuracy be same or higher than accuracy needed for optimal kill return optimal kill value
			if (accuracy >= (GetOptimalCritShots(health) / GetOptimalTotalShots(health) * 100))
			{
				return GetOptimalBodyShots(health);
			}
			//should body damage be higher than crit damage (:weirdchamp:) return total amount of shots for optimal kill
			else if (this._bodyDamage >= this._critDamage)
			{
				return GetOptimalTotalShots(health);
			}
			//any other case (technically), aka the fun part
			else if (this._bodyDamage < this._critDamage)
			{
				//if accuracy with one more shot, and an according amount of bodyshots is smaller than desired accuracy
				//evaluate with one more shot
				//else evaluate with two more shots
				var addedBulletAmount = AddOneShot(health, accuracy) ? 1 : 2;
				//calculate damage dealt with one more crit than total if optimal kill
				var totalCritDamage = ((GetOptimalTotalShots(health) + addedBulletAmount) * this._critDamage);
				//calculate difference between health and total damage from evaluation above and divide by difference of crit and body damage
				//this will give the allowed amount of bodyshots with a decimal point that will be trimmed with the rounddown
				var shotVariability = (totalCritDamage - health) / (this._critDamage - this._bodyDamage);
				return (int)Math.Round(shotVariability, 0, MidpointRounding.ToNegativeInfinity);
			}
			throw new Exception("If you see this something went wrong. Terribly. I have no idea either.");
		}

		public int GetAccuracyAdjCritShots(int health, int accuracy)
			=> (int)Math.Round((health - (GetAccuracyAdjBodyShots(health, accuracy) * this._bodyDamage)) / this._critDamage, 0, MidpointRounding.ToPositiveInfinity);

		private bool AddOneShot(int health, int accuracy)
		{
			//calculate damage dealt with one more crit than total shot if optimal kill
			var totalCritDamage = (GetOptimalTotalShots(health) + 1) * this._critDamage;
			//calculate difference between health and total damage from evaluation above and divide by difference of crit and body damage
			//this will give the allowed amount of bodyshots with a decimal point that will be trimmed with the rounddown
			var shotVariability = (totalCritDamage - health) / (this._critDamage - this._bodyDamage);
			//damage dealt by the beforehand calculated amount of bodyshots
			var totalBodyDamage = Math.Round(shotVariability, 0, MidpointRounding.ToNegativeInfinity) * this._bodyDamage;
			//the amount of crits needed after the calculated amount of bodyshots
			var critsNeeded = Math.Round((health - totalBodyDamage) / this._critDamage, 0, MidpointRounding.ToPositiveInfinity);
			//no if statement needed here, due to division never returning less than 0
			var bodyShotAmount = critsNeeded;

			bodyShotAmount += Math.Round(shotVariability, 0, MidpointRounding.ToNegativeInfinity);

			//if accuracy is greater or equal than the needed crits at the given accuracy
			//divided by the needed bodyshots at the given accuracy return true
			return accuracy >= (critsNeeded / bodyShotAmount * 100);
		}
	}
}
