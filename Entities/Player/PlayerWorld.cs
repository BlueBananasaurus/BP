using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class PlayerWorld
    {
        public Vector2 Velocity;
        public bool Walking;
        public IPlayer CurretnObjectControl;
        public Vector2 Speed;

        public PlayerWorld()
        {
            Speed = new Vector2(0.2f);
            Velocity = Vector2.Zero;
            MaxSpeed = new Vector2(0.6f);

            Resolver = new CollisionResolver(Globals.TileSize,Game1.mapLiveWorld);
            Time = 0;
            ControlsActive = true;

            Boundary = new RectangleF(new Vector2(16, 32), Vector2.Zero);
        }

        public bool ControlsActive { get; set; }
        public CollisionResolver Resolver { get; set; }
        public float Time { get; set; }
        public RectangleF Boundary { get; set; }
        public Vector2 MaxSpeed { get; private set; }

        public void Controling()
        {
            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk left world"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk right world"]))
            {
                Velocity -= new Vector2(Speed.X, 0);

                if (Velocity.X < -MaxSpeed.X)
                {
                    Velocity = new Vector2(-MaxSpeed.X, Velocity.Y);
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk right world"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk left world"]))
            {
                Velocity += new Vector2(Speed.X, 0);

                if (Velocity.X > MaxSpeed.X)
                {
                    Velocity = new Vector2(MaxSpeed.X, Velocity.Y);
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk up world"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk down world"]))
            {
                Velocity -= new Vector2(0, Speed.Y);

                if (Velocity.Y < -MaxSpeed.Y)
                {
                    Velocity = new Vector2(Velocity.X, -MaxSpeed.Y);
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk down world"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk up world"]))
            {
                Velocity += new Vector2(0,Speed.Y);

                if (Velocity.Y > MaxSpeed.Y)
                {
                    Velocity = new Vector2(Velocity.X, MaxSpeed.Y);
                }
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera2DWorld.GetViewMatrix());
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlayerWorld"], Boundary.Position);
            Game1.SpriteBatchGlobal.End();
        }

        public void Push(Vector2 velocity)
        {
            Velocity += new Vector2(velocity.X, velocity.Y);
        }

        public void Update(Vector2 mousePos)
        {
            if (ControlsActive == true)
            {
                Controling();
            }

            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Keys.E) && KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.E))
            {
                foreach (City city in Game1.mapLiveWorld.Cities)
                {
                    if (CompareF.RectangleFVsRectangleF(Boundary, city.Boundary))
                    {
                        city.GoInside();
                    }
                }
            }

            Time += Game1.Delta;

            if (ControlsActive == true) Resolver.move(ref Velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.6f), new Vector2(0.1f), Game1.mapLiveWorld.MapMovables, 0,false,map: Game1.mapLiveWorld);

            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Keys.E) && KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.E))
            {
                foreach (ITriggers trgr in Game1.mapLive.mapTriggers)
                {
                    if (CompareF.RectangleFVsRectangleF(Boundary, trgr.Boundary))
                    {
                        trgr.TriggerSwitch();
                    }
                }
            }
        }
    }
}