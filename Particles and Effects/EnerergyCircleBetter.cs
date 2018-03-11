using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class EnerergyCircle : IEffect
    {
        private CircleF _circle;
        private List<Vector2> _nodes;
        private byte _count;
        private List<Vector2> _velocities;
        private Vector2 _velocity;
        private float _transparency;
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;

        public EnerergyCircle(CircleF circle, byte count, Vector2 velocity)
        {
            _circle = circle;
            _nodes = new List<Vector2>();
            _velocities = new List<Vector2>();
            _velocity = velocity;
            _transparency = 0f;
            Generate(count);
        }

        public void Generate(byte count)
        {
            _count = count;
            _nodes.Clear();
            float angleBetween;

            angleBetween = (float)(Math.PI * 2) / count;

            for (int i = 0; i < count; i++)
            {
                Vector2 _velocity = new Vector2(((float)Globals.GlobalRandom.NextDouble() * 2f),0);
                _velocities.Add(CompareF.RotateVector2(_velocity, angleBetween * i));
            }

            _nodes = _circle.DidvideCircle(count);
            _nodes.AddRange(_circle.DidvideCircle(count));
            CircleF _n = new CircleF(_circle.Center,_circle.Radius-6);
            _nodes.AddRange(_n.DidvideCircle(count));
            _nodes.AddRange(_n.DidvideCircle(count));
        }

        public void Update(List<IEffect> effects)
        {
            _transparency += Game1.Delta / 128;

            for (int i = 0; i < _velocities.Count; i++)
            {
                _nodes[i] += _velocities[i] + _velocity * Game1.Delta;
                _nodes[i + 1 * _count] += _velocities[i] + _velocity * Game1.Delta;
                _nodes[i + 2 * _count] += _velocities[i] + _velocity * Game1.Delta;
                _nodes[i + 3 * _count] += _velocities[i] + _velocity * Game1.Delta;
            }

            if (_transparency > Math.PI)
            {
                effects.Remove(this);
            }
        }

        public void Draw()
        {
            Game1.BscEffect.World = Game1.World;
            Game1.BscEffect.View = Game1.View;
            Game1.BscEffect.Projection = Game1.projection;
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = Game1.Textures["LightningBig"];
            //Game1.BscEffect.Texture = Game1.podBase;
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = (float)Math.Sin(_transparency);

            Game1.GraphicsGlobal.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            float much = 1f;

            short[] indices = new short[_count * 6];
            VertexPositionTexture[] vertices = new VertexPositionTexture[_nodes.Count];

            for(int i = 0; i <= vertices.GetLength(0)/6+_count/3; i++)
            {
                if (i < (vertices.GetLength(0) / 6 + _count / 3))
                {
                    vertices[i * 4 + 0] = new VertexPositionTexture(new Vector3(_nodes[i], 0), new Vector2(0 + i / much, 0));
                    vertices[i * 4 + 1] = new VertexPositionTexture(new Vector3(_nodes[i + _count + 1], 0), new Vector2((i + 1) / much, 0));
                    vertices[i * 4 + 2] = new VertexPositionTexture(new Vector3(_nodes[i + _count * 2], 0), new Vector2(0 + i / much, 1));
                    vertices[i * 4 + 3] = new VertexPositionTexture(new Vector3(_nodes[i + _count * 3 + 1], 0), new Vector2((i + 1) / much, 1));
                }
                else
                {
                    vertices[i * 4 + 0] = new VertexPositionTexture(new Vector3(_nodes[_count-1], 0), new Vector2(0 + i / much, 0));
                    vertices[i * 4 + 1] = new VertexPositionTexture(new Vector3(_nodes[_count], 0), new Vector2((i+1) / much, 0));
                    vertices[i * 4 + 2] = new VertexPositionTexture(new Vector3(_nodes[_count * 3-1], 0), new Vector2(0 + i / much, 1));
                    vertices[i * 4 + 3] = new VertexPositionTexture(new Vector3(_nodes[_count * 3], 0), new Vector2((i + 1) / much, 1));
                }
            }

            for (int i = 0; i < _count; i++)
            {//vlevo dobre
                indices[i * 6 + 0] = (short)(i * 4 + 0);
                indices[i * 6 + 1] = (short)(i * 4 + 1);
                indices[i * 6 + 2] = (short)(i * 4 + 2);
                indices[i * 6 + 3] = (short)(i * 4 + 2);
                indices[i * 6 + 4] = (short)(i * 4 + 1);
                indices[i * 6 + 5] = (short)(i * 4 + 3);
            }

            IndexBuffer = new IndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), indices.GetLength(0), BufferUsage.WriteOnly);
            IndexBuffer.SetData(indices);

            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            VertexBuffer = new VertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionTexture), vertices.GetLength(0), BufferUsage.WriteOnly);
            VertexBuffer.SetData(vertices);

            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.GetLength(0), 0, _count*2);
            }

            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {
            Draw();
        }
    }
}