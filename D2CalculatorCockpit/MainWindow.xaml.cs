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
				_ = this.OptWeaponTypeComboBox.Items.Add(new ComboBoxItem
				{
					Foreground = new SolidColorBrush(Color.FromRgb(185, 185, 186)),
					Content = weaponType.Name,
					Tag = weaponType.Id
				});
				_ = this.AdjWeaponTypeComboBox.Items.Add(new ComboBoxItem
				{
					Foreground = new SolidColorBrush(Color.FromRgb(185, 185, 186)),
					Content = weaponType.Name,
					Tag = weaponType.Id
				});
			}
		}

		private void OptimalShotsPerResilClick(object sender, RoutedEventArgs e) 
			=> new OptimalShotsPerResilWindow(
				double.Parse(this.bodyDmgSelector.Text),
				double.Parse(this.critDmgSelector.Text),
				"", "").Show();

		private void OptimalShotsPerResilClick_ArchetypeWIP(object sender, RoutedEventArgs e)
		{
			var archetype = MasterDataCache
				.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.OptWeaponTypeComboBox.SelectedItem).Tag)
				.Archetypes.First(x => x.Id == (int)((ComboBoxItem)this.OptArchetypeComboBox.SelectedItem).Tag);
			var bodyDamage = archetype.BaselineBodyDamage;
			var critDamage = archetype.BaselineCritDamage;

			new OptimalShotsPerResilWindow(
				bodyDamage,
				critDamage,
				archetype.Name,
				MasterDataCache.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.OptWeaponTypeComboBox.SelectedItem).Tag).Name).Show();
		}

		private void WeaponTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (((ComboBox)sender).Name == "OptWeaponTypeComboBox")
			{
				this.OptArchetypeComboBox.IsEnabled = true;
				this.OptArchetypeComboBox.Items.Clear();
			}
			else if (((ComboBox)sender).Name == "AdjWeaponTypeComboBox")
			{
				this.AdjArchetypeComboBox.IsEnabled = true;
				this.AdjArchetypeComboBox.Items.Clear();
			}
			foreach (var archetype in MasterDataCache
				.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)e.AddedItems[0]).Tag).Archetypes)
			{
				var item = new ComboBoxItem
				{
					Foreground = new SolidColorBrush(Color.FromRgb(185, 185, 186)),
					Content = archetype.Name,
					Tag = archetype.Id
				};
				if (((ComboBox)sender).Name == "OptWeaponTypeComboBox")
				{
					_ = this.OptArchetypeComboBox.Items.Add(item);
				}
				else if (((ComboBox)sender).Name == "AdjWeaponTypeComboBox")
				{
					_ = this.AdjArchetypeComboBox.Items.Add(item);
				}
			}
		}

		private void AdjustedShotsPerResilClick(object sender, RoutedEventArgs e)
			=> new AdjustedShotsPerResilWindow(
				double.Parse(this.adjBodyDmgSelector.Text),
				double.Parse(this.adjCritDmgSelector.Text),
				int.Parse(this.damageAccuracySelector.Text),
				"", "").Show();

		private void AdjustedShotsPerResilClick_ArchetypeWIP(object sender, RoutedEventArgs e)
		{
			var archetype = MasterDataCache
				.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.AdjWeaponTypeComboBox.SelectedItem).Tag)
				.Archetypes.First(x => x.Id == (int)((ComboBoxItem)this.AdjArchetypeComboBox.SelectedItem).Tag);
			var bodyDamage = archetype.BaselineBodyDamage;
			var critDamage = archetype.BaselineCritDamage;

			new AdjustedShotsPerResilWindow(
				bodyDamage,
				critDamage,
				int.Parse(archetypeAccuracySelector.Text),
				archetype.Name,
				MasterDataCache.WeaponTypes.First(x => x.Id == (int)((ComboBoxItem)this.AdjWeaponTypeComboBox.SelectedItem).Tag).Name).Show();
		}
	}
}
