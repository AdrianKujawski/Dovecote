using System;
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
			DovecoteComboBox.ItemsSource = Provider.GetList<Dovecote>(typeof(Dovecote));
			FatherComboBox.ItemsSource = Provider.GetList<Pigeon>(typeof(Pigeon));
			MotherComboBox.ItemsSource = Provider.GetList<Pigeon>(typeof(Pigeon));
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
			var name = Name.Text;
			var hatched = DatePicker.SelectedDate;
			var eyeColor = (EyeColor)EyeColorComboBox.SelectedItem;
			var dovecote = (Dovecote)DovecoteComboBox.SelectedItem;
			var father = (Pigeon)FatherComboBox.SelectedItem;
			var mother = (Pigeon)MotherComboBox.SelectedItem;

			int gender;
			if (Male.IsChecked != null && (bool)Male.IsChecked) 
				gender = 1;
			else
				gender = 2;


			var pigeon = new Pigeon {
				RingNO = ringNo,
				Color_Id = color.Id,
				Yearbook = yearbook,
				Race_Id = race.Id,
				Line_Id = line.Id,
				Name = name,
				Hatched = hatched,
				EyeColor_Id = eyeColor.Id,
				Dovecote_Id = dovecote.Id,
				Father = father?.Id,
				Mother = mother?.Id,
				Gender_Id = gender
			};

			try {
				Provider.Add(pigeon);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			MessageBox.Show("Pomyślnie dodane gołebia.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}
	}
}
