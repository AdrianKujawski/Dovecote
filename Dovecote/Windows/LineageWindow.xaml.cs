// -----------------------------------------------------------------------
// <copyright file="LineageWindow.xaml.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dovecote.Windows {

	/// <summary>
	/// Interaction logic for LineageWindow.xaml
	/// </summary>
	public partial class LineageWindow : Window {
		const int Rows = 16;
		const int Columns = 4;

		List<Pigeon> Pigeons { get; set; }

		public LineageWindow() {
			InitializeComponent();
			Pigeons = new List<Pigeon>();
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			ListOfPigeon.ItemsSource = Pigeons;

			ChooseStatus.ItemsSource = Enum.GetNames(typeof(PigeonStatus));
			FillGrid();
		}

		void ChooseStatus_LostFocus(object sender, RoutedEventArgs e) {
			if (ChooseStatus.SelectedItem == null) return;

			var selectedItem = (PigeonStatus)Enum.Parse(typeof(PigeonStatus), (string)ChooseStatus.SelectedItem);
			switch (selectedItem) {
				case PigeonStatus.Loty:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Statue == "Loty");
					break;
				case PigeonStatus.Rozpłód:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Statue == "Rozpłód");
					break;
				case PigeonStatus.Samica:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Gender == "Samica");
					break;
				case PigeonStatus.Samiec:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Gender == "Samiec");
					break;
				case PigeonStatus.Archiwum:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Statue == "Archiwum");
					break;
				case PigeonStatus.Młode:
					ListOfPigeon.ItemsSource = Pigeons.Where(p => p.Yearbook == DateTime.Now.Year.ToString());
					break;
			}
		}

		void FillGrid() {
			var iteracja = 1;
			var borderStyle = FindResource("CutBorder") as Style;
			var boldRedText = FindResource("BoldRedText") as Style;
			var redText = FindResource("RedText") as Style;

			for (var column = Columns - 1; column >= 0; column--) {
				for (var row = 0; row < Rows; row += iteracja) {
					var border = new Border {
						Style = borderStyle
					};

					var stackPanel = new StackPanel();

					
					var ringNo = new TextBlock {
						Text = "Siemaa!!",
						Style = boldRedText
					};
					stackPanel.Children.Add(ringNo);

					if (column != 3) {
						var name = new TextBlock {
							Text = "Zioemk",
							Style = redText
						};
						stackPanel.Children.Add(name);

						var race = new TextBlock {
							Text = "rasa",
							Style = redText
						};
						stackPanel.Children.Add(race);

						var color = new TextBlock {
							Text = "color"
						};
						stackPanel.Children.Add(color);
					}

					border.Child = stackPanel;
					ParentGrid.Children.Add(border);
					Grid.SetRow(border, row);
					Grid.SetColumn(border, column);
					Grid.SetRowSpan(border, iteracja);
				}

				iteracja *= 2;
			}
		}

		void ClearGrid() {
			foreach (var textBlock in FindVisualChildren<TextBlock>(ParentGrid)) {
				textBlock.Text = string.Empty;
			}
			
		}

		static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
			where T : DependencyObject {
			if (depObj == null) yield break;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
				var child = VisualTreeHelper.GetChild(depObj, i);
				var children = child as T;
				if (children != null) {
					yield return children;
				}

				foreach (var childOfChild in FindVisualChildren<T>(child)) {
					yield return childOfChild;
				}
			}
		}

		private void ListOfPigeon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			if (ListOfPigeon.SelectedItem != null) {
				ClearGrid();
			}
		}
	}

}
