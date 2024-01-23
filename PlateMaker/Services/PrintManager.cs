using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlateMaker.Services
{
    public static class PrintManager
    {
        public static void Print(Canvas canvas)
        {
            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog() != true) return;

            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

            var scale = 1.0;
            var widthFactor = printDialog.PrintableAreaWidth / canvas.ActualWidth;
            var heightFactor = printDialog.PrintableAreaHeight / canvas.ActualHeight;

            if (widthFactor < 1.0 || heightFactor < 1.0) scale = Math.Min(widthFactor, heightFactor);

            canvas.LayoutTransform = new ScaleTransform(scale, scale);
            printDialog.PrintVisual(canvas, "Print Canvas");
        }
    }
}
