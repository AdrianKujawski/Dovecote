// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Dovecote.Windows;

namespace Dovecote {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			UpdateAppSettings("connectionString");
			InitializeComponent();
			ShowPigeonOnList();
			Provider.DataChanged += ShowPigeonOnList;
		}

		List<Pigeon> Pigeons { get; set; }

		void AddPigeon(object sender, RoutedEventArgs e) {
			var window = new AddPigeonWindow();
			window.ShowDialog();
		}

		void ShowLineage(object sender, RoutedEventArgs e) {
			var window = new LineageWindow();
			window.ShowDialog();
		}

		void ShowPigeon(object sender, RoutedEventArgs e) {
			ShowPigeonOnList();
		}

		void ShowPigeonOnList() {
			Pigeons = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			DataGrid.ItemsSource = Pigeons;
			SetPigeonCounter();
		}

		void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e) {
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		void DataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ClearFamilyContext();

			Pigeon pigeon;
			if (GetSelectedPigeon(out pigeon)) return;

			var father = pigeon.GetFather();
			FatherInfo.DataContext = father;
			if (father != null) {
				GrandfatherOneInfo.DataContext = father.GetFather();
				GrandMotherOneInfo.DataContext = father.GetMother();
			}

			var mother = Pigeons.FirstOrDefault(p => p.Id == pigeon.Mother);
			MotherInfo.DataContext = mother;
			if (mother == null) return;

			GrandfatherTwoInfo.DataContext = mother.GetFather();
			GrandMotherTwoInfo.DataContext = mother.GetMother();
		}

		void ClearFamilyContext() {
			FatherInfo.DataContext = null;
			MotherInfo.DataContext = null;
			GrandfatherOneInfo.DataContext = null;
			GrandMotherOneInfo.DataContext = null;
			GrandfatherTwoInfo.DataContext = null;
			GrandMotherTwoInfo.DataContext = null;
		}

		void SetPigeonCounter() {
			if (DataGrid.ItemsSource == null) return;

			PigeonCount.Text = DataGrid.ItemsSource.Cast<Pigeon>().Count().ToString();
			MaleCount.Text = DataGrid.ItemsSource.Cast<Pigeon>().Count(p => p.Gender == "Samiec").ToString();
			FemaleCount.Text = DataGrid.ItemsSource.Cast<Pigeon>().Count(p => p.Gender == "Samica").ToString();
		}

		void AllPigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons;
			SetPigeonCounter();
		}

		void MalePigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Gender == "Samiec");
			SetPigeonCounter();
		}

		void FemalePigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Gender == "Samica");
			SetPigeonCounter();
		}

		void FlyingPigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Statue == "Loty");
			SetPigeonCounter();
		}

		void BreedingPigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Statue == "Rozpłód");
			SetPigeonCounter();
		}

		void YoungPigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Yearbook == DateTime.Now.Year.ToString());
			SetPigeonCounter();
		}

		void RemovePigeon(object sender, RoutedEventArgs e) {
			var resut = MessageBox.Show("Czy na pewno chcesz usunąć tego gołębia?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (resut == MessageBoxResult.No) return;

			Pigeon pigeon;
			if (GetSelectedPigeon(out pigeon)) return;

			Provider.Remove(pigeon);
			DataGrid.Items.Refresh();
		}

		bool GetSelectedPigeon(out Pigeon pigeon) {
			pigeon = DataGrid.SelectedItem as Pigeon;
			return pigeon == null;
		}

		void ShowPigeonInfo(object sender, MouseButtonEventArgs e) {
			var row = (DataGrid)sender;
			var pigeon = (Pigeon)row.SelectedItem;

			var window = new AddPigeonWindow(pigeon);
			window.ShowDialog();
		}

		static void UpdateAppSettings(string keyName) {
			try {
				var xmlDoc = new XmlDocument();

				xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

				if (xmlDoc.DocumentElement != null)
					foreach (XmlElement xElement in xmlDoc.DocumentElement) {
						if (xElement.Name != "connectionStrings") continue;

						foreach (XmlNode xNode in xElement.ChildNodes) {
							if (xNode.Attributes == null) continue;

							foreach (XmlAttribute attribute in xNode.Attributes) {
								if (attribute.Name == keyName) {
									attribute.Value = GetDataSourcePath(attribute.Value);
								}
							}
						}
					}

				xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			}
			catch (Exception exception) {
				MessageBox.Show($"Błąd podczas ustawiania ściężki do pliku Hodowla.sqlite.{Environment.NewLine}{exception}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		static string GetDataSourcePath(string value) {
			var split = value.Split(new[] { "data source=" }, StringSplitOptions.None);
			var path = AppDomain.CurrentDomain.BaseDirectory;
			split[1] = "data source=" + path + "Hodowla.sqlite'";
			return split[0] + split[1];
		}
	}

}
