using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface Icarry
    {
        RectangleF Boundary { get; }

        void Carry(Vector2 origin);

        void LetGo(Vector2 velocity);
    }
}