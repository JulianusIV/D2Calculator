using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Documents;
using D2Calculator.D2Calculator;

namespace D2CalculatorCockpit
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly int[] resils = new int[11]
		{
			185, 186, 187, 188, 189, 190, 192, 194, 196, 198, 200
		};

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Plot.Plot.Clear();

			var calculator = new Calculator(int.Parse(this.bodyDmgSelector.Text), int.Parse(this.critDmgSelector.Text));

			var bodyShots = new double[11];
			var critShots = new double[11];


			for (var i = 0; i < 11; i++)
			{
				bodyShots[i] = calculator.GetBodyShots(this.resils[i]);
				critShots[i] = calculator.GetCritShots(this.resils[i]);
			}

			for (var i = 0; i < bodyShots.Length; i++)
			{
				bodyShots[i] += critShots[i];
			}

			var bar = this.Plot.Plot.AddBar(bodyShots);
			bar.ShowValuesAboveBars = true;
			bar.Label = "Bodyshots";
			bar = this.Plot.Plot.AddBar(critShots);
			bar.Label = "Critical hits";

			_ = this.Plot.Plot.XAxis.Label("Resilience Level");
			_ = this.Plot.Plot.YAxis.Label("Shots needed");

			var axispos = new List<double>();
			var axislabels = new List<string>();
			for (var i = 0; i < bodyShots.Last() + 1; i++)
			{
				axispos.Add(i);
				axislabels.Add(i.ToString());
			}
			this.Plot.Plot.YAxis.ManualTickPositions(axispos.ToArray(), axislabels.ToArray());

			axispos = new();
			axislabels = new();
			for (int i = 0; i < 11; i++)
			{
				axispos.Add(i);
				axislabels.Add($"Resil {i}");
			}
			this.Plot.Plot.XAxis.ManualTickPositions(axispos.ToArray(), axislabels.ToArray());

			this.Plot.Plot.SetAxisLimits(yMin: 0);
			_ = this.Plot.Plot.Legend();

			this.Plot.Refresh();
		}
	}
}
