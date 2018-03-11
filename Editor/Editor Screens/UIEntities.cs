using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class UIEntities : IWindow
    {
        private static UIEntities instance;
        public ItemsHolder Entities { get; private set; }
        public Property[] Controls { get; private set; }

        public List<DropDown> Items;

        public PropertyHolder propertiesHold;

        private UIEntities()
        {
            Items = new List<DropDown>();

            Items.Add(new DropDown(typeof(mechFacingCabin), 128 + 64, new Vector2(256)));
            Items.Add(new DropDown(typeof(mechFacingCabin), 128 + 64, new Vector2(256, 256 + 64)));

            propertiesHold = new PropertyHolder();

            Entities = new ItemsHolder(new RectangleF((256 + 16) * 3 + 64, 640 + 121, 1024 - 128 - 16, 128 + 64), 3, 256 + 16, ChangeSelection);

            PropertyHolder properties = new PropertyHolder();
            properties.AddPropertyText("asfdfssf", "dsfasf");
            properties.AddPropertyText("asfdfssf", "dsfasf");
            properties.AddPropertyText("asfdfssf", "dsfasf");
            properties.AddPropertyText("asfdfssf", "dsfasf");
            properties.AddPropertyText("asfdfssf", "dsfasf");
            properties.AddPropertyText("asfdfssf", "dsfasf");

            PropertyHolder properties2 = new PropertyHolder();
            properties2.AddPropertyText("43212", "123213231");
            properties2.AddPropertyText("123231", "45645556");

            for (int x = 0; x < Game1.EditorEntitiesTargets.Count; x++)
            {
                int y = (int)Math.Floor((float)x / Entities.LineHolds);
                if(x<3)
                    Entities.Items.Add(new ListItemEntity(Entities.Items.Count, Entities.Boundary, Game1.EditorEntitiesTargets[x],3, properties));
                else
                    Entities.Items.Add(new ListItemEntity(Entities.Items.Count, Entities.Boundary, Game1.EditorEntitiesTargets[x], 3, properties2));
            }
        }

        public void Reset()
        {
        }

        public void ChangeSelection()
        {
            if (Entities.SelectedIndex.HasValue == true)
                SetProperties((Entities.Items[Entities.SelectedIndex.Value] as ListItemEntity).properties);
            else propertiesHold.Clear();
        }

        public void SetProperties(PropertyHolder property)
        {
            for (int x = 0; x < 16; x++)
            {
                propertiesHold.Controls[x] = property.Controls[x];
            }
        }

        public void Update()
        {
            Entities.Update();

            propertiesHold.Update();
        }

        public void Draw()
        {
            Entities.Draw(Game1.Win);

            propertiesHold.Draw();
        }

        public static UIEntities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIEntities();
                }
                return instance;
            }
        }
    }
}