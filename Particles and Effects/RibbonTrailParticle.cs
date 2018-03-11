using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public sealed class RibbonTrailParticle : IParticle
    {
        private Texture2D _tex;
        public DynamicVertexBuffer VertexBuffer { get; set; }
        public DynamicIndexBuffer IndexBuffer { get; set; }
        public int PrimitivesCount { get; set; }
        short[] _indices;
        public RectangleF Boundary { get; set; }
        RasterizerState rasterizerState;
        VertexPositionTexture[] _vertices;
        private float _alpha;

        public RibbonTrailParticle(DynamicVertexBuffer vertex, DynamicIndexBuffer index, short[] indices, VertexPositionTexture[] vertices, Texture2D tex, int primitivesCount)
        {
            _tex = tex;
            IndexBuffer = index;
            rasterizerState = new RasterizerState();
            VertexBuffer = vertex;
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            _alpha = 1;
            PrimitivesCount = primitivesCount;
            _indices = indices;
            _vertices = vertices;
        }

        public void Draw()
        {
            Game1.BscEffect.World = Game1.World;
            Game1.BscEffect.View = Game1.View;
            Game1.BscEffect.Projection = Game1.projection;
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = _tex;
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = _alpha;

            VertexBuffer.SetData(_vertices);
            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);
     
            IndexBuffer.SetData(_indices);
            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            if (PrimitivesCount > 2)
            {
                foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.GetLength(0), 0, PrimitivesCount);
                }
            }

            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {

        }

        public void Update()
        {
            _alpha -= Game1.Delta / 256;

            if (_alpha <= 0) Game1.mapLive.mapParticles.Remove(this);
        }

        public void Push(Vector2 velocity)
        {

        }

        public void DrawNormal()
        {

        }
    }
}