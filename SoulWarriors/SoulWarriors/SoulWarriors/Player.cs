using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    public class Player
    {
        public CollidableObject CollidableObject;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, InGame.Camera.TransformMatrix);
            spriteBatch.Draw(CollidableObject.Texture, CollidableObject.Position, Color.White);
            spriteBatch.End();
        }
    }
}
