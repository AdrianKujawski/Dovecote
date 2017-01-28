// -----------------------------------------------------------------------
// <copyright file="AddValueWindow.xaml.cs">
//     Copyright (c) 2016, Adrian Kujawski. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Dovecote.Windows {

	/// <summary>
	/// 
	/// Interaction logic for AddValue.xaml
	/// </summary>
	public partial class AddValueWindow : Window {
		readonly ValueType _valueType;
		readonly HodowlaEntities _entity;

		public AddValueWindow(ValueType type) {
			InitializeComponent();
			_valueType = type;
			_entity = new HodowlaEntities();
		}

		void Add(object sender, RoutedEventArgs e) {
			ChooseValueType();
			_entity.SaveChanges();
			Close();
		}

		void ChooseValueType() {
			switch (_valueType) {
				case ValueType.Color:
					_entity.Color.Add(new Color { Name = TextBox.Text });
					break;
				case ValueType.Race:
					_entity.Race.Add(new Race { Name = TextBox.Text });
					break;
				case ValueType.Line:
					_entity.Line.Add(new Line() { Name = TextBox.Text });
					break;
			}
		}
	}

}
