using PlateMaker.Interfaces;

namespace PlateMaker.Models
{
	public class RoomNumberObject : ContentObject, IRtfString
    {
        private string _number = "";

        public string Number
        {
	        get => _number;
	        set
	        {
		        if (_number == value) return;

		        _number = value;
		        OnPropertyChanged(nameof(Number));
	        }
        }

        public string RtfString
        {
	        get => _number;
	        set => Number = value;
        }
    }
}
