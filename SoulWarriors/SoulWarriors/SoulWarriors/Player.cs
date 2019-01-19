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

        public CollidableObject CollidableObject;
        protected readonly Vector2 SpawnPosition;
        /// <summary>
        /// movement speed in pixels per millisecond
        /// </summary>
        public float speed = .2f;

        public Player(Vector2 spawnPosition)
        {
            this.SpawnPosition = spawnPosition;

        }

        public static void UpdateKeyboard()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
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
    }
}
