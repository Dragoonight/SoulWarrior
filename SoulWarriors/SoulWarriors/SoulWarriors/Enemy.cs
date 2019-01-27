using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SoulWarriors
{
    public enum Targets
    {
        Door,
        Archer,
        Knight
    }

    public class Enemy
    {

        public CollidableObject CollidableObject;
        protected readonly Vector2 SpawnPosition;

        /// <summary>
        /// movement speed in pixels per millisecond
        /// </summary>
        public float speed = .1f;

        public Enemy(Vector2 spawnPosition)
        {
            this.SpawnPosition = spawnPosition;

        }


        /// <summary>
        /// adds velocity to position while clamping to PlayArea
        /// </summary>
        protected void AddToPosition(Vector2 valueToAdd)
        {
            CollidableObject.Position = Vector2.Clamp(
                CollidableObject.Position + valueToAdd,
                new Vector2(InGame.PlayArea.Left, InGame.PlayArea.Top),
                new Vector2(InGame.PlayArea.Right, InGame.PlayArea.Bottom));
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CollidableObject.Texture,
                CollidableObject.Position,
                CollidableObject.SourceRectangle,
                Color.White,
                CollidableObject.Rotation,
                CollidableObject.origin,
                1,
                SpriteEffects.None,
                0);

        }

        // Key Rectangles/Areas (Spawn and target)



        // Go directly for the door



        // Mainly attack player (check who is closed to the enemy)


        // Go for door, attack player if near by (Check which circle is colliding first)




    }
}
