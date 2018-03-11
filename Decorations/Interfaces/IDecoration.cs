using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface IDecoration
    {
        Vector2 Position { set; get; }

        void Draw();

        void DrawNormal();

        void DrawLight();
    }
}