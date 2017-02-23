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
using System.Windows.Input;
using System.Windows.Media;

namespace Dovecote.Windows {

	/// <summary>
	/// Interaction logic for LineageWindow.xaml
	/// </summary>
	public partial class LineageWindow : Window {
		const int Rows = 16;
		const int Columns = 4;
		static int lol = 0;

		List<Pigeon> Pigeons { get; set; }

		public LineageWindow() {
			InitializeComponent();
			Pigeons = new List<Pigeon>();
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			Pigeons.AddRange((IEnumerable<Pigeon>)Provider.GetList<Pigeon>(typeof(Pigeon)));
			ListOfPigeon.ItemsSource = Pigeons;

			ChooseStatus.ItemsSource = Enum.GetNames(typeof(PigeonStatus));
			FillGrid(null);
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

		void FillGrid(Pigeon pigeon) {
				CreateBorders(pigeon?.GetFather(), 1);
				CreateBorders(pigeon?.GetMother(), 1, 8);
				Console.WriteLine("Zrobilem: " + lol);
		}

		void ListOfPigeon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			var selectedPigeon = (Pigeon)ListOfPigeon.SelectedItem;

			if (selectedPigeon == null) return;

			MainPigeon.DataContext = selectedPigeon;
			ClearGrid();
			FillGrid(selectedPigeon);
		}

		void ClearGrid() {
			foreach (var textBlock in FindVisualChildren<TextBlock>(ParentGrid)) {
				textBlock.Text = string.Empty;
			}
		}

		//void CreateBorders(int column, Pigeon pigeon) {
		//	var currentRow = 0;
		//	var borders = (int)Math.Pow(2, column);
		//	var span = borders == Rows ? 1 : Rows / borders;
		//	for (var i = 0; i < borders; i++) {
		//		var borderStyle = FindResource("CutBorder") as Style;
		//		var boldRedText = FindResource("BoldRedText") as Style;
		//		var redText = FindResource("RedText") as Style;

		//		var border = new Border { Style = borderStyle };
		//		var stackPanel = new StackPanel();

		//		var ringNo = new TextBlock { Style = boldRedText, Text = pigeon?.RingNO };
		//		stackPanel.Children.Add(ringNo);

		//		if (column != 4) {
		//			var name = new TextBlock { Style = redText, Text = pigeon?.Name };
		//			stackPanel.Children.Add(name);

		//			var race = new TextBlock { Style = redText, Text = pigeon?.Race };
		//			stackPanel.Children.Add(race);

		//			var color = new TextBlock { Text = pigeon?.Color };
		//			stackPanel.Children.Add(color);
		//		}

		//		border.Child = stackPanel;
		//		ParentGrid.Children.Add(border);
		//		Grid.SetRow(border, currentRow);
		//		Grid.SetColumn(border, column - 1);
		//		Grid.SetRowSpan(border, span);
		//		currentRow += span;
		//	}
		//}

		void CreateBorders(Pigeon pigeon, int column, int currentRow = 0) {
			if (column > 4) return;

			var borders = (int)Math.Pow(2, column);
			var span = borders == Rows ? 1 : Rows / borders;

			var borderStyle = FindResource("CutBorder") as Style;
			var boldRedText = FindResource("BoldRedText") as Style;
			var redText = FindResource("RedText") as Style;

			var border = new Border { Style = borderStyle };
			var stackPanel = new StackPanel();

			var ringNo = new TextBlock { Style = boldRedText, Text =pigeon?.RingNO };
			stackPanel.Children.Add(ringNo);

			if (column != 4) {
				var name = new TextBlock { Style = redText, Text = pigeon?.Name };
				stackPanel.Children.Add(name);

				var race = new TextBlock { Style = redText, Text = pigeon?.Race };
				stackPanel.Children.Add(race);

				var color = new TextBlock { Text = pigeon?.Color };
				stackPanel.Children.Add(color);
			}
			border.Child = stackPanel;
			ParentGrid.Children.Add(border);
			Grid.SetRow(border, currentRow);
			Grid.SetColumn(border, column - 1);
			Grid.SetRowSpan(border, span);
			column++;
			CreateBorders(pigeon?.GetFather(), column, currentRow);
			currentRow += span/2;
			CreateBorders(pigeon?.GetMother(), column, currentRow);
		}

		static
			IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
			where T : DependencyObject {
			if
				(depObj == null) yield break;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
				var child = VisualTreeHelper.GetChild(depObj, i);

				var children = child as T;
				if
				(children
				!= null) {
					yield return children;
				}

				foreach
					(var childOfChild in FindVisualChildren<T>(child)) {
					yield
						return
						childOfChild;
				}
			}
		}
	}

}
