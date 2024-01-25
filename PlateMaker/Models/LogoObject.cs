using System.Windows.Controls;
using System.Windows.Media;

namespace PlateMaker.Models
{
	public class LogoObject : ContentObject
    {
	    private Image _img = new();

	    public Image Img
	    {
		    get => _img;
		    set
		    {
			    if (_img == value) return;

			    _img = value;
			    OnPropertyChanged(nameof(Img));
		    }
	    }

	    public LogoObject() { }
	    public LogoObject(int w, int h, int x, int y, ImageSource s)
	    {
		    _img = new Image
		    {
			    Source = s
		    };

		    Width = w;
		    Height = h;
		    XPosition = x;
		    YPosition = y;
	    }
    }
}
