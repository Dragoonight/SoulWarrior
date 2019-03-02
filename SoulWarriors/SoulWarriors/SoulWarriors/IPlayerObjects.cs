using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    interface IPlayerObjects
    {
        int Health { get; set; }
        int SMeter { get; set; }
    }

   
    class PlayCharacters : IPlayerObjects
    {
        public int Health { get; set; }

        public int SMeter { get; set; }


        public void Draw(SpriteBatch spriteBatch)
        {
        }

    }

    
}
