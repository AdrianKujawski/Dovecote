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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dovecote.Windows;

namespace Dovecote {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {


		public MainWindow() {
			InitializeComponent();

			
			FatherInfo.DataContext = Provider.GetList<Pigeon>(typeof(Pigeon));
		}

		void AddPigeon(object sender, RoutedEventArgs e) {
			var window = new AddPigeonWindow();
			window.ShowDialog();
		}

		void ShowPigeon(object sender, RoutedEventArgs e) {
			PigeonList.ItemsSource = Provider.GetList<Pigeon>(typeof(Pigeon));
		}
	}
}
