using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_GL
{
    public class Elevator : IRectanglePhysics
    {
        public Vector2 Start;
        public float Speed;
        public float SpeedMax;
        public Movable _platform;
        public bool On { get; set; }
        public string Name { get; }
        public List<Vector2> Nodes;
        private int from;

        public RectangleF Boundary
        {
            get
            {
                return _platform.Boundary;
            }
            set
            {
                _platform.Boundary = value;
            }
        }

        public Vector2 velocity
        {
            get
            {
                if (Speed != 0)
                    return _platform.Velocity;
                else return Vector2.Zero;
            }
        }

        [JsonConstructor]
        public Elevator(Vector2 start, float speed, string name, List<Vector2> nodes)
        {
            Nodes = new List<Vector2>();
            Name = name;
            _platform = new Movable(new RectangleF(64, 32, start.X, start.Y));
            Start = start;
            SpeedMax = speed;
            On = false;
            Nodes = nodes;
            from = 0;
        }

        public Elevator(Vector2 start, float speed, string name, params Vector2[] nodes)
        {
            Nodes = new List<Vector2>();
            Name = name;
            _platform = new Movable(new RectangleF(64, 32, start.X - 32, start.Y - 16));
            Start = start;
            SpeedMax = speed;
            On = true;
            Nodes.AddRange(nodes);
        }

        public void SetOff()
        {
            On = false;
        }

        public void SetOn()
        {
            On = true;
        }

        public void Update()
        {
            if (On == false)
            {
                Speed = 0;
            }
            else
            {
                Speed = SpeedMax;

                if (from == 0)
                {
                    _platform.Update(new LineSegmentF(Start, Nodes[0]).NormalizedWithZeroSolution() * Speed);

                    if (LineSegmentF.Lenght(Start, Nodes[0]) < LineSegmentF.Lenght(Start, _platform.Boundary.Origin))
                    {
                        _platform.Origin = Nodes[0];
                        from++;
                    }
                }

                if (from > 0 && from < Nodes.Count)
                {
                    _platform.Update(new LineSegmentF(Nodes[from - 1], Nodes[from]).NormalizedWithZeroSolution() * Speed);

                    if (LineSegmentF.Lenght(Nodes[from - 1], Nodes[from]) < LineSegmentF.Lenght(Nodes[from - 1], _platform.Boundary.Origin))
                    {
                        _platform.Origin = Nodes[from];
                        from++;
                    }
                }

                if (from == Nodes.Count)
                {
                    _platform.Update(new LineSegmentF(Nodes[from - 1], Start).NormalizedWithZeroSolution() * Speed);

                    if (LineSegmentF.Lenght(Nodes[from - 1], Start) < LineSegmentF.Lenght(Nodes[from - 1], _platform.Boundary.Origin))
                    {
                        _platform.Origin = Start;
                        from = 0;
                    }
                }
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["ElevatorClawBack"], _platform.Origin, origin: new Vector2(18*2), rotation: _platform.Velocity.ToAngle());


            Game1.SpriteBatchGlobal.Draw(Game1.liftRail, position: Start, sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Start, Nodes[0]),14*2), origin: new Vector2(0, 7*2), rotation: new LineSegmentF(Start, Nodes[0]).ToAngle());

            if (Nodes.Count > 1)
            {
                for (int i = 0; i < Nodes.Count - 1; i++)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.liftRail, position: Nodes[i], sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Nodes[i], Nodes[i + 1]),14*2),origin: new Vector2(0,7*2), rotation: new LineSegmentF(Nodes[i], Nodes[i + 1]).ToAngle());
                }
            }

            if(Nodes.Count>2)
            Game1.SpriteBatchGlobal.Draw(Game1.liftRail, position: Nodes[Nodes.Count - 1], sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Nodes[Nodes.Count - 1], Start),14*2), origin: new Vector2(0, 7*2), rotation: new LineSegmentF(Nodes[Nodes.Count - 1], Start).ToAngle());

            Game1.SpriteBatchGlobal.Draw(Game1.liftEnd, position: Start, origin: new Vector2(11*2));

            foreach (Vector2 node in Nodes)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.liftEnd, position: node, origin: new Vector2(11*2));
            }

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["ElevatorClaw"], _platform.Origin, origin: new Vector2(18*2), rotation: _platform.Velocity.ToAngle());
            _platform.Draw(Game1.platformTex);
        }

        public void DrawNormal()
        {
            Game1.EffectRotateNormals.Parameters["flip"].SetValue(new Vector2(1, 1));
            Game1.EffectRotateNormals.Parameters["angle"].SetValue(new LineSegmentF(Start, Nodes[0]).ToAngle());
            Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["liftRailNormal"], position: Start, sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Start, Nodes[0]), 14 * 2), origin: new Vector2(0, 7 * 2), rotation: new LineSegmentF(Start, Nodes[0]).ToAngle());

            if (Nodes.Count > 1)
            {
                for (int i = 0; i < Nodes.Count - 1; i++)
                {
                    Game1.EffectRotateNormals.Parameters["angle"].SetValue(new LineSegmentF(Nodes[i], Nodes[i + 1]).ToAngle());
                    Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();

                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["liftRailNormal"], position: Nodes[i], sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Nodes[i], Nodes[i + 1]), 14 * 2), origin: new Vector2(0, 7 * 2), rotation: new LineSegmentF(Nodes[i], Nodes[i + 1]).ToAngle());
                }
            }

            if (Nodes.Count > 2)
            {
                Game1.EffectRotateNormals.Parameters["angle"].SetValue(new LineSegmentF(Nodes[Nodes.Count - 1], Start).ToAngle());
                Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["liftRailNormal"], position: Nodes[Nodes.Count - 1], sourceRectangle: new Rectangle(0, 0, (int)LineSegmentF.Lenght(Nodes[Nodes.Count - 1], Start), 14 * 2), origin: new Vector2(0, 7 * 2), rotation: new LineSegmentF(Nodes[Nodes.Count - 1], Start).ToAngle());
            }

            Effects.ResetEffect3D();

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["liftEndNormal"], position: Start, origin: new Vector2(11 * 2));

            foreach (Vector2 node in Nodes)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["liftEndNormal"], position: node, origin: new Vector2(11 * 2));
            }

            Game1.EffectRotateNormals.Parameters["angle"].SetValue(_platform.Velocity.ToAngle());
            Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["elevatorClawNormal"], _platform.Origin, origin: new Vector2(18 * 2), rotation: _platform.Velocity.ToAngle());
            Effects.ResetEffect3D();

            _platform.DrawNormal(Game1.Textures["platformNormal"]);
        }
    }
}