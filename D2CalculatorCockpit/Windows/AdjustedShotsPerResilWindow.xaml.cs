using System.Collections.Generic;
using System.Linq;
using System.Windows;
using D2Calculator;

namespace D2CalculatorCockpit.Windows
{
	/// <summary>
	/// Interaction logic for AdjustedShotsPerResilWindow.xaml
	/// </summary>
	public partial class AdjustedShotsPerResilWindow : Window
	{
		public AdjustedShotsPerResilWindow(double bodyDamage, double critDamage, int accuracy, string archetype, string weapon)
		{
			InitializeComponent();

			//Clear any present data 
			this.Plot.Plot.Clear();

			//initialize calculator with values the user set
			var calculator = new Calculator(bodyDamage, critDamage);

			//create arrays to hold data
			var bodyShots = new double[11];
			var critShots = new double[11];

			//calculate shotcounts for each level of resilience
			for (var i = 0; i < 11; i++)
			{
				bodyShots[i] = calculator.GetAccuracyAdjBodyShots(ConstantProvider.Resiliences[i], accuracy);
				critShots[i] = calculator.GetAccuracyAdjCritShots(ConstantProvider.Resiliences[i], accuracy);
			}

			//push up bodyshots to be visible above critshots
			for (var i = 0; i < bodyShots.Length; i++)
			{
				bodyShots[i] += critShots[i];
			}

			//add data to plot (bodyshots first so the base of them gets covered by crits)
			this.Plot.Plot.AddBar(bodyShots).Label = "Bodyshots";
			this.Plot.Plot.AddBar(critShots).Label = "Critical hits";

			//label axis
			_ = this.Plot.Plot.XAxis.Label("Resilience Level");
			_ = this.Plot.Plot.YAxis.Label("Shots needed");

			//set up y axis tick labeling
			var axispos = new List<double>();
			var axislabels = new List<string>();
			for (var i = 0; i < bodyShots.Last() + 1; i++)
			{
				axispos.Add(i);
				axislabels.Add(i.ToString());
			}
			this.Plot.Plot.YAxis.ManualTickPositions(axispos.ToArray(), axislabels.ToArray());

			//set up x axis tick labeling
			axispos = new();
			axislabels = new();
			for (var i = 0; i < 11; i++)
			{
				axispos.Add(i);
				axislabels.Add($"Resil {i}");
			}
			this.Plot.Plot.XAxis.ManualTickPositions(axispos.ToArray(), axislabels.ToArray());

			this.Plot.Plot.SetAxisLimits(yMin: 0);

			_ = this.Plot.Plot.Legend();
			this.Plot.Plot.Title($"Accuracy adjusted shots to kill for {archetype} {weapon} with {accuracy}% accuracy");

			//Set plot style
			this.Plot.Plot.Style(ScottPlot.Style.Gray2);

			//reload and display plot
			this.Plot.Refresh();
		}
	}
}
