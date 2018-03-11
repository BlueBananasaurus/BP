using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class PlayersItem
    {
        public Module Type { get; set; }
        public byte Amount { get; set; }

        public PlayersItem(Module type,byte amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
