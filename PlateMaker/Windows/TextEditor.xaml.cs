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

            fontFamilyComboBox.SelectedItem = DefaultFont;
            fontSizeComboBox.SelectedItem = DefaultSize;

            _insideText = _withRtf.RtfString;

            RtfEncoder.DecodeAndSetRtfText(_insideText, richText);
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(BoldCommand, BoldExecuted));
            CommandBindings.Add(new CommandBinding(ItalicCommand, ItalicExecuted));
        }

        private void BoldExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (richText.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight fontWeight 
                && fontWeight == FontWeights.Bold) richText.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else richText.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (richText.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle fontStyle
                && fontStyle == FontStyles.Italic) richText.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else richText.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
	        RtfEncoder.EncodeAndSetString(out _insideText, richText);
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
		    var selectedFontFamily = (fontFamilyComboBox.SelectedItem as ComboBoxItem)!.Content.ToString();

		    if (!double.TryParse((fontSizeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
			        out var selectedFontSize)) return;

		    ChangeFontFamily(new FontFamily(selectedFontFamily!));
		    ChangeFontSize(selectedFontSize);
	    }

	    private void ChangeFontSize(double newSize)
	    {
		    if (richText.Selection.IsEmpty) richText.FontSize = newSize;
		    else richText.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, newSize);
	    }

	    private void ChangeFontFamily(FontFamily newFontFamily)
	    {
		    if (richText.Selection.IsEmpty) richText.FontFamily = newFontFamily;
		    else richText.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, newFontFamily);
	    }
    }
}
