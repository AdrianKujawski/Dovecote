// -----------------------------------------------------------------------
// <copyright file="AddPigeonWindow.xaml.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dovecote.Abstract;
using Dovecote.Model;

namespace Dovecote.Windows {

	/// <summary>
	/// Interaction logic for AddPigeon.xaml
	/// </summary>
	public partial class AddPigeonWindow : Window {
		BitmapImage _pigeonImage;
		BitmapImage _eyeImage;
		const double MaxImageSize = 800;
		readonly Pigeon _editedPigeon;

		public AddPigeonWindow() {
			Initialize();
			SaveClick = AddPigeon;
		}

		public AddPigeonWindow(Pigeon pigeon) {
			Initialize();
			_editedPigeon = pigeon;
			ColorComboBox.SelectedIndex = FindValue(ColorComboBox, pigeon.Color);
			RaceComboBox.SelectedIndex = FindValue(RaceComboBox, pigeon.Race);
			LineComboBox.SelectedIndex = FindValue(LineComboBox, pigeon.Line);
			EyeColorComboBox.SelectedIndex = FindValue(EyeColorComboBox, pigeon.EyeColor);
			YearComboBox.SelectedIndex = FindValue(YearComboBox, pigeon.Yearbook);
			CategoryComboBox.SelectedIndex = FindValue(CategoryComboBox, pigeon.Category);
			DovecoteComboBox.SelectedIndex = FindValue(DovecoteComboBox, pigeon.Dovecote);
			CategoryComboBox.SelectedIndex = FindValue(CategoryComboBox, pigeon.Category);

			var father = pigeon.GetFather();
			var mother = pigeon.GetMother();
			FatherInDovecoteComboBox.SelectedIndex = FindValue(FatherInDovecoteComboBox, father?.Dovecote);
			MotherInDovecoteComboBox.SelectedIndex = FindValue(MotherInDovecoteComboBox, mother?.Dovecote);
			GetParentList();

			FatherComboBox.SelectedIndex = FindValueParent(FatherComboBox, father?.RingNO);
			MotherComboBox.SelectedIndex = FindValueParent(MotherComboBox, mother?.RingNO);

			PigeonImage.Source = ImagePicker.LoadImage(pigeon.Image);
			EyeImage.Source = ImagePicker.LoadImage(pigeon.EyeImage);
			SetRadioButton(pigeon);
			SetCheckBox(pigeon);
			RingNo.Text = pigeon.RingNO;
			PigeonName.Text = pigeon.Name;

			if(pigeon.Hatched != null)
				DatePicker.SelectedDate = (DateTime)pigeon.Hatched;

			SaveClick = SaveChanges;
		}

		int FindValue(ComboBox comboBox, string value) {
			for (var index = 0; index < comboBox.Items.Count; index++) {
				var item = (ITable)comboBox.Items[index];
				if (item.Name == value) {
					return index;
				}
			}
			return -1;
		}

		int FindValueParent(ComboBox comboBox, string value) {
			for (var index = 0; index < comboBox.Items.Count; index++) {
				var item = (Pigeon)comboBox.Items[index];
				if (item.RingNO == value) {
					return index;
				}
			}
			return -1;
		}

		void SetRadioButton(Pigeon pigeon) {
			if (pigeon.Gender.Equals(Male.Content.ToString()))
				Male.IsChecked = true;
			if (pigeon.Gender.Equals(Female.Content.ToString()))
				Female.IsChecked = true;
		}

		void SetCheckBox(Pigeon pigeon) {
			if (pigeon.Statue == null) return;

			if (pigeon.Statue.Equals(Rozplod.Content.ToString()))
				Rozplod.IsChecked = true;
			if (pigeon.Statue.Equals(Loty.Content.ToString()))
				Loty.IsChecked = true;
			if (pigeon.Statue.Equals(Archiwum.Content.ToString()))
				Archiwum.IsChecked = true;
		}

		void Initialize() {
			InitializeComponent();
			LoadItemSource();
			Provider.DataChanged += LoadItemSource;
		}

