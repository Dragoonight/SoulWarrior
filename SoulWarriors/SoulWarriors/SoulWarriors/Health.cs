using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    class Health
    {
        //
        public int lifes = 3;

        public Texture2D heart;

        public void LoadContent(ContentManager content)
        {
           heart = content.Load<Texture2D>(@"Textures/Heart");

           heart = content.Load<Texture2D>(@"Textures/Heart");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = lifes; i > 0; i--)
            {
                spriteBatch.Draw(heart, new Vector2
                    (-50 + i * 75, 15), Color.White);
            }

            for (int i = lifes; i > 0; i--)
            {
                spriteBatch.Draw(heart, new Vector2
                    (1600 + i * 75, 15), Color.White);
            }
        }
    }
}
