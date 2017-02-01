﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Dovecote.Windows {

	/// <summary>
	/// Interaction logic for AddPigeon.xaml
	/// </summary>
	public partial class AddPigeonWindow : Window {

		public AddPigeonWindow() {
			InitializeComponent();
			LoadItemSource();
			Provider.DataChanged += LoadItemSource;
		}

		void LoadItemSource() {
			ColorComboBox.ItemsSource = Provider.GetList<Color>(typeof(Color));
			RaceComboBox.ItemsSource = Provider.GetList<Race>(typeof(Race));
			LineComboBox.ItemsSource = Provider.GetList<Line>(typeof(Line));
			EyeColorComboBox.ItemsSource = Provider.GetList<EyeColor>(typeof(EyeColor));

			var listOfDovecote = Provider.GetList<Dovecote>(typeof(Dovecote));
			DovecoteComboBox.ItemsSource = listOfDovecote;
			FatherInDocecoteComboBox.ItemsSource = listOfDovecote;
			MotherInDocecoteComboBox.ItemsSource = listOfDovecote;

			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			if(FatherInDocecoteComboBox.SelectedItem != null)
				FatherComboBox.ItemsSource = listOfPigeon.Where(p => p.GetFather().Dovecote == FatherInDocecoteComboBox.SelectedItem.ToString());

			if (MotherInDocecoteComboBox.SelectedItem != null)
				MotherComboBox.ItemsSource = listOfPigeon.Where(p => p.GetMother().Dovecote == MotherInDocecoteComboBox.SelectedItem.ToString());
		}

		void AddNewValue(object sender, RoutedEventArgs e) {
			var addValueWindow = new AddValueWindow();

			var button = (Button)sender;
			var tag = button.Tag.ToString();
			if (tag == "Color") {
				addValueWindow.Title = "Dodaj kolor";
				addValueWindow.Type = typeof(Color);
			}
			else if (tag == "Race") {
				addValueWindow.Title = "Dodaj rase";
				addValueWindow.Type = typeof(Race);
			}
			else if (tag == "Line") {
				addValueWindow.Title = "Dodaj line";
				addValueWindow.Type = typeof(Line);
			}
			else if (tag == "EyeColor") {
				addValueWindow.Title = "Dodaj kolor oka";
				addValueWindow.Type = typeof(EyeColor);
			}
			else if (tag == "Dovecote") {
				addValueWindow.Title = "Dodaj gołębnik";
				addValueWindow.Type = typeof(Dovecote);
			}
			else if (tag == "Father") {
				addValueWindow.Title = "Dodaj ojca";
				addValueWindow.Type = typeof(Pigeon);
			}
			else if (tag == "Mother") {
				addValueWindow.Title = "Dodaj matke";
				addValueWindow.Type = typeof(Pigeon);
			}

			addValueWindow.ShowDialog();
		}

		void AddPigeon(object sender, RoutedEventArgs e) {
			var ringNo = RingNo.Text;
			var color = (Color)ColorComboBox.SelectedItem;
			var yearbook = int.Parse(Yearbook.Text);
			var race = (Race)RaceComboBox.SelectedItem;
			var line = (Line)LineComboBox.SelectedItem;
			var name = PigeonName.Text;
			var hatched = DatePicker.SelectedDate;
			var eyeColor = (EyeColor)EyeColorComboBox.SelectedItem;
			var dovecote = (Dovecote)DovecoteComboBox.SelectedItem;
			var father = (Pigeon)FatherComboBox.SelectedItem;
			var mother = (Pigeon)MotherComboBox.SelectedItem;

			string gender;
			if (Male.IsChecked != null && (bool)Male.IsChecked)
				gender = Male.Content.ToString();
			else
				gender = Female.Content.ToString();


			var pigeon = new Pigeon {
				RingNO = ringNo,
				Color = color.Name,
				Yearbook = yearbook,
				Race = race.Name,
				Line = line.Name,
				Name = name,
				Hatched = hatched,
				EyeColor = eyeColor.Name,
				Dovecote = dovecote.Name,
				Father = father?.Id,
				Mother = mother?.Id,
				Gender = gender
			};

			var result = Provider.Add(pigeon);
			
			if(result == Result.Success)
				MessageBox.Show("Pomyślnie dodane gołebia.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}

		void FatherInDocecoteComboBox_LostFocus(object sender, RoutedEventArgs e) {
			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			if (FatherInDocecoteComboBox.SelectedItem != null)
				FatherComboBox.ItemsSource = listOfPigeon.Where(p => p.Dovecote == FatherInDocecoteComboBox.SelectedItem.ToString());
		}

		void MotherInDocecoteComboBox_LostFocus(object sender, RoutedEventArgs e) {
			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			if (MotherInDocecoteComboBox.SelectedItem != null)
				MotherComboBox.ItemsSource = listOfPigeon.Where(p => p.GetMother().Dovecote == MotherInDocecoteComboBox.SelectedItem.ToString());
		}
	}
}
