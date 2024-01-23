using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PlateMaker.Services
{
	public static class RtfEncoder
    {
        public static void DecodeAndSetRtfText(string withRtf, RichTextBox richTextBox)
        {
            if (withRtf == "")
                return;

            var textPointer = richTextBox.Document.ContentStart;
            using var ms = new MemoryStream(Encoding.Default.GetBytes(withRtf));
            var textRange = new TextRange(textPointer, textPointer);

            textRange.Load(ms, DataFormats.Rtf);
        }

        public static void EncodeAndSetString(out string withRtf, RichTextBox richTextBox)
        {
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            using var ms = new MemoryStream();

            textRange.Save(ms, DataFormats.Rtf);
            withRtf = Encoding.Default.GetString(ms.ToArray());
        }
    }
}
