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
    class Enemy
    {
        //Variables
        private Texture2D enemyTestTexture;
        private int speed;
        private Vector2 enemyPosition;
        private Rectangle rectTarget;
        private Rectangle rectSpawn;


        // Key Rectangles/Areas (Spawn and target)



        // Go directly for the door



        // Mainly attack player (check who is closed to the enemy)



        // Go for door, attack player if near by (Check which circle is colliding first)


        // LoadContent (temporary)
        public void LoadContent(ContentManager content)
        {
            enemyTestTexture = content.Load<Texture2D>("Insert name here");
        }

        // Update
        public void Update(GameTime gameTime)
        {

        }


        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw();
            spriteBatch.End();
        }


    }
}
