namespace Monogame_GL
{
    public interface IPickable
    {
        void Update();

        void Pick();

        void Draw();

        void DrawLight();
    }
}