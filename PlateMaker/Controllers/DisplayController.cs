using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PlateMaker.Models;
using PlateMaker.Windows;

namespace PlateMaker.Controllers
{
    internal class DisplayController(MainWindow window)
	{
		public DoorTag? DoorTag = window.DoorTag;
		// TODO FIX NO NEED TO PASS WINDOW
		private MainWindow _window = window;
		private readonly Canvas _mainCanvas = window.MainCanvas;
		private readonly TextBox _textBoxWidth = window.TextBoxWidth;
		private readonly TextBox _textBoxHeight = window.TextBoxHeight;
		private readonly TextBox _textBoxX = window.TextBoxX;
		private readonly TextBox _textBoxY = window.TextBoxY;
		private readonly Button _buttonNumber = window.ButtonNumber;
		private readonly Button _buttonMembers = window.ButtonMembers;
		private readonly Button _buttonChooseLogo = window.ButtonChooseLogo;
		private readonly ComboBox _comboBoxOption = window.ComboBoxOption;
		private readonly Button _buttonSave = window.ButtonSave;
		private readonly Button _buttonPrint = window.ButtonPrint;
		private readonly Button _buttonSet = window.ButtonSet;

		private double _currentScale = 1.0;
		private const double ScaleDelta = 0.1;

		// TODO FIX WINDOW SELECTED OBJECT
		public void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is not ComboBox comboBox) return;

			if (comboBox.SelectedItem is not ComboBoxItem comboBoxItem) return;

			_window.SelectedObject = comboBoxItem.Content switch
			{
				"Numer sali" => DoorTag!.RoomNumber,
				"Osoby w pokoju" => DoorTag!.RoomMembers,
				"Logo" => DoorTag!.Logo,
				_ => throw new ArgumentOutOfRangeException()
			};

			_textBoxWidth.Text = _window.SelectedObject.Width.ToString();
			_textBoxHeight.Text = _window.SelectedObject.Height.ToString();
			_textBoxX.Text = _window.SelectedObject.XPosition.ToString();
			_textBoxY.Text = _window.SelectedObject.YPosition.ToString();
		}

		public void ZoomOnScroll(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta > 0)
			{
				_currentScale += ScaleDelta;
			}
			else
			{
				_currentScale -= ScaleDelta;

				if (_currentScale < 0.1) _currentScale = 0.1;
			}

			_mainCanvas.LayoutTransform = new ScaleTransform(_currentScale, _currentScale);
		}

		public void InputAvailable(bool mode)
		{
			_mainCanvas.IsEnabled = mode;
			_buttonNumber.IsEnabled = mode;
			_buttonMembers.IsEnabled = mode;
			_buttonChooseLogo.IsEnabled = mode;
			_comboBoxOption.IsEnabled = mode;
			_textBoxHeight.IsEnabled = mode;
			_textBoxWidth.IsEnabled = mode;
			_textBoxX.IsEnabled = mode;
			_textBoxY.IsEnabled = mode;
			_buttonSave.IsEnabled = mode;
			_buttonPrint.IsEnabled = mode;

			_mainCanvas.Background = mode ? Brushes.White : Brushes.LightGray;
		}

		public void SelectedObjectOptionsAvailable(bool mode)
		{
			_textBoxWidth.IsEnabled = mode;
			_textBoxHeight.IsEnabled = mode;
			_textBoxX.IsEnabled = mode;
			_textBoxY.IsEnabled = mode;
			_buttonSet.IsEnabled = mode;
		}

		public bool CreateNewTag()
		{
			if (DoorTag == null) return true;

			var result =
				MessageBox.Show(
					"Czy chcesz utworzyć nową tabliczkę?\nNiezapisane zmiany zostaną utracone", "Utrata danych",
					MessageBoxButton.YesNo, MessageBoxImage.Warning);

			return result == MessageBoxResult.Yes;
		}

		public void ResetCanvas()
		{
			window.MainRichTextCanvas.Document.Blocks.Clear();
			window.NumberRichTextCanvas.Document.Blocks.Clear();
		}

		public bool ValidData()
		{
			var canvasWidth = (int)_mainCanvas.ActualWidth;
			var canvasHeight = (int)_mainCanvas.ActualHeight;

			if (int.TryParse(_textBoxHeight.Text, out var height)
			    && int.TryParse(_textBoxWidth.Text, out var width)
				&& int.TryParse(_textBoxX.Text, out var sizeY)
				&& int.TryParse(_textBoxY.Text, out var sizeX))
			{
				var possible = (height >= 0 && width >= 0 && sizeY >= 0 && sizeX >= 0) && ((sizeX + width) <= canvasWidth && (sizeY + height) <= canvasHeight);
				if (!possible)  MessageBox.Show("Niewłaściwe wartości w polach!\nObiekt znajdzie się w całości poza polem roboczym", "Błąd", 
					MessageBoxButton.OK, MessageBoxImage.Error);
				return possible;
			}

			MessageBox.Show("Niepoprawne wartości!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			return false;
		}
	}
}
