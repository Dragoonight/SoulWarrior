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
    public struct PlayerControlScheme
    {
        public Keys MoveUp { get; set; }
        public Keys MoveDown { get; set; }
        public Keys MoveLeft { get; set; }
        public Keys MoveRight { get; set; }
        public Keys Action1 { get; set; }
        public Keys Action2 { get; set; }
        public Keys Action3 { get; set; }
        public Keys Action4 { get; set; }

        public PlayerControlScheme(Keys moveUp, Keys moveDown, Keys moveLeft, Keys moveRight, Keys action1, Keys action2, Keys action3, Keys action4)
        {
            MoveUp = moveUp;
            MoveDown = moveDown;
            MoveLeft = moveLeft;
            MoveRight = moveRight;
            Action1 = action1;
            Action2 = action2;
            Action3 = action3;
            Action4 = action4;
        }
    }

    public class Player
    {
        public static KeyboardState currentKeyboardState;
        public static readonly float MaxChainLength = 640f; // TODO: Change MaxChainLength name to a more descriptive one

        protected PlayerControlScheme ControlScheme;
        public CollidableObject CollidableObject;
        protected readonly Vector2 SpawnPosition;
        /// <summary>
        /// movement speed in pixels per millisecond
        /// </summary>
        public float speed = .2f;

        public Player(Vector2 spawnPosition, PlayerControlScheme controlScheme)
        {
            this.SpawnPosition = spawnPosition;
            ControlScheme = controlScheme;
        }

        public virtual void Update(GameTime gameTime)
        {
            GetInput(gameTime);
        }
        
        private void GetInput(GameTime gameTime)
        {
            GetMovement(gameTime);
            GetActions();
        }

        private void GetMovement(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            Vector2 displacement = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(ControlScheme.MoveUp))
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(ControlScheme.MoveLeft))
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(ControlScheme.MoveRight))
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(ControlScheme.MoveDown))
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            // If the new distance is greater than MaxChainLength
            if (Vector2.Distance(CollidableObject.Position + displacement, InGame.GetOtherPlayerPosition(CollidableObject.Position)) > MaxChainLength)
            {
                // Don´t add displacement to position
                return; // TODO: make the remaining length used too
            }

            AddToPosition(displacement);
        }

        /// <summary>
        /// adds velocity to position while clamping to PlayArea
        /// </summary>
        private void AddToPosition(Vector2 valueToAdd)
        {
            CollidableObject.Position = Vector2.Clamp(
                CollidableObject.Position + valueToAdd,
                new Vector2(InGame.PlayArea.Left, InGame.PlayArea.Top),
                new Vector2(InGame.PlayArea.Right, InGame.PlayArea.Bottom));
        }

        private void GetActions()
        {
            if (currentKeyboardState.IsKeyDown(ControlScheme.Action1))
            {
                Action1();
            }
            if (currentKeyboardState.IsKeyDown(ControlScheme.Action2))
            {
                Action2();
            }
            if (currentKeyboardState.IsKeyDown(ControlScheme.Action3))
            {
                Action3();
            }
            if (currentKeyboardState.IsKeyDown(ControlScheme.Action4))
            {
                Action4();
            }

        }

        protected virtual void Action1()
        {
        }

        protected virtual void Action2()
        {
        }

        protected virtual void Action3()
        {
        }

        protected virtual void Action4()
        {
        }

        /// <summary>
        /// Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
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
