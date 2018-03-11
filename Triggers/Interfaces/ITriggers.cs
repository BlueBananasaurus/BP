using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface ITriggers
    { 
        string Name { get; set; }
        Vector2 Position { get; set; }
        bool On { get; set; }
        RectangleF Boundary { get; set; }
        string Target { get; set; }

        void TriggerSwitch();

        void Draw();
        void DrawNormal();
        void DrawLight();

        void Update();
    }
}