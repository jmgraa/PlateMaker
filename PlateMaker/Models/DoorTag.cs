namespace PlateMaker.Models
{
	public class DoorTag
    {
        public string Name = "";
        public LogoObject Logo { get; set; } = new();
        public RoomNumberObject RoomNumber { get; set; } = new();
        public RoomMembersObjectText RoomMembers { get; set; } = new();
    }
}
