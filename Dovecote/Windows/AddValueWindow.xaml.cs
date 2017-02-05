// -----------------------------------------------------------------------
// <copyright file="AddValueWindow.xaml.cs">
//     Copyright (c) 2016, Adrian Kujawski. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows;

namespace Dovecote.Windows {

	/// <summary>
	/// 
	/// Interaction logic for AddValue.xaml
	/// </summary>
	public partial class AddValueWindow : Window {
		public Type Type { get; set; }

		public AddValueWindow() {
			InitializeComponent();
		}

		void Add(object sender, RoutedEventArgs e) {
			var result = AddNewValue();
			if(result == Result.Success)
				Close();
		}

		Result AddNewValue() {
			try {
				object value = null;
				if (Type == typeof(Color)) {
					value = new Color { Name = UserValue.Text };
				}
				if (Type == typeof(Race)) {
					value = new Race { Name = UserValue.Text };
				}

				if (Type == typeof(Line)) {
					value = new Line { Name = UserValue.Text };
				}

				if (Type == typeof(EyeColor)) {
					value = new EyeColor { Name = UserValue.Text };
				}

				if (Type == typeof(Dovecote)) {
					value = new Dovecote { Name = UserValue.Text };
				}

				if (Type == typeof(Pigeon)) {
					value = new Pigeon { Name = UserValue.Text };
				}

				if (Type == typeof(Yearbook)) {
					value = new Yearbook { Name = UserValue.Text };
				}

				if (Type == typeof(Category)) {
					value = new Category { Name = UserValue.Text };
				}

				if (value == null) throw new Exception($"Brak cechy {Type}");

				var result = Provider.Add(value);

				if(result == Result.Success)
					return Result.Success;
			}
			catch (Exception exception) {
				MessageBox.Show("Dodanie cechy nie powiodło się." + Environment.NewLine + exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return Result.Error;
				 
		}
	}

}
