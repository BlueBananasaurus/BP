using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Battery : CarryBase, Inpc, Icarry
    {
        private Vector2 _oldVelocity;
        private float _transparency;

        public Battery(Vector2 position) :base()
        {
            Boundary = new RectangleF(new Vector2(36, 64), position);
            _resolver = new CollisionResolver(Globals.TileSize);
            _weight = 1f;
            _oldVelocity = Velocity;
            _transparency = 0f;
        }

        public void Update(List<Inpc> npcs)
        {
            _transparency = 0.75f + (float)(Math.Sin(Game1.Time*2+ Globals.GlobalRandom.NextDouble())/4);
            if (Locked == false)
            {
                _oldVelocity = _velocity;

                _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0.2f, new Vector2(0.2f), new Vector2(0.02f), new Vector2(0.3f), Game1.mapLive.MapMovables);

                float changeVel = Math.Abs(_oldVelocity.X - Velocity.X) + Math.Abs(_oldVelocity.Y - Velocity.Y);

                if (Math.Sign(_velocity.X) != Math.Sign(_oldVelocity.X) && (_resolver.TouchLeft || _resolver.TouchRight || _resolver.TouchLeftMovable || _resolver.TouchRightMovable))
                {
                    Sound.PlaySoundPositionVolume(Boundary.Origin, Game1.soundGrenadeHit, changeVel);
                }

                if (Math.Sign(_velocity.Y) != Math.Sign(_oldVelocity.Y) && (_resolver.TouchTop || _resolver.TouchTopMovable || _resolver.TouchBottom || _resolver.TouchBottomMovable))
                {
                    Sound.PlaySoundPositionVolume(Boundary.Origin, Game1.soundGrenadeHit, changeVel);
                }
            }

            Pressure(this);
        }

        public override void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Battery"], Boundary.Position);
        }

        public override void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["batteryNormal"], Boundary.Position);
        }

        public override void DrawLight()
        {
            Effects.ColorEffect(new Vector4(1, 1, 1, _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BatteryLight"], Boundary.Origin, origin: new Vector2(32));
            Effects.ResetEffect3D();
        }
    }
}
