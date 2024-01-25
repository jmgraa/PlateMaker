using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PlateMaker.Models;
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

		public static string ChooseDoorTagJson(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				Filter = "Pliki json (*.json)|*.json"
			};

			return openFileDialog.ShowDialog() != true ? "" : File.ReadAllText(openFileDialog.FileName);
		}

		public static void SaveDoorTagJson(string content)
		{
			var saveFileDialog = new SaveFileDialog
			{
				Filter = "Pliki json (*.json)|*.json"
			};

			if (saveFileDialog.ShowDialog() != true) return;

			File.WriteAllText(saveFileDialog.FileName, content);
		}
	}
}
