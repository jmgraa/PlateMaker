using System.Windows.Media;

namespace PlateMaker.Models
{
	internal class CustomLogoSerializer : ContentObject
	{
		public ImageSource? ImageSource;

		public CustomLogoSerializer(LogoObject? o)
		{
			if (o == null) return;

			Height = o.Height;
			Width = o.Width;

			ImageSource = o.Img.Source;
		}
	}
}