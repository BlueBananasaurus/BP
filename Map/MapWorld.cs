using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class MapWorld : IMap
    {
        public short[,] BackgroundTileMap { get; set; }
        public short[,] ForegroundTileMap { get; set; }
        public short[,] FunctionTileMap { get; set; }
        public List<LineSegmentF> TileMapLines { get; set; }
        public List<IRectanglePhysics> MapMovables { get; set; }
        public List<IDecoration> mapDecorations { get; set; }
        public List<IDecoration> mapDecorationsLayer2 { get; set; }
        public bool DrawFunctionMap { get; set; }
        public bool DrawBordersOfTiles { get; set; }
        public Vector3 LightLevel { get; set; }
        public MapTreeHolder MapTree { get; set; }
        public MapTreeHolder WaterTree { get; set; }
        public bool ShowBoundary { get; set; }
        public bool ShowGround { get; set; }
        public List<City> Cities;

        public RectangleF MapBoundary { get { return new RectangleF(Game1.mapLive.FunctionTileMap.GetLength(0) * Globals.TileSize, Game1.mapLive.FunctionTileMap.GetLength(1) * Globals.TileSize, 0, 0); } }

        private float _transparency;
        private float _time;

        public MapWorld()
        {
            Cities = new List<City>();
            DrawFunctionMap = false;
            DrawBordersOfTiles = false;
            TileMapLines = new List<LineSegmentF>();
            MapMovables = new List<IRectanglePhysics>();
            mapDecorations = new List<IDecoration>();
            mapDecorationsLayer2 = new List<IDecoration>();
            _transparency = 1f;
            _time = 0f;
            LightLevel = new Vector3(0f);
            ShowBoundary = false;
            ShowGround = true;
        }

        public void ChangeLightLevel(Vector3 light)
        {
            LightLevel = light;
        }

        public void GenerateMapAndWater()
        {
            MapTree = new MapTreeHolder((int)MapBoundary.Size.X, (int)MapBoundary.Size.Y);
            WaterTree = new MapTreeHolder((int)MapBoundary.Size.X, (int)MapBoundary.Size.Y);

            for (int y = 0; y < FunctionTileMap.GetLength(1); y++)
            {
                for (int x = 0; x < FunctionTileMap.GetLength(0); x++)
                {
                    if (FunctionTileMap[x, y] == 1)
                        MapTree.AddRectangle32(new Vector2(x, y) * Globals.TileSize);
                    if (FunctionTileMap[x, y] == 2)
                        WaterTree.AddRectangle32(new Vector2(x, y) * Globals.TileSize);
                }
            }
        }

        public void AddTileMap(short[,] background = null, short[,] foreground = null, short[,] function = null)
        {
            if (background != null)
            {
                BackgroundTileMap = background;
            }
            if (foreground != null)
            {
                ForegroundTileMap = foreground;
            }
            if (function != null)
            {
                FunctionTileMap = function;
            }
        }

        public void Update()
        {
            _time = Game1.Time;

            foreach (IRectanglePhysics mvbl in MapMovables)
            {
                mvbl.Update();
            }

            if (_transparency > 0)
            {
                _transparency -= Game1.Delta / 500;
            }
        }

        public void DrawLayer2()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera2DWorld.GetViewMatrixLayer2());

            foreach (IDecoration dcr in mapDecorationsLayer2)
            {
                dcr.Draw();
            }

            Game1.SpriteBatchGlobal.End();
        }

        public void DrawWaterMask()
        {
            if (FunctionTileMap != null)
            {
                Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, transformMatrix: Camera2DWorld.GetViewMatrix(), samplerState: SamplerState.PointWrap);

                Game1.SpriteBatchGlobal.Draw(Game1.tileMask, sourceRectangle: new Rectangle(0, 0, 1920 / 2, FunctionTileMap.GetLength(1) * Globals.TileSize + 1080), position: new Vector2(-1920 / 2, -1080 / 2));
                Game1.SpriteBatchGlobal.Draw(Game1.tileMask, sourceRectangle: new Rectangle(0, 0, 1920/2, FunctionTileMap.GetLength(1) * Globals.TileSize + 1080), position: new Vector2(FunctionTileMap.GetLength(0) * Globals.TileSize, -1080 / 2));

                Game1.SpriteBatchGlobal.Draw(Game1.tileMask, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize, 1080 / 2), position: new Vector2(0, -1080 / 2));
                Game1.SpriteBatchGlobal.Draw(Game1.tileMask, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize, 1080 / 2), position: new Vector2(0, FunctionTileMap.GetLength(1) * Globals.TileSize));

                for (int y = Math.Max((int)(Camera2DWorld.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DWorld.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, FunctionTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DWorld.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DWorld.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, FunctionTileMap.GetLength(0)); x++)
                    {
                        if (FunctionTileMap[x, y] == 2)
                            Game1.SpriteBatchGlobal.Draw(Game1.tileMask, new Vector2(x * Globals.TileSize, y * Globals.TileSize));
                        if (FunctionTileMap[x, y] == 3)
                            Game1.SpriteBatchGlobal.Draw(Game1.tileMask, new Vector2(x * Globals.TileSize, y * Globals.TileSize + Globals.TileSize/2),sourceRectangle: new Rectangle(0,0,32,16));
                    }
                }
                Game1.SpriteBatchGlobal.End();
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(transformMatrix: Camera2DWorld.GetViewMatrix(), samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            if (ShowBoundary == true)
            {
                Game1.EffectSlide.Parameters["UV"].SetValue(new Vector2(0, _time));
                Game1.EffectSlide.CurrentTechnique.Passes[0].Apply();
                Game1.SpriteBatchGlobal.Draw(Game1.boundary, position: new Vector2(-64, 0), sourceRectangle: new Rectangle(0, 0, 32, FunctionTileMap.GetLength(1) * Globals.TileSize / 2), scale: new Vector2(2));
                Game1.SpriteBatchGlobal.Draw(Game1.boundary, position: new Vector2(FunctionTileMap.GetLength(0) * Globals.TileSize, 0), sourceRectangle: new Rectangle(0, 0, 32, FunctionTileMap.GetLength(1) * Globals.TileSize / 2), scale: new Vector2(2), effects: SpriteEffects.FlipHorizontally);
                Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            }

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["SandCover"], sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize + 1920, FunctionTileMap.GetLength(1) * Globals.TileSize + 1080), position: new Vector2(-1920/2,-1080/2));

            foreach (IDecoration dcr in mapDecorations)
            {
                dcr.Draw();
            }

            foreach (City city in Cities)
            {
                city.Draw();
            }

            if (BackgroundTileMap != null)
            {
                for (int y = Math.Max((int)(Camera2DGame.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DGame.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, BackgroundTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DGame.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DGame.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, BackgroundTileMap.GetLength(0)); x++)
                    {
                        if (Camera2DGame.Position.Y - Globals.TileSize < y * Globals.TileSize && y * Globals.TileSize < Camera2DGame.Position.Y + Globals.WinRenderSize.Y + Globals.TileSize)
                        {
                            if (Camera2DGame.Position.X - Globals.TileSize < x * Globals.TileSize && x * Globals.TileSize < Camera2DGame.Position.X + Globals.WinRenderSize.X + Globals.TileSize)
                            {
                                if (BackgroundTileMap[x, y] == 1)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 2)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 3)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 4)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(96, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 5)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(128, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 6)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(160, 0, 32, 32));
                                }

                                if (BackgroundTileMap[x, y] == 7)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 8)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 9)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 10)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(96, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 11)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(128, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 11)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(160, 32, 32, 32));
                                }

                                if (BackgroundTileMap[x, y] == 12)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 64, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 13)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 64, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 14)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 64, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 15)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(96, 64, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 16)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(128, 64, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 17)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(160, 64, 32, 32));
                                }

                                if (BackgroundTileMap[x, y] == 18)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 96, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 19)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 96, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 20)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 96, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 21)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(96, 96, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 22)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(128, 96, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 23)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesWorld"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(160, 96, 32, 32));
                                }
                            }
                        }
                    }
                }
            }

            if (ForegroundTileMap != null)
            {
                for (int y = Math.Max((int)(Camera2DGame.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DGame.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, ForegroundTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DGame.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DGame.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, ForegroundTileMap.GetLength(0)); x++)
                    {
                    }
                }
            }

            if (DrawFunctionMap == true)
            {
                for (int y = Math.Max((int)(Camera2DGame.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DGame.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, FunctionTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DGame.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DGame.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, FunctionTileMap.GetLength(0)); x++)
                    {
                        if (Camera2DGame.Position.Y - Globals.TileSize < y * Globals.TileSize && y * Globals.TileSize < Camera2DGame.Position.Y + Globals.WinRenderSize.Y + Globals.TileSize)
                        {
                            if (Camera2DGame.Position.X - Globals.TileSize < x * Globals.TileSize && x * Globals.TileSize < Camera2DGame.Position.X + Globals.WinRenderSize.X + Globals.TileSize)
                            {
                                if (FunctionTileMap[x, y] == 1)
                                    Game1.SpriteBatchGlobal.Draw(Game1.tileBack1, new Vector2(x * Globals.TileSize, y * Globals.TileSize), scale: new Vector2(2));
                                if (FunctionTileMap[x, y] == 2)
                                    Game1.SpriteBatchGlobal.Draw(Game1.waterTex, new Vector2(x * Globals.TileSize, y * Globals.TileSize), scale: new Vector2(2));
                            }
                        }
                    }
                }
            }

            Game1.SpriteBatchGlobal.End();

            Game1.PlayerWorldInstance.Draw();

            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Transition, Vector2.Zero);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.End();
        }

        public void DrawNormal()
        {

        }

        public void DrawLights()
        {
            Game1.PlayerInstance.DrawLight();

            foreach (IDecoration dcr in mapDecorations)
            {
                dcr.DrawLight();
            }
        }
    }
}