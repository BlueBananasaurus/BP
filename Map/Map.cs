
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Monogame_GL
{
    public class Map : IMap
    {
        public short[,] BackgroundTileMap { get; set; }
        public short[,] ForegroundTileMap { get; set; }
        public short[,] FunctionTileMap { get; set; }
        public List<LineSegmentF> TileMapLines { get; set; }
        public List<IProjectile> MapProjectiles { get; set; }
        public List<IRectanglePhysics> MapMovables { get; set; }
        public List<Inpc> MapNpcs { get; set; }
        public List<ITriggers> mapTriggers { get; set; }
        public List<IEffect> mapLightings { get; set; }
        public List<IPickable> mapPickables { get; set; }
        public List<IParticle> mapParticles { get; set; }
        public List<IDecoration> mapDecorations { get; set; }
        public List<IDecoration> mapDecorationsLayer2 { get; set; }
        public List<ICrate> mapCrates { get; set; }
        public List<Light> Lights { get; set; }
        public bool DrawFunctionMap { get; set; }
        public bool DrawBordersOfTiles { get; set; }
        public Vector3 LightLevel { get; set; }
        [JsonIgnore]
        public MapTreeHolder MapTree { get; set; }
        [JsonIgnore]
        public MapTreeHolder WaterTree { get; set; }
        public bool ShowBoundary { get; set; }
        public bool ShowGround { get; set; }
        private bool _transparencyArrive;
        private bool _active;

        public RectangleF MapBoundary { get { return new RectangleF(FunctionTileMap.GetLength(0) * Globals.TileSize, FunctionTileMap.GetLength(1) * Globals.TileSize, 0, 0); } }

        private float _transparency;
        private float _time;

        [JsonConstructor]
        public Map()
        {
            BackgroundTileMap = null;
            ForegroundTileMap = null;
            FunctionTileMap = null;
            DrawFunctionMap = false;
            DrawBordersOfTiles = false;
            TileMapLines = new List<LineSegmentF>();
            MapProjectiles = new List<IProjectile>();
            MapMovables = new List<IRectanglePhysics>();
            MapNpcs = new List<Inpc>();
            mapTriggers = new List<ITriggers>();
            mapLightings = new List<IEffect>();
            mapPickables = new List<IPickable>();
            mapParticles = new List<IParticle>();
            mapDecorations = new List<IDecoration>();
            mapDecorationsLayer2 = new List<IDecoration>();
            mapCrates = new List<ICrate>();
            _transparency = 1f;
            _time = 0f;
            LightLevel = new Vector3(0.5f);
            ShowBoundary = false;
            ShowGround = true;
            _transparencyArrive = true;
            _active = true;
        }

        public void ChangeLightLevel(Vector3 light)
        {
            LightLevel = light;
        }

        public void Generate()
        {
            int sizeY = Globals.GlobalRandom.Next(64, 64);
            int sizeX = Globals.GlobalRandom.Next(64, 64);
            FunctionTileMap = new short[sizeX, sizeY];
            BackgroundTileMap = new short[sizeX, sizeY];
            ForegroundTileMap = new short[sizeX, sizeY];

            FunctionTileMap.DefaultFill<short>(0);
            BackgroundTileMap.DefaultFill<short>(0);
            ForegroundTileMap.DefaultFill<short>(0);

            int xSpace;
            short spaceBlock = 1;

            for (int y = 0; y < sizeY; y++)
            {
                if (y % 6 == 0 && y >=6 && y < sizeY-2)
                {
                    xSpace = Globals.GlobalRandom.Next(3, 12);

                    for (int x = 0; x < sizeX; x++)
                    {
                        if (xSpace > 0)
                        {
                            FunctionTileMap[x, y] = spaceBlock;
                            xSpace--;
                        }
                        else
                        {
                            xSpace = Globals.GlobalRandom.Next(3, 12);
                            if (spaceBlock == 1)
                                spaceBlock = 0;
                            else
                                spaceBlock = 1;
                        }

                        if (x%16 == 0 && Globals.GlobalRandom.Next(5)==0 && y < sizeY - 4)
                        {
                            Game1.mapLive.MapNpcs.Add(new Turret(new Vector2(x * Globals.TileSize, y * Globals.TileSize+64), 100));
                        }
                    }
                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                FunctionTileMap[x, sizeY - 1] = 1;
            }

            Array.Copy(FunctionTileMap, BackgroundTileMap, FunctionTileMap.Length);
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

            if (_active == true)
            {
                foreach (ITriggers trgr in mapTriggers)
                {
                    trgr.Update();
                }

                foreach (IRectanglePhysics mvbl in MapMovables)
                {
                    mvbl.Update();
                }

                foreach (Inpc npc in MapNpcs.Reverse<Inpc>())
                {
                    npc.Update(MapNpcs);
                }

                foreach (IEffect lght in mapLightings.Reverse<IEffect>())
                {
                    lght.Update(mapLightings);
                }

                foreach (IProjectile prjctl in MapProjectiles.Reverse<IProjectile>())
                {
                    prjctl.Update(this);
                }

                foreach (IPickable pick in mapPickables.Reverse<IPickable>())
                {
                    pick.Update();
                }

                foreach (IParticle prtcl in mapParticles.Reverse<IParticle>())
                {
                    prtcl.Update();
                }
            }

            if (_transparencyArrive == true)
            {
                if (_transparency > 0)
                {
                    _transparency -= Game1.Delta / 500;
                }
            }
            else
            {
                if (_transparency < 1)
                {
                    _transparency += Game1.Delta / 500;
                }
                else
                {
                    _transparencyArrive = true;
                    _transparency = 1;
                    _active = true;
                    Game1.State = GlobalState.inWorld;
                }
            }
        }

        public void GoOut()
        {
            _transparencyArrive = false;
            _active = false;
        }

        public void DrawLayer2()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera2DGame.GetViewMatrixLayer2());

            foreach (IDecoration dcr in mapDecorationsLayer2)
            {
                dcr.Draw();
            }

            Game1.SpriteBatchGlobal.End();
        }

        public void DrawWaterTop()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, transformMatrix: Camera2DGame.GetViewMatrix(), samplerState: SamplerState.PointWrap, blendState: BlendState.NonPremultiplied);

            for (int y = Math.Max((int)(Camera2DGame.PositionPoint.Y / 32), 1); y < Math.Min((int)((Camera2DGame.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, FunctionTileMap.GetLength(1)); y++)
            {
                for (int x = Math.Max((int)(Camera2DGame.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DGame.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, FunctionTileMap.GetLength(0)); x++)
                {
                    if (FunctionTileMap[x, y] == 2 && FunctionTileMap[x, y - 1] != 2 && x % 2 == 0)
                        Game1.SpriteBatchGlobal.Draw(Game1.WaterAnimateLeftTop, new Vector2(x * Globals.TileSize, (y - 1) * Globals.TileSize));
                    if (FunctionTileMap[x, y] == 2 && FunctionTileMap[x, y - 1] != 2 && x % 2 == 1)
                        Game1.SpriteBatchGlobal.Draw(Game1.WaterAnimateRightTop, new Vector2(x * Globals.TileSize, (y - 1) * Globals.TileSize));
                }
            }

            Game1.SpriteBatchGlobal.End();
        }

        public void DrawWaterMask()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, transformMatrix: Camera2DGame.GetViewMatrix(), samplerState: SamplerState.PointWrap);

            for (int y = Math.Max((int)(Camera2DGame.PositionPoint.Y / 32), 1); y < Math.Min((int)((Camera2DGame.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, FunctionTileMap.GetLength(1)); y++)
            {
                for (int x = Math.Max((int)(Camera2DGame.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DGame.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, FunctionTileMap.GetLength(0)); x++)
                {
                    if (FunctionTileMap[x, y] == 2 && FunctionTileMap[x, y-1] == 2)
                        Game1.SpriteBatchGlobal.Draw(Game1.tileMask, new Vector2(x * Globals.TileSize, y * Globals.TileSize));
                    if (FunctionTileMap[x, y] == 2 && FunctionTileMap[x, y - 1] != 2 && x%2==0)
                        Game1.SpriteBatchGlobal.Draw(Game1.WaterAnimateLeft, new Vector2(x * Globals.TileSize, (y-1) * Globals.TileSize));
                    if (FunctionTileMap[x, y] == 2 && FunctionTileMap[x, y - 1] != 2 && x % 2 == 1)
                        Game1.SpriteBatchGlobal.Draw(Game1.WaterAnimateRight, new Vector2(x * Globals.TileSize, (y - 1) * Globals.TileSize));
                }
            }

            Game1.SpriteBatchGlobal.End();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(transformMatrix: Camera2DGame.GetViewMatrix(), samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            Game1.Game1Self.GraphicsDevice.Clear(Color.CornflowerBlue);

            if (ShowBoundary == true)
            {
                Game1.EffectSlide.Parameters["UV"].SetValue(new Vector2(0, _time));
                Game1.EffectSlide.CurrentTechnique.Passes[0].Apply();
                Game1.SpriteBatchGlobal.Draw(Game1.boundary, position: new Vector2(-64, 0), sourceRectangle: new Rectangle(0, 0, 64, FunctionTileMap.GetLength(1) * Globals.TileSize));
                Game1.SpriteBatchGlobal.Draw(Game1.boundary, position: new Vector2(FunctionTileMap.GetLength(0) * Globals.TileSize, 0), sourceRectangle: new Rectangle(0, 0, 64, FunctionTileMap.GetLength(1) * Globals.TileSize), effects: SpriteEffects.FlipHorizontally);
                Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            }

            if (ShowGround == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.tileTop, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize / 2 + 1920 / 2, Globals.TileSize / 2), position: new Vector2(-1920 / 2, (FunctionTileMap.GetLength(1)-1) * Globals.TileSize), scale: new Vector2(2));
                Game1.SpriteBatchGlobal.Draw(Game1.Tile03, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize / 2 + 1920 / 2, Globals.TileSize * 10), position: new Vector2(-1920 / 2, (FunctionTileMap.GetLength(1)-1) * Globals.TileSize + Globals.TileSize), scale: new Vector2(2));
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                if((dcr as DecorationStatic).IsFront == false)
                dcr.Draw();
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
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 2)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 3)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 4)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 5)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 6)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["TilesSand"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 32, 32, 32));
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

            foreach (ICrate crt in mapCrates)
            {
                crt.Draw();
            }

            foreach (ITriggers trgr in mapTriggers)
            {
                trgr.Draw();
            }

            foreach (IRectanglePhysics mvbl in MapMovables)
            {
                mvbl.Draw();
            }

            foreach (IPickable pick in mapPickables)
            {
                pick.Draw();
            }

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is CarryBase == false)
                    npc.Draw();
            }

            Game1.SpriteBatchGlobal.End();

            Game1.PlayerInstance.Draw();

            Game1.SpriteBatchGlobal.Begin(transformMatrix: Camera2DGame.GetViewMatrix(), samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is CarryBase == true)
                    npc.Draw();
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                if ((dcr as DecorationStatic).IsFront == true)
                    dcr.Draw();
            }

            foreach (IEffect lght in mapLightings)
            {
                lght.Draw();
            }

            foreach (IProjectile prjctl in MapProjectiles)
            {
                prjctl.Draw();
            }

            foreach (IParticle prtcl in mapParticles)
            {
                prtcl.Draw();
            }

            Game1.SpriteBatchGlobal.End();

            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Transition, Vector2.Zero);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.End();
        }

        public void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointWrap, transformMatrix: Camera2DGame.GetViewMatrix());

            if (ShowGround == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["tileTopNormal"], sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize + 1920, Globals.TileSize), position: new Vector2(-1920 / 2, (FunctionTileMap.GetLength(1) - 1) * Globals.TileSize));

                Game1.SpriteBatchGlobal.Draw(Game1.Textures["tileNormal"], sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize + 1920, Globals.TileSize * 20), position: new Vector2(-1920 / 2, (FunctionTileMap.GetLength(1) - 1) * Globals.TileSize + Globals.TileSize));
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                if ((dcr as DecorationStatic).IsFront == false)
                    dcr.DrawNormal();
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
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 2)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(32, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 3)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 4)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 0, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 5)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(0, 32, 32, 32));
                                }
                                if (BackgroundTileMap[x, y] == 6)
                                {
                                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["tilesNormal"], new Vector2(x * Globals.TileSize, y * Globals.TileSize), sourceRectangle: new Rectangle(64, 32, 32, 32));
                                }
                            }
                        }
                    }
                }
            }

            foreach (ICrate crt in mapCrates)
            {
                crt.DrawNormal();
            }

            foreach (ITriggers trgr in mapTriggers)
            {
                trgr.DrawNormal();
            }

            foreach (IRectanglePhysics mvbl in MapMovables)
            {
                mvbl.DrawNormal();
            }

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is CarryBase == false)
                    npc.DrawNormal();
            }

            Game1.SpriteBatchGlobal.End();

            Game1.PlayerInstance.DrawNormal();

            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointWrap, transformMatrix: Camera2DGame.GetViewMatrix());

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is CarryBase == true)
                    npc.DrawNormal();
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                if((dcr as DecorationStatic).IsFront == true)
                dcr.DrawNormal();
            }

            foreach (IProjectile prjctl in MapProjectiles)
            {
                prjctl.DrawNormal();
            }
            Game1.SpriteBatchGlobal.End();

            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointWrap, transformMatrix: Camera2DGame.GetViewMatrix());

            foreach (IParticle prtcl in mapParticles)
            {
                prtcl.DrawNormal();
            }

            Game1.SpriteBatchGlobal.End();
        }

        public void DrawLights()
        {
            Game1.PlayerInstance.DrawLight();

            foreach (Inpc npc in MapNpcs)
            {
                npc.DrawLight();
            }

            foreach (IProjectile prjctl in MapProjectiles)
            {
                prjctl.DrawLight();
            }

            foreach (ITriggers trgr in mapTriggers)
            {
                trgr.DrawLight();
            }

            foreach (IEffect lght in mapLightings)
            {
                lght.DrawLight();
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                dcr.DrawLight();
            }

            foreach (IParticle prtcl in mapParticles)
            {
                prtcl.DrawLight();
            }

            foreach (IPickable pck in mapPickables)
            {
                pck.DrawLight();
            }
        }
    }
}