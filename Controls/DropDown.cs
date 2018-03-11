using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Monogame_GL
{
    public class DropDown : IControl
    {
        private RectangleF _boundary;
        public bool IsActive { get; private set; }
        public delegate void OnChange();
        public OnChange _changeMehod;
        public Type enumeration;
        public Button btnDrop;
        public List<Button> Selections;
        public bool Droped;
        public byte SelectedIndex;
        string[] names;
        public RectangleF Clickable;
        private int _width;
        private Vector2 __position;

        public DropDown(Type e,int width, Vector2 position, OnChange change = null)
        {
            Selections = new List<Button>();
            IsActive = false;
            _changeMehod = change;
            enumeration = e;
            _boundary = new RectangleF(width, 32, position.X+32, position.Y);
            btnDrop = new Button(position, "",Drop,type: ButtonType.drop,width: width+32);
            Droped = false;
            int i = 0;
            SelectedIndex = 0;
            _width = width;
            __position = position;
            foreach (Enum en in enumeration.GetEnumValues())
            {
                Selections.Add(new Button(new Vector2(position.X, position.Y + i * 32+32),(byte)i,en.ToString(),Select));
                    i++;
            }

            names = Enum.GetNames(enumeration);
        }

        public void Drop()
        {
            Droped = !Droped;
        }

        public bool InClickable()
        {
            if(Droped == false)
                Clickable = new RectangleF(_width, 32, __position.X, __position.Y);
            else
                Clickable = new RectangleF(_width, names.GetLength(0)*32 + 32, __position.X, __position.Y);

            if (CompareF.RectangleVsVector2(Clickable, MouseInput.MouseRealPosMenu()) == true)
            {
                return true;
            }

            return false;
        }

        public void Select(byte index)
        {
            SelectedIndex = index;
            Droped = false;
        }

        public bool Update()
        {
            bool returnClicked = false;

            _changeMehod?.Invoke();
            if (btnDrop.Update() == true)
                returnClicked = true;

            if (Droped)
                foreach (Button btn in Selections)
                {
                    if(btn.Update() == true)
                        returnClicked = true;
                }

            return returnClicked;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["TextBoxBack"], destinationRectangle: new Rectangle((int)_boundary.Position.X + 1, (int)_boundary.Position.Y + 1, (int)_boundary.Size.X - 2, (int)_boundary.Size.Y - 2));
            btnDrop.Draw();

            DrawRectangleBoundary.DrawBlue(_boundary.ToRectangle());

            DrawString.DrawText(names[SelectedIndex], _boundary.Position + new Vector2(8, 7), Align.left, Globals.LightBlueText, FontType.small);

            if (Droped)
                foreach (Button btn in Selections)
                {
                    btn.Draw();
                }
        }
    }
}