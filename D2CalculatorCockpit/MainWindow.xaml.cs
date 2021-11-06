using System.Windows;

using D2CalculatorCockpit.Windows;

namespace D2CalculatorCockpit
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var window = new ShotsPerResilWindow(double.Parse(this.bodyDmgSelector.Text), double.Parse(this.critDmgSelector.Text));
			window.Show();
		}
	}
}
