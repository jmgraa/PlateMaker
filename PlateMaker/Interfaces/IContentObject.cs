namespace PlateMaker
{
    public interface IContentObject
    {
        void Serialize();
        void SetPosition(int x, int y);
        void SetSize(int x, int y);
    }
}
