// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Dovecote.Windows;

namespace Dovecote {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		List<Pigeon> Pigeons { get; set; }

		public MainWindow() {
			InitializeComponent();
			ShowPigeonOnList();
			Provider.DataChanged += ShowPigeonOnList;
		}

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
	}

}