		void LoadItemSource() {
			ColorComboBox.ItemsSource = Provider.GetList<Color>(typeof(Color));
			RaceComboBox.ItemsSource = Provider.GetList<Race>(typeof(Race));
			LineComboBox.ItemsSource = Provider.GetList<Line>(typeof(Line));
			EyeColorComboBox.ItemsSource = Provider.GetList<EyeColor>(typeof(EyeColor));
			YearComboBox.ItemsSource = Provider.GetList<Yearbook>(typeof(Yearbook));
			CategoryComboBox.ItemsSource = Provider.GetList<Category>(typeof(Category));

			var listOfDovecote = Provider.GetList<Dovecote>(typeof(Dovecote));
			DovecoteComboBox.ItemsSource = listOfDovecote;
			FatherInDovecoteComboBox.ItemsSource = listOfDovecote;
			MotherInDovecoteComboBox.ItemsSource = listOfDovecote;

			GetParentList();
		}

		void GetParentList() {
			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));

			if (FatherInDovecoteComboBox.SelectedItem != null)
				FatherComboBox.ItemsSource = listOfPigeon.Where(p => p.Dovecote == FatherInDovecoteComboBox.SelectedItem.ToString() && p.Gender == "Samiec");

			if (MotherInDovecoteComboBox.SelectedItem != null)
				MotherComboBox.ItemsSource = listOfPigeon.Where(p => p.Dovecote == MotherInDovecoteComboBox.SelectedItem.ToString() && p.Gender == "Samica");
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
			if (tag == "Year") {
				addValueWindow.Title = "Dodaj Rocznik";
				addValueWindow.Type = typeof(Yearbook);
			}
			else if (tag == "Category") {
				addValueWindow.Title = "Dodaj kategorie";
				addValueWindow.Type = typeof(Category);
			}

