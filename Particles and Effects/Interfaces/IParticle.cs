using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface IParticle
    {
        RectangleF Boundary { get; set; }

        void Update();

        void Draw();

        void DrawLight();

        void DrawNormal();

        void Push(Vector2 velocity);
    }
}