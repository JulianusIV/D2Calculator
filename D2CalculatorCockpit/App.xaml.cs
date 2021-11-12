using System.Linq;
using System.Windows;

namespace D2CalculatorCockpit
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static bool KnowsWhatTheyAreDoing { get; set; }

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			KnowsWhatTheyAreDoing = e.Args.Contains("IKnowWhatIAmDoing");
		}
	}
}
