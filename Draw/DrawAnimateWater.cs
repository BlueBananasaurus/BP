using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public static class DrawAnimateWater
    {
        static public DynamicVertexBuffer VertexBuffer { get; set; }
        static public DynamicIndexBuffer IndexBuffer { get; set; }
        static short[] indices;
        static RasterizerState rasterizerState;
        static VertexPositionTexture[] vertices;
        public static float Wave1 { get; private set; }
        public static float Wave2 { get; private set; }
        public static float Elevation { get; private set; }

        static DrawAnimateWater()
        {
            indices = new short[6];
            IndexBuffer = new DynamicIndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), indices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState = new RasterizerState();
            vertices = new VertexPositionTexture[4];
            VertexBuffer = new DynamicVertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionTexture), vertices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            Wave1 = 0;
            Wave2 = 0;
            Elevation = 0;
        }

        public static void Update()
        {
            Wave1 = (float)Math.Sin(Game1.Time * 4) * 4;
            Wave2 = (float)Math.Sin(Game1.Time * 4) * -4;
            Elevation= (float)Math.Cos(Game1.Time * 4) * 4-8;
        }

        public static void DrawLeft()
        {
            Game1.BscEffect.World = Matrix.Identity;
            Game1.BscEffect.View = Matrix.Identity;
            Game1.BscEffect.Projection = Matrix.CreateOrthographicOffCenter(0, Globals.TileSize, Globals.TileSize * 2, 0, 0, -1);
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = Game1.tileMask;
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = 1f;

            vertices[0] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Wave1+ Elevation, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + Wave2+ Elevation, 0), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize+ Globals.TileSize, 0), new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Globals.TileSize, 0), new Vector2(0, 1));

            VertexBuffer.SetData(vertices);
            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            IndexBuffer.SetData(indices);
            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }

            Effects.ResetEffect3D();
        }

        public static void DrawRight()
        {
            Game1.BscEffect.World = Matrix.Identity;
            Game1.BscEffect.View = Matrix.Identity;
            Game1.BscEffect.Projection = Matrix.CreateOrthographicOffCenter(0, Globals.TileSize, Globals.TileSize * 2, 0, 0, -1);
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = Game1.tileMask;
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = 1f;

            vertices[0] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Wave2+ Elevation, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + Wave1+ Elevation, 0), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + Globals.TileSize, 0), new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Globals.TileSize, 0), new Vector2(0, 1));

            VertexBuffer.SetData(vertices);
            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            IndexBuffer.SetData(indices);
            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }

            Effects.ResetEffect3D();
        }
    }

    public static class DrawAnimateWaterOver
    {
        static public DynamicVertexBuffer VertexBuffer { get; set; }
        static public DynamicIndexBuffer IndexBuffer { get; set; }
        static short[] indices;
        static RasterizerState rasterizerState;
        static VertexPositionTexture[] vertices;
        static float wave1;
        static float wave2;
        static float elevation;

        static DrawAnimateWaterOver()
        {
            indices = new short[6];
            IndexBuffer = new DynamicIndexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(short), indices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState = new RasterizerState();
            vertices = new VertexPositionTexture[4];
            VertexBuffer = new DynamicVertexBuffer(Game1.GraphicsGlobal.GraphicsDevice, typeof(VertexPositionTexture), vertices.GetLength(0), BufferUsage.WriteOnly);
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            wave1 = 0;
            wave2 = 0;
            elevation = 0;
        }

        public static void Update()
        {
            wave1 = DrawAnimateWater.Wave1;
            wave2 = DrawAnimateWater.Wave2;
            elevation = DrawAnimateWater.Elevation;
        }

        public static void DrawLeft()
        {
            Game1.BscEffect.World = Matrix.Identity;
            Game1.BscEffect.View = Matrix.Identity;
            Game1.BscEffect.Projection = Matrix.CreateOrthographicOffCenter(0, Globals.TileSize, Globals.TileSize * 2, 0, 0, -1);
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = Game1.Textures["waterOver"];
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = 1f;

            vertices[0] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + wave1 + elevation, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + wave2+ elevation, 0), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + Globals.TileSize + wave2 + elevation, 0), new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Globals.TileSize + wave1 + elevation, 0), new Vector2(0, 1));

            VertexBuffer.SetData(vertices);
            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            IndexBuffer.SetData(indices);
            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }

            Effects.ResetEffect3D();
        }

        public static void DrawRight()
        {
            Game1.BscEffect.World = Matrix.Identity;
            Game1.BscEffect.View = Matrix.Identity;
            Game1.BscEffect.Projection = Matrix.CreateOrthographicOffCenter(0, Globals.TileSize, Globals.TileSize * 2, 0, 0, -1);
            Game1.BscEffect.TextureEnabled = true;
            Game1.BscEffect.LightingEnabled = false;
            Game1.BscEffect.Texture = Game1.Textures["waterOver"];
            Game1.BscEffect.AmbientLightColor = new Vector3(1f);
            Game1.BscEffect.Alpha = 1f;

            vertices[0] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + wave2 + elevation, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + wave1 + elevation, 0), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(Globals.TileSize, Globals.TileSize + Globals.TileSize + wave1 + elevation, 0), new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(0, Globals.TileSize + Globals.TileSize + wave2 + elevation, 0), new Vector2(0, 1));

            VertexBuffer.SetData(vertices);
            Game1.GraphicsGlobal.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            IndexBuffer.SetData(indices);
            Game1.GraphicsGlobal.GraphicsDevice.Indices = IndexBuffer;

            Game1.GraphicsGlobal.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in Game1.BscEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game1.GraphicsGlobal.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }

            Effects.ResetEffect3D();
        }
    }
}