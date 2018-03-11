using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class NodeLighting
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public NodeLighting(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public void Update()
        {
            Position += new Vector2(Velocity.X / 1 * Game1.Delta / 16, Velocity.Y / 1 * Game1.Delta / 16);
        }
    }

    public class Lightning : IEffect
    {
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;

        [JsonIgnore]
        private Texture2D _tex;

        public List<NodeLighting> Nodes { get; set; }
        public NodeLighting Start { get; set; }
        public NodeLighting End { get; set; }
        public byte Width { get; set; }
        public float Transparency { get; set; }
        private float amount;
        private float rotation = 0.1f;
        private float lenght;
        private float _scroll;
        private VertexPositionTexture[] vertices;
        private short[] indices;
        private RasterizerState rasterizerState;
        private Vector3 rotated;

        public Lightning(Vector2 start, Vector2 end, Texture2D tex, byte width, float scroll = 1f, float velocity = 1.2f)
        {
            Nodes = new List<NodeLighting>();
            lenght = new LineSegmentF(start, end).Lenght().Value;
            Start = new NodeLighting(start, Vector2.Zero);
            End = new NodeLighting(end, Vector2.Zero);
            _tex = tex;
            Width = width;
            Transparency = 1f;
            _scroll = scroll;
            vertices = new VertexPositionTexture[4 * 64];
            indices = new short[8 * 64];

            //IndexBuffer = new IndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), (Nodes.Count * 4) + (Nodes.Count) * 4, BufferUsage.WriteOnly);
            IndexBuffer = new IndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), 8 * 64, BufferUsage.WriteOnly);

            //VertexBuffer = new VertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionTexture), Nodes.Count * 4, BufferUsage.WriteOnly);
            VertexBuffer = new VertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionTexture), 4 * 64, BufferUsage.WriteOnly);

            amount = Globals.GlobalRandom.Next(300, 600);

            Nodes.Add(new NodeLighting(Vector2.Zero, Vector2.Zero));

            if (lenght > 32)
            {
                for (int i = 32; i < lenght - 16; i += 32)
                {
                    Vector2 velocityAdd = new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f) * velocity, (float)(Globals.GlobalRandom.NextDouble() - 0.5f) * velocity);
                    Nodes.Add(new NodeLighting(new Vector2(i, 0 + Globals.GlobalRandom.Next(-8, 9)), velocityAdd));
                }
            }

            Nodes.Add(new NodeLighting(Vector2.Zero + new Vector2(lenght, 0), Vector2.Zero));

            rotation = new LineSegmentF(Start.Position, End.Position).ToAngle();
        }

        public void Update(List<IEffect> effects)//v pohodě
        {
            rotation = new LineSegmentF(Start.Position, End.Position).ToAngle();

            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Update();
            }

            Transparency -= Game1.Delta / amount;
            if (Transparency <= 0)
            {
                Game1.mapLive.mapLightings.Remove(this);
            }
        }

        public void Draw()
        {
            Game1.BscEffect.World = Game1.World;
            Game1.BscEffect.View = Game1.View;
            Game1.BscEffect.Projection = Game1.projection;
            Game1.BscEffect.Alpha = Transparency;
            Game1.BscEffect.Texture = _tex;

            Game1.GraphicsGlobal.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            float a = 4;

            for (int i = 0; i < Nodes.Count - 1; i++)
            {
                rotated = new Vector3(Nodes[i].Position.X, 0 + Nodes[i].Position.Y - Width / 2, 0);
                rotated = Vector3.Transform(rotated, Matrix.CreateRotationZ(rotation));
                vertices[i * 4 + 0] = new VertexPositionTexture(rotated + new Vector3(Start.Position, 0), new Vector2((i / a) + (Game1.Time * _scroll), 0));

                rotated = new Vector3(Nodes[i + 1].Position.X, 0 + Nodes[i + 1].Position.Y - Width / 2, 0);
                rotated = Vector3.Transform(rotated, Matrix.CreateRotationZ(rotation));

                vertices[i * 4 + 1] = new VertexPositionTexture(rotated + new Vector3(Start.Position, 0), new Vector2(((i + 1) / a) + (Game1.Time * _scroll), 0));

                rotated = new Vector3(Nodes[i + 1].Position.X, Nodes[i + 1].Position.Y + Width / 2, 0);
                rotated = Vector3.Transform(rotated, Matrix.CreateRotationZ(rotation));

                vertices[i * 4 + 2] = new VertexPositionTexture(rotated + new Vector3(Start.Position, 0), new Vector2(((i + 1) / a) + (Game1.Time * _scroll), 1));

                rotated = new Vector3(Nodes[i].Position.X, Nodes[i].Position.Y + Width / 2, 0);
                rotated = Vector3.Transform(rotated, Matrix.CreateRotationZ(rotation));

                vertices[i * 4 + 3] = new VertexPositionTexture(rotated + new Vector3(Start.Position, 0), new Vector2((i / a) + (Game1.Time * _scroll), 1));
            }

            for (int i = 0; i < (indices.GetLength(0) / 6); i++)
            {
                indices[i * 6 + 0] = (short)(0 + i * 4);
                indices[i * 6 + 1] = (short)(1 + i * 4);
                indices[i * 6 + 2] = (short)(2 + i * 4);
                indices[i * 6 + 3] = (short)(0 + i * 4);
                indices[i * 6 + 4] = (short)(2 + i * 4);
                indices[i * 6 + 5] = (short)(3 + i * 4);
            }

            IndexBuffer.SetData(indices, 0, (Nodes.Count * 4) + (Nodes.Count) * 4);

            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            VertexBuffer.SetData(vertices, 0, Nodes.Count * 4);

            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Nodes.Count * 4, 0, Nodes.Count * 2);
            }

            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {
            Draw();
        }
    }
}