using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Diagnostics;
namespace Monogame_GL
{
    public struct PosAndVelocity
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }

        public PosAndVelocity(Vector2 position, Vector2 velocity)
        {
            Velocity = velocity;
            Position = position;
        }
    }

    public sealed class RibbonTrail
    {
        List<PosAndVelocity> nodes;
        private int _number;
        private Timer _time;
        private int _width;
        private Texture2D _tex;
        private Texture2D _normalMap;
        private Texture2D _texLight;
        public DynamicVertexBuffer VertexBuffer { get; set; }
        public DynamicIndexBuffer IndexBuffer { get; set; }
        short[] indices;
        RasterizerState rasterizerState;
        VertexPositionNormalTexture[] vertices;
        private float _alpha;

        public RibbonTrail(Vector2 start, Vector2 velocity, int numberOfnodes, int width, int delay, Texture2D tex, Texture2D normalMap ,Texture2D light = null)
        {
            nodes = new List<PosAndVelocity>();
            nodes.Add(new PosAndVelocity(start, velocity));
            _number = numberOfnodes;
            _time = new Timer(delay);
            _width = width;
            _tex = tex;
            indices = new short[(_number * 6)];
            IndexBuffer = new DynamicIndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), indices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState = new RasterizerState();
            vertices = new VertexPositionNormalTexture[_number * 2];
            VertexBuffer = new DynamicVertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionNormalTexture), vertices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            _alpha = 1;
            _texLight = light;
            _normalMap = normalMap;
        }

        public void Update(Vector2 position, Vector2 velocity, float alpha)
        {
            _time.Update();

            if (_time.Ready == true)
            {
                nodes.Insert(0, new PosAndVelocity(position, velocity));

                if (nodes.Count > _number)
                {
                    nodes.RemoveAt(_number);
                }

                _time.Reset();
            }

            _alpha = alpha;

            float UVOff = 1f / _number;

            for (int i = 0; i < nodes.Count; i++)
            {
                vertices[i * 2] = new VertexPositionNormalTexture(new Vector3(nodes[i].Position.ShiftOverDistance(_width / 2, nodes[i].Velocity.ToAngle() - (float)(Math.PI / 2)), 0), new Vector3(nodes[i].Velocity.X, nodes[i].Velocity.Y, 0), new Vector2(0, i * UVOff));
                vertices[i * 2 + 1] = new VertexPositionNormalTexture(new Vector3(nodes[i].Position.ShiftOverDistance(_width / 2, nodes[i].Velocity.ToAngle() + (float)(Math.PI / 2)), 0), new Vector3(nodes[i].Velocity.X, nodes[i].Velocity.Y, 0), new Vector2(1, i * UVOff));
            }

            VertexBuffer.SetData(vertices);

            int number = 2;

            for (int i = 0; i < (indices.GetLength(0) / 6); i++)
            {
                indices[i * 6 + 0] = (short)(0 + i * number);
                indices[i * 6 + 1] = (short)(1 + i * number);
                indices[i * 6 + 2] = (short)(2 + i * number);
                indices[i * 6 + 3] = (short)(2 + i * number);
                indices[i * 6 + 4] = (short)(1 + i * number);
                indices[i * 6 + 5] = (short)(3 + i * number);
            }

            IndexBuffer.SetData(indices);
        }

        public void Draw()
        {
            if (_tex != null)
            {
                Game1.BscEffect.World = Game1.World;
                Game1.BscEffect.View = Game1.View;
                Game1.BscEffect.Projection = Game1.projection;
                Game1.BscEffect.Alpha = _alpha;
                Game1.BscEffect.Texture = _tex;
                Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);
                Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;
                Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

                if (nodes.Count > 2)
                {
                    foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, nodes.Count / 2, 0, (nodes.Count - 1) * 2);
                    }
                }

                Effects.ResetEffect3D();
            }
        }

        public void DrawNormal()
        {
            if (_normalMap != null)
            {
                Game1.EffectVertexNormal.Parameters["World"].SetValue(Game1.World);
                Game1.EffectVertexNormal.Parameters["View"].SetValue(Game1.View);
                Game1.EffectVertexNormal.Parameters["Projection"].SetValue(Game1.projection);
                if (Game1.EffectVertexNormal.Parameters["NormalMap"] != null)
                    Game1.EffectVertexNormal.Parameters["NormalMap"].SetValue(_normalMap);


                Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);
                Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;
                Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

                Game1.EffectVertexNormal.CurrentTechnique.Passes[0].Apply();
                if (nodes.Count > 2)
                {
                    Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, nodes.Count / 2, 0, (nodes.Count - 1) * 2);
                }


                Effects.ResetEffect3D();
            }
        }

        public void DrawLight()
        {
            if (_texLight != null)
            {
                Game1.BscEffect.World = Game1.World;
                Game1.BscEffect.View = Game1.View;
                Game1.BscEffect.Projection = Game1.projection;
                Game1.BscEffect.Alpha = _alpha;
                Game1.BscEffect.Texture = _texLight;
                Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);
                Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;
                Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

                if (nodes.Count > 2)
                {
                    foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, nodes.Count / 2, 0, (nodes.Count - 1) * 2);
                    }
                }

                Effects.ResetEffect3D();
            }
        }

        public void LeftParticle()
        {
            //Game1.mapLive.mapParticles.Add(new RibbonTrailParticle(VertexBuffer, IndexBuffer, indices, vertices, _tex, (nodes.Count - 2) * 2));
        }
    }
}