			addValueWindow.ShowDialog();
		}

		void Confirm(object sender, RoutedEventArgs e) {
			SaveClick?.Invoke();
		}

		void SaveChanges() {
			try {
				Pigeon pigeon;
				if (GetNewPigeon(out pigeon)) return;

				var result = Provider.EditPigeon(pigeon, _editedPigeon?.Id);

				if (result == Result.Success)
					MessageBox.Show("Pomyślnie edytowano gołebia.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
				Close();
			}
			catch (Exception ex) {
				MessageBox.Show("Nie udało się edytować gołebia" + Environment.NewLine + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		void AddPigeon() {
			try {
				Pigeon pigeon;
				if (GetNewPigeon(out pigeon)) return;

				var result = Provider.Add(pigeon);

				if (result == Result.Success)
					MessageBox.Show("Pomyślnie dodane gołebia.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
				Close();
			}
			catch (Exception ex) {
				MessageBox.Show("Nie udało się dodać gołebia" + Environment.NewLine + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		bool GetNewPigeon(out Pigeon pigeon) {
			var ringNo = RingNo.Text;
			var color = (Color)ColorComboBox.SelectedItem;
			var yearbook = (Yearbook)YearComboBox.SelectedItem;
			var race = (Race)RaceComboBox.SelectedItem;
			var line = (Line)LineComboBox.SelectedItem;
			var name = PigeonName.Text;
			var hatched = DatePicker.SelectedDate;
			var eyeColor = (EyeColor)EyeColorComboBox.SelectedItem;
			var dovecote = (Dovecote)DovecoteComboBox.SelectedItem;
			var father = (Pigeon)FatherComboBox.SelectedItem;
			var mother = (Pigeon)MotherComboBox.SelectedItem;
			var category = (Category)CategoryComboBox.SelectedItem;

			var status = GetStatus();
			var gender = GetGender();

			var pigeonImage = GetImage(_pigeonImage);
			var eyeImage = GetImage(_eyeImage);

			if (!CheckData()) {
				pigeon = null;
				return true;
			}

			pigeon = new Pigeon {
				RingNO = ringNo,
				Color = color?.Name,
				Yearbook = yearbook.Name,
				Race = race?.Name,
				Line = line?.Name,
				Name = name,
				Hatched = hatched,
				EyeColor = eyeColor?.Name,
				Dovecote = dovecote?.Name,
				Father = father?.Id,
				Mother = mother?.Id,
				Gender = gender,
				Statue = status,
				Category = category?.Name,
				Image = ImagePicker.ConvertToByteArray(pigeonImage, new JpegBitmapEncoder()),
				EyeImage = ImagePicker.ConvertToByteArray(eyeImage, new JpegBitmapEncoder())
			};
			return false;
		}

		string GetGender() {
			string gender;
			if (Male.IsChecked != null && (bool)Male.IsChecked)
				gender = Male.Content.ToString();
			else
				gender = Female.Content.ToString();
			return gender;
		}

		string GetStatus() {
			var status = string.Empty;
			foreach (var checkBox in FindVisualChildren<CheckBox>(StatusToCheck)) {
				if (checkBox.IsChecked != null && (bool)checkBox.IsChecked) {
					status = (string)checkBox.Content;
				}
			}

			return status;
		}

		BitmapSource GetImage(BitmapImage image) {
			if (image == null) return null;

			return CheckImageSize(image) ? image : image; //ResizeImage(image);
		}

		bool CheckData() {
			var message = "Musisz podać:" + Environment.NewLine;
			var isCorrect = true;

			if (string.IsNullOrEmpty(RingNo.Text)) {
				isCorrect = false;
				message += " - numer obrączki" + Environment.NewLine;
			}

			if (YearComboBox.SelectedItem == null) {
				isCorrect = false;
				message += " - rocznik" + Environment.NewLine;
			}

			if (DovecoteComboBox.SelectedItem == null) {
				isCorrect = false;
				message += " - gołębnik" + Environment.NewLine;
			}

			if (isCorrect) return true;

			MessageBox.Show(message, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
			return false;
		}

		void FatherInDocecoteComboBox_LostFocus(object sender, RoutedEventArgs e) {
			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			if (FatherInDovecoteComboBox.SelectedItem != null)
				FatherComboBox.ItemsSource = listOfPigeon.Where(p => p.Dovecote == FatherInDovecoteComboBox.SelectedItem.ToString() && p.Gender == "Samiec");
		}

		void MotherInDocecoteComboBox_LostFocus(object sender, RoutedEventArgs e) {
			var listOfPigeon = (List<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon));
			if (MotherInDovecoteComboBox.SelectedItem != null)
				MotherComboBox.ItemsSource = listOfPigeon.Where(p => p.Dovecote == MotherInDovecoteComboBox.SelectedItem.ToString() && p.Gender == "Samica");
		}

		void ToggleButton_OnChecked(object sender, RoutedEventArgs e) {
			var clickedCheckBox = (CheckBox)sender;
			foreach (var checkBox in FindVisualChildren<CheckBox>(StatusToCheck)) {
				if (!Equals(clickedCheckBox, checkBox))
					checkBox.IsChecked = false;
			}
		}

		static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
			if (depObj == null) yield break;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
				var child = VisualTreeHelper.GetChild(depObj, i);
				if (child is T children) {
					yield return children;
				}

				foreach (var childOfChild in FindVisualChildren<T>(child)) {
					yield return childOfChild;
				}
			}
		}

		void AddPigeonImage(object sender, RoutedEventArgs e) {
			_pigeonImage = ImagePicker.GetImage();
			PigeonImage.Source = _pigeonImage;
		}

		void AddEyeImage(object sender, RoutedEventArgs e) {
			_eyeImage = ImagePicker.GetImage();
			EyeImage.Source = _eyeImage;
		}

		bool CheckImageSize(BitmapSource image) {
			return image.PixelHeight <= MaxImageSize && image.PixelWidth <= MaxImageSize;
		}

		//TODO
		BitmapSource ResizeImage(BitmapSource image) {
			double highestSize = Math.Max(image.PixelWidth, image.PixelHeight);
			var scale = Math.Round(MaxImageSize / highestSize, 2, MidpointRounding.ToEven);
			var newWidth = Math.Floor(scale * image.PixelWidth);
			var newHeight = Math.Floor(scale * image.PixelHeight);
			var newImage = new TransformedBitmap(image, new ScaleTransform(newWidth, newHeight));
			return newImage.Source;
		}

		public delegate void SaveOrEdit();
		public static event SaveOrEdit SaveClick;
	}

}
