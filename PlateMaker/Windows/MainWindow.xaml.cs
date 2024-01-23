using PlateMaker.Controllers;
using PlateMaker.Models;
using PlateMaker.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlateMaker.Windows
{
	public partial class MainWindow : INotifyPropertyChanged
	{
		private DoorTag? _doorTag;
		public DoorTag? DoorTag
		{
			get => _doorTag;

			set
			{
				_doorTag = value;
				_displayController.InputAvailable(_doorTag != null);
				OnPropertyChanged(nameof(DoorTag));
			}
		}

		private ContentObject? _selectedObject;
		public ContentObject? SelectedObject
		{
			get => _selectedObject;
			set
			{
				_selectedObject = value;
				_displayController.SelectedObjectOptionsAvailable(_selectedObject != null);
			}
		}

		private DisplayController _displayController = null!;
		private FileController _fileController = null!;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
	        _displayController.ComboBoxSelectionChanged(sender, e);
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
	        _displayController.ZoomOnScroll(sender, e);
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
	        if (sender is not Button button)
		        return;

	        var buttonName = button.Name;

	        switch (buttonName)
	        {
		        case "ButtonChooseLogo":
			        _fileController.ChooseLogoFile(sender, e);
			        break;
				case "ButtonSet":
					// TODO DATA CLEAR
			        SelectedObject.SetSize(int.Parse(TextBoxWidth.Text), int.Parse(TextBoxHeight.Text));
			        SelectedObject.SetPosition(int.Parse(TextBoxX.Text), int.Parse(TextBoxY.Text));
			        UpdateLayout();
					break;
				case "ButtonNumber":
			        WindowsController.ShowTextEditor(DoorTag!.RoomNumber, NumberRichTextCanvas);
			        break;
				case "ButtonMembers":
			        WindowsController.ShowTextEditor(DoorTag!.RoomMembers, MainRichTextCanvas);
			        break;
				case "ButtonPrint":
			        PrintManager.Print(mainCanvas);
			        break;
				case "ButtonCreateTag":
			        if (!_displayController.CreateNewTag()) return;
			        _displayController.ResetCanvas();
			        DoorTag = new DoorTag();
			        UpdateControllers();
			        MessageBox.Show("Utworzono nową tabliczkę!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
			        break;
	        }
        }
		
        public void InitializeControllers()
        {
	        _displayController = new DisplayController(this);
			_fileController = new FileController(this);
			
			_displayController.InputAvailable(false);
			_displayController.SelectedObjectOptionsAvailable(false);
        }

        private void UpdateControllers()
        {
			_fileController.DoorTag = DoorTag;
			_displayController.DoorTag = DoorTag;

			// TODO FIX THIS - PROPERTY CALL
			DoorTag = DoorTag;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
	        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}