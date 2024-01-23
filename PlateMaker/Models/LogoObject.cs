using System.Windows.Controls;

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
    }
}
