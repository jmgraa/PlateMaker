using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace PlateMaker.Models
{
    public class DoorTag
    {
        public string Name;
        public LogoObject Logo { get; set; }
        public RoomNumberObject RoomNumber { get; set; }
        public RoomMembersObjectText RoomMembers { get; set; }

        public DoorTag()
        {
            Logo = new LogoObject();

            /*Logo.Img = new Image
            {
				Source = new BitmapImage(new Uri("C:/Users/jmgra/Downloads/uj_kolorowe.jpg", UriKind.RelativeOrAbsolute))
            };

            Logo.SetSize(200, 200);*/

            RoomNumber = new RoomNumberObject();
            RoomMembers = new RoomMembersObjectText();
        }
    }
}
