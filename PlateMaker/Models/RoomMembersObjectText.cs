using PlateMaker.Interfaces;

namespace PlateMaker.Models
{
	public class RoomMembersObjectText : ContentObject, IRtfString
	{
		private string _members = "";
		
	    public string Members
	    {
		    get => _members;
		    set
		    {
			    if (_members == value) return;

			    _members = value;
			    OnPropertyChanged(nameof(_members));
		    }
	    }

	    public string RtfString
	    {
		    get => _members;
		    set => Members = value;
	    }
	}
}
