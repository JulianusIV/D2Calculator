using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using D2CalculatorDataProvider;
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
			
			MasterDataCache.GenerateSampleData();

			foreach (var weaponType in MasterDataCache.WeaponTypes)
			{
				_ = this.WeaponTypeComboBox.Items.Add(new ComboBoxItem
				{
					Foreground = new SolidColorBrush(Color.FromRgb(185, 185, 186)),
					Content = weaponType.Name,
					Tag = weaponType.Id
				});
			}
		}

		private void ShotsPerResilClick(object sender, RoutedEventArgs e)
		{
			var window = new ShotsPerResilWindow(double.Parse(this.bodyDmgSelector.Text), double.Parse(this.critDmgSelector.Text));
			window.Show();
		}

		private void ShotsPerResilClick_ArchetypeWIP(object sender, RoutedEventArgs e)
		{
			var archetype = MasterDataCache
				.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.WeaponTypeComboBox.SelectedItem).Tag)
				.Archetypes.First(x => x.Id == (int)((ComboBoxItem)this.ArchetypeComboBox.SelectedItem).Tag);
			var bodyDamage = archetype.BaselineBodyDamage;
			var critDamage = archetype.BaselineCritDamage;

			var window = new ShotsPerResilWindow(bodyDamage, critDamage);
			window.Show();
		}

		private void WeaponTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ArchetypeComboBox.IsEnabled = true;
			this.ArchetypeComboBox.Items.Clear();
			foreach (var archetype in MasterDataCache
				.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.WeaponTypeComboBox.SelectedItem).Tag).Archetypes)
			{
				_ = this.ArchetypeComboBox.Items.Add(new ComboBoxItem
				{
					Foreground = new SolidColorBrush(Color.FromRgb(185, 185, 186)),
					Content = archetype.Name,
					Tag = archetype.Id
				});
			}
		}
	}
}
