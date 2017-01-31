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
		readonly Type _type;

		public AddValueWindow(Type type) {
			InitializeComponent();
			_type = type;
		}

		void Add(object sender, RoutedEventArgs e) {
			ChooseValueType();
			Close();
		}

		void ChooseValueType() {
			object value = null;
			if (_type == typeof(Color)) {
				value = new Color { Name = TextBox.Text };
			}
			if (_type == typeof(Race)) {
				value = new Race { Name = TextBox.Text };
			}

			if (_type == typeof(Line)) {
				value = new Line { Name = TextBox.Text };
			}

			if(value != null)
				Provider.Add(value);
		}
	}

}
