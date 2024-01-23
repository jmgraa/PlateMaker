using Microsoft.Win32;
using PlateMaker.Models;
using System.Windows;
using System.Windows.Media.Imaging;
using PlateMaker.Windows;

namespace PlateMaker.Controllers
{
    internal class FileController(MainWindow window)
	{
		public DoorTag? DoorTag = window.DoorTag;

		public void ChooseLogoFile(object sender, RoutedEventArgs e)
		{
			if (DoorTag == null)
			{
				MessageBox.Show("Nie została wybrana żadna tabliczka!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var openFileDialog = new OpenFileDialog
			{
				Filter = "Pliki obrazów (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
			};

			if (openFileDialog.ShowDialog() != true) return;

			DoorTag.Logo.Img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
		}
	}
}
