using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class GridItem
    {
        public byte[,] ItemBounds;
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; }
        public Point Index { get; set; }
        public Module Type { get; set; }
        public float VelocityPercentage { get; set; }
        public float DispersionPercentage { get; set; }
        public float DamagePercentage { get; set; }

        public GridItem(float rotation, Point index, Module type)
        {
            Rotation = rotation;
            Index = index;
            Type = type;
            VelocityPercentage = 0;
            DispersionPercentage = 0;
            DamagePercentage = 0;

            switch (type)
            {
                default:
                case
                Module.test:
                    Texture = Game1.Textures["Part"];
                    Globals.LoadJson(ref ItemBounds, "part");
                    DamagePercentage = 0.25f;
                    break;
                case
                Module.tier2:
                    Texture = Game1.Textures["Tier2"];
                    Globals.LoadJson(ref ItemBounds, "block");
                    break;
                case
                Module.tier3:
                    Texture = Game1.Textures["Tier3"];
                    Globals.LoadJson(ref ItemBounds, "block");
                    break;
            }
        }
    }
}
