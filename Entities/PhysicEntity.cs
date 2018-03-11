using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class PhysicEntity
    {
        public RectangleF Boundary { get; set; }
        public CircleF BoundaryCircle { get; set; }
        protected CollisionResolver _resolver;
        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
        protected Vector2 _maxSpeed;
        protected float _weight;
        protected Vector2 _velocity;

        public virtual void Push(Vector2 velocity)
        {
            _resolver.TouchTop = false;
            Velocity += velocity / _weight;
        }

        public virtual void Draw()
        {

        }

        public virtual void DrawLight()
        {
        }
    }
}