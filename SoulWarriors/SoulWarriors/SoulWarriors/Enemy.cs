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
    public class Enemy
    {
        //Variables
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Random random = new Random();
       

        //Archers and Knights dinstance from the enemy
        //Something better exist - ask Julius/Alexander
        private float archerDistanceY;
        private float archerDistanceX;
        private float knightDistanceY;
        private float knightDistanceX;

        Archer archer;
        Knight knight;

        //Constructor
        public Enemy(Texture2D enemyTexture)
        {
            texture = enemyTexture;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            //ArcherDistance will get the players position but it will get closer to its position
            archerDistanceX = archer.CollidableObject.Position.X - position.X;
            archerDistanceY = archer.CollidableObject.Position.Y - position.Y;

            knightDistanceX = knight.CollidableObject.Position.X - position.X;
            knightDistanceY = knight.CollidableObject.Position.Y - position.Y;

        }

        // Key Rectangles/Areas (Spawn and target)
       


        // Go directly for the door



        // Mainly attack player (check who is closed to the enemy)
        

        // Go for door, attack player if near by (Check which circle is colliding first)


        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, Color.White);
        }


    }
}
