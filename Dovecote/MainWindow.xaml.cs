// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dovecote.Windows;

namespace Dovecote {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		List<Pigeon> Pigeons { get; set; }

		public MainWindow() {
			InitializeComponent();
			Provider.DataChanged += ShowPigeonOnList;
		}

		void AddPigeon(object sender, RoutedEventArgs e) {
			var window = new AddPigeonWindow();
			window.ShowDialog();
		}

		void ShowPigeon(object sender, RoutedEventArgs e) {
			ShowPigeonOnList();
		}

		void ShowPigeonOnList() {
			LoadListOfPigeons();
			DataGrid.ItemsSource = Pigeons;
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

		void LoadListOfPigeons() {
			Pigeons = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			PigeonCount.Text = Pigeons.Count.ToString();
			MaleCount.Text = Pigeons.Count(p => p.Gender == "Samiec").ToString();
			FemaleCount.Text = Pigeons.Count(p => p.Gender == "Samica").ToString();
		}


		void AllPigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons;
		}

		void MalePigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Gender == "Samiec");
		}

		void FemalePigeons(object sender, RoutedEventArgs e) {
			DataGrid.ItemsSource = Pigeons.Where(p => p.Gender == "Samica");
		}

		void RemovePigeon(object sender, RoutedEventArgs e) {
			Pigeon pigeon;
			if (GetSelectedPigeon(out pigeon)) return;

			Provider.Remove(pigeon);
			DataGrid.Items.Refresh();
		}

		bool GetSelectedPigeon(out Pigeon pigeon) {
			pigeon = DataGrid.SelectedItem as Pigeon;
			return pigeon == null;
		}
	}

}
