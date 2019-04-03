using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SoulWarriors
{
    class Health
    {
        public Texture2D container, knightsHearts, archerHearts;
        public Vector2 position;
        public Vector2 position2;
        private int fullLife = 3;
        private int knightCurrentHearts = 3;
        private int archerCurrentHearts = 3;

        public Health(ContentManager content)
        {
            position = new Vector2(100, 100);
            position = new Vector2(200, 200);
            LoadContent(content);
            knightCurrentHearts = fullLife;
            archerCurrentHearts = fullLife;
        }

        public void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>(@"Textures/HeartsContainer");
            knightsHearts = content.Load<Texture2D>(@"Textures/Hearts");
            archerHearts = content.Load<Texture2D>(@"Textures/Hearts");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = fullLife; i > 0; i--)
            {
                spriteBatch.Draw(container, new Vector2
                    (position2.X + i * 140, position2.Y + 15), Color.White);
            }

            for (int i = knightCurrentHearts; i > 0; i--)
            {
                spriteBatch.Draw(knightsHearts, new Vector2
                    (position2.X + i * 140, position2.Y + 15), Color.White);
            }

            for (int i = fullLife; i > 0; i--)
            {
                spriteBatch.Draw(container, new Vector2
                    (position.X + i * 140, position.Y + 15), Color.White);
            }

            for (int i = archerCurrentHearts; i > 0; i--)
            {
                spriteBatch.Draw(archerHearts, new Vector2
                    (position.X + i * 140, position.Y + 17), Color.White);
            }



        }
    }

}

