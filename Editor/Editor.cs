using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Monogame_GL
{
    public static class Editor
    {
        public static MapEdit EditMap;
        public static string SelectedEditTile = "";
        public static LayerEditing layerEdit;

        static Editor()
        {
            EditMap = new MapEdit();
            layerEdit = LayerEditing.noChoosed;
            SetMapSize(new Point(32, 32));
            ResetLayers();
        }

        public static void Update()
        {
            if (Game1.EditorMenuOpen == true) EditorMenu.Update();
            else
            {
                if (MouseInput.MouseStateOld.LeftButton == ButtonState.Pressed)
                {
                    if (UIDrawing.Instance.Tiles.SelectedIndex != null || UIDrawing.Instance.TilesPhysics.SelectedIndex != null)
                        DrawOnBackground(SelectedEditTile);
                }
                if (MouseInput.MouseStateNew.MiddleButton == ButtonState.Pressed)
                {
                    Camera2DEditor.MoveBy(MouseInput.MouseStateNew.Position.ToVector2() - MouseInput.MouseStateOld.Position.ToVector2());
                }
                if (MouseInput.MouseStateOld.RightButton == ButtonState.Pressed)
                {
                    DrawOnBackground("_blank_");
                }
                if (MouseInput.MouseClickedLeft() == true)
                {
                    if (UIEntities.Instance.Entities.SelectedIndex != null)
                    {
                        if (UIEntities.Instance.Entities.SelectedIndex == 0)
                            AddPlayer(new Vector2(MouseInput.MousePositionRealGrid().X, MouseInput.MousePositionRealGrid().Y - (62 * 2)));
                        if (UIEntities.Instance.Entities.SelectedIndex == 1)
                            AddEntity(new Vector2(MouseInput.MousePositionRealGrid().X, MouseInput.MousePositionRealGrid().Y - (194)));
                    }
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyUp(Keys.Tab) && KeyboardInput.KeyboardStateOld.IsKeyDown(Keys.Tab)) Game1.EditorMenuOpen = !Game1.EditorMenuOpen;
        }

        public static void Draw()
        {
            EditorMenu.Draw();
        }

        public static void DrawEntityCursor()
        {
            if (CompareF.RectangleVsVector2(new RectangleF(Editor.EditMap.MapBoundary.Size * Globals.ScreenRatio.X, Editor.EditMap.MapBoundary.Position), MouseInput.MouseStateNew.Position.ToVector2() + Camera2DEditor.PositionPoint * Globals.ScreenRatio.X) == true)
            {
                if (EditorMenu.Radios.IndexSelected != 2)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["CursorGrid"], MouseInput.MousePositionGridBetter(), scale: new Vector2(1));
                }
            }
            if (EditorMenu.Radios.IndexSelected == 2)
            {
                DrawMouseEntity();
            }
        }

        public static void DrawMouseEntity()
        {
            if (UIEntities.Instance.Entities.SelectedIndex == 0)
            {
                GameDraw.DrawEntity(AllEntitiesEditor.player,BuddyModule.PlayerBoundarySize, new Vector2(MouseInput.MousePositionGridhalfBetter().X, MouseInput.MousePositionGridhalfBetter().Y - (62 * 2)));
            }
            if (UIEntities.Instance.Entities.SelectedIndex == 1)
            {
                GameDraw.DrawEntity(AllEntitiesEditor.mech, MechModule.MechBoundarySize, new Vector2(MouseInput.MousePositionGridhalfBetter().X, MouseInput.MousePositionGridhalfBetter().Y - (97 * 2)));
            }
        }

        public static void DrawForeground()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera2DEditor.GetViewMatrixLayer2());

            foreach (IDecoration dcr in EditMap.mapDecorationsForeground)
            {
                dcr.Draw();
            }

            Game1.SpriteBatchGlobal.End();
        }

        public static void AddPlayer(Vector2 position)
        {
            if (EditorMenu.Radios.IndexSelected == 2)
            {
                MapEdit.Player = new PlayerEditor(position);
            }
        }

        public static void AddEntity(Vector2 position)
        {
            EditMap.mapEntities.Add(new MechEditor(position, (mechFacingCabin)Enum.GetValues(UIEntities.Instance.Items[0].enumeration).GetValue(UIEntities.Instance.Items[0].SelectedIndex)));
        }

        public static void DrawOnBackground(string tileName)
        {
            if (EditorMenu.Radios.IndexSelected == 1)
            {
                if (MouseInput.MousePositionGridIndex().X >= 0 && MouseInput.MousePositionGridIndex().X < EditMap.MapSize.X && MouseInput.MousePositionGridIndex().Y >= 0 && MouseInput.MousePositionGridIndex().Y < EditMap.MapSize.Y)
                {
                    if (layerEdit == LayerEditing.background)
                        EditMap.BackgroundTileMap[MouseInput.MousePositionGridIndex().X, MouseInput.MousePositionGridIndex().Y] = tileName;
                    if (layerEdit == LayerEditing.foreground)
                        EditMap.ForegroundTileMap[MouseInput.MousePositionGridIndex().X, MouseInput.MousePositionGridIndex().Y] = tileName;
                    if (layerEdit == LayerEditing.physics)
                        EditMap.FunctionTileMap[MouseInput.MousePositionGridIndex().X, MouseInput.MousePositionGridIndex().Y] = tileName;
                }
            }
        }

        public static void SetMapSize(Point size)
        {
            EditMap.MapSize = size;
        }

        public static void ResetLayers()
        {
            EditMap.BackgroundTileMap = new string[EditMap.MapSize.X, EditMap.MapSize.Y].DefaultFill("_blank_");
            EditMap.ForegroundTileMap = new string[EditMap.MapSize.X, EditMap.MapSize.Y].DefaultFill("_blank_");
            EditMap.FunctionTileMap = new string[EditMap.MapSize.X, EditMap.MapSize.Y].DefaultFill("_blank_");
        }
    }
}