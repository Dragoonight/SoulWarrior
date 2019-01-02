using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    static class HelperClass
    {
        /// <summary>
        /// Draws a texture between two Vector2
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end)
        {
            spriteBatch.Draw(texture, start, null, Color.White,
                (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                new Vector2(0f, (float)texture.Height / 2),
                new Vector2(Vector2.Distance(start, end), texture.Height),
                SpriteEffects.None, 0f);
        }
    }
}
