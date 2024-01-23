using System.ComponentModel;

namespace PlateMaker.Models
{
	public class ContentObject : IContentObject, INotifyPropertyChanged
    {
	    private int _width = 500;
	    private int _height = 500;
	    private int _xPosition;
	    private int _yPosition;

	    public int Width
	    {
		    get => _width;
		    set
		    {
			    if (_width == value) return;

			    _width = value;
			    OnPropertyChanged(nameof(Width));
		    }
	    }

	    public int Height
	    {
		    get => _height;
		    set
		    {
			    if (_height == value) return;

			    _height = value;
			    OnPropertyChanged(nameof(Height));
		    }
	    }

	    public int XPosition
	    {
		    get => _xPosition;
		    set
		    {
			    if (_xPosition == value) return;

			    _xPosition = value;
			    OnPropertyChanged(nameof(XPosition));
		    }
	    }

	    public int YPosition
	    {
		    get => _yPosition;
		    set
		    {
			    if (_yPosition == value) return;

			    _yPosition = value;
			    OnPropertyChanged(nameof(YPosition));
		    }
	    }

	    public void Serialize()
	    {
		    throw new NotImplementedException();
	    }

	    public void SetPosition(int x, int y)
	    {
		    XPosition = x;
		    YPosition = y;
	    }

	    public void SetSize(int width, int height)
	    {
		    Width = width;
		    Height = height;
	    }

	    public event PropertyChangedEventHandler? PropertyChanged;

	    protected virtual void OnPropertyChanged(string propertyName)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
    }
}
