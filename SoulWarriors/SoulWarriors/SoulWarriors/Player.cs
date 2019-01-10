using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public class Player
    {
        public static KeyboardState currentKeyboardState;
        public static KeyboardState previousKeyboardState;
        //The speed measures how many pixel per second
        //Change speed 
        public float speed = 200.0f;
        
        public GameTime gameTime;


        public CollidableObject CollidableObject;
        protected readonly Vector2 SpawnPosition;

        public Player(Vector2 spawnPosition)
        {
            this.SpawnPosition = spawnPosition;

        }

        public static void UpdateKeyboard()
        {
            currentKeyboardState = Keyboard.GetState();
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
    }
}
