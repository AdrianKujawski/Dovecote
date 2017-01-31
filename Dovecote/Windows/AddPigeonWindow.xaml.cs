using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dovecote.Windows {
	/// <summary>
	/// Interaction logic for AddPigeon.xaml
	/// </summary>
	public partial class AddPigeonWindow : Window {

		AddValueWindow _addValueWindow;

		AddValueWindow AddValueWindow => _addValueWindow ?? new AddValueWindow(ValueType.Color);

		public AddPigeonWindow() {
			InitializeComponent();
			HodowlaEntities entities = new HodowlaEntities();
			ColorComboBox.ItemsSource = entities.Color.ToList();
			RaceComboBox.ItemsSource = entities.Race.ToList();
		}

		void AddColor(object sender, RoutedEventArgs e) {
			var window = new AddValueWindow(ValueType.Color);
			window.Title = "Dodaj kolor";
			window.Show();
		}

		void AddRace(object sender, RoutedEventArgs e) {
			var window = new AddValueWindow(ValueType.Race);
			window.Title = "Dodaj rase";
			window.Show();
		}

		void AddLine(object sender, RoutedEventArgs e) {
			var window = new AddValueWindow(ValueType.Line);
			window.Title = "Dodaj linie";
			window.Show();
		}
	}
}
