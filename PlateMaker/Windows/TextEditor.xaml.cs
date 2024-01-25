using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using PlateMaker.Interfaces;
using PlateMaker.Services;

namespace PlateMaker.Windows
{
    public partial class TextEditor
    {
        public static readonly RoutedUICommand BoldCommand = new("Bold", "Bold", typeof(MainWindow));
        public static readonly RoutedUICommand ItalicCommand = new("Italic", "Italic", typeof(MainWindow));

        private readonly IRtfString _withRtf;
        private readonly RichTextBox _mainRichTextBox;

        private string _insideText;

        public TextEditor(IRtfString withRtf, RichTextBox mainRichTextBox)
        {
            InitializeComponent();
            InitializeCommands();

            _withRtf = withRtf;
            _mainRichTextBox = mainRichTextBox;

            FontFamilyComboBox.SelectedItem = DefaultFont;
            FontSizeComboBox.SelectedItem = DefaultSize;

            _insideText = _withRtf.RtfString;

            RtfEncoder.DecodeAndSetRtfText(_insideText, RichText);
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(BoldCommand, BoldExecuted));
            CommandBindings.Add(new CommandBinding(ItalicCommand, ItalicExecuted));
        }

        private void BoldExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (RichText.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight fontWeight 
                && fontWeight == FontWeights.Bold) RichText.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else RichText.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (RichText.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle fontStyle
                && fontStyle == FontStyles.Italic) RichText.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else RichText.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
	        RtfEncoder.EncodeAndSetString(out _insideText, RichText);
	        _withRtf.RtfString = _insideText;
	        Close();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
	        Close();
        }

        private void TextEditor_OnClosed(object? sender, EventArgs e)
        {
            _mainRichTextBox.Document.Blocks.Clear();
	        RtfEncoder.DecodeAndSetRtfText(_withRtf.RtfString, _mainRichTextBox);
        }

	    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
		    ApplyFontSettings();
	    }

	    private void ApplyFontSettings()
	    {
		    var selectedFontFamily = (FontFamilyComboBox.SelectedItem as ComboBoxItem)!.Content.ToString();

		    if (!double.TryParse((FontSizeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
			        out var selectedFontSize)) return;

		    ChangeFontFamily(new FontFamily(selectedFontFamily!));
		    ChangeFontSize(selectedFontSize);
	    }

	    private void ChangeFontSize(double newSize)
	    {
		    if (RichText.Selection.IsEmpty) RichText.FontSize = newSize;
		    else RichText.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, newSize);
	    }

	    private void ChangeFontFamily(FontFamily newFontFamily)
	    {
		    if (RichText.Selection.IsEmpty) RichText.FontFamily = newFontFamily;
		    else RichText.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, newFontFamily);
	    }
    }
}
