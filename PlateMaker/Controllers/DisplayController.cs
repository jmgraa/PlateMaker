using System.Windows;
using PlateMaker.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PlateMaker.Windows;

namespace PlateMaker.Controllers
{
    internal class DisplayController(MainWindow window)
	{
		public DoorTag? DoorTag = window.DoorTag;
		// TODO FIX NO NEED TO PASS WINDOW
		private MainWindow _window = window;
		private readonly Canvas _mainCanvas = window.mainCanvas;
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

		public bool CorrectValuesInTextField()
		{
			var canvasWidth = (int)_mainCanvas.ActualWidth;
			var canvasHeight = (int)_mainCanvas.ActualHeight;

			if (int.TryParse(_textBoxHeight.Text, out var textBoxHeight) &&
			    int.TryParse(_textBoxWidth.Text, out var textBoxWidth) &&
			    int.TryParse(_textBoxX.Text, out var textBoxY) &&
			    int.TryParse(_textBoxY.Text, out var textBoxX))
			{
				if (textBoxHeight < 0 || textBoxHeight > canvasHeight ||
				    textBoxWidth < 0 || textBoxWidth > canvasWidth ||
				    textBoxX < 0 || textBoxX > canvasWidth ||
				    textBoxY < 0 || textBoxY > canvasHeight)
				{
					MessageBox.Show($"Wartości w polach muszą byc dodatnie oraz dla wartości X oraz szerokości mniejsze od: {canvasWidth} oraz dla wartości Y oraz wysokości mniejsze od: {canvasHeight}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
					return false;
				}

				if (textBoxHeight + textBoxY > canvasHeight)
				{
					MessageBox.Show($"Wartości sumy pol X oraz szerokości musi byc mniejsza od: {canvasWidth}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
					return false;
				}

				if (textBoxWidth + textBoxX > canvasWidth)
				{
					MessageBox.Show($"Wartości sumy pol Y oraz wysokości musi byc mniejsza od: {canvasHeight}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
					return false;
				}

				MessageBox.Show("Wartości poprawne", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
				return true;
			}

			MessageBox.Show("Wartości w polach mogą być tylko liczbami", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
			return false;
		}
	}
}
