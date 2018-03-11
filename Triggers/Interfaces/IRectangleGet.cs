using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface IRectanglePhysics
    {
        string Name { get; }
        RectangleF Boundary { get; set; }
        Vector2 velocity { get; }
        bool On { get; set; }

        void SetOff();

        void SetOn();

        void Update();

        void Draw();

        void DrawNormal();
    }
}