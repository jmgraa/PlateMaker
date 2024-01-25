using Newtonsoft.Json;

namespace PlateMaker.Models
{
	internal class DoorTagSerializer
	{
		public RoomNumberObject RoomNumber;
        public RoomMembersObjectText RoomMembers;
        public CustomLogoSerializer Logo;

        public DoorTagSerializer()
        {
	        RoomMembers = new RoomMembersObjectText();
	        RoomNumber = new RoomNumberObject();
	        Logo = new CustomLogoSerializer(null);
        }

        public DoorTagSerializer(DoorTag t)
        {
            RoomMembers = t.RoomMembers;
            RoomNumber = t.RoomNumber;
            Logo = new CustomLogoSerializer(t.Logo);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}