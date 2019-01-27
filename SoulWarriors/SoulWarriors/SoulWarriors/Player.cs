using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
        private static KeyboardState currentKeyboardState;
        private static readonly float MaxChainLength = 640f; // TODO: Change MaxChainLength name to a more descriptive one

        protected readonly Vector2 SpawnPosition;
        private readonly PlayerControlScheme _controlScheme;
        public CollidableObject CollidableObject;
        protected List<Animation> Animations;

        /// <summary>
        /// movement speed in pixels per millisecond
        /// </summary>
        private float speed = .2f;

        private AnimationDirections _animationDirection = AnimationDirections.Down;
        private AnimationsStates _animationState = AnimationsStates.Idle;

        private Animation CurrentAnimation
        {
            get
            {
                string identifier = (_animationState.ToString() + _animationDirection.ToString());
                foreach (Animation animation in Animations)
                {
                    if (animation.AnimationType.ToString().Equals(identifier))
                    {
                        return animation;
                    }
                }
                throw new ArgumentException();
            }
        }

        protected Player(Vector2 spawnPosition, PlayerControlScheme controlScheme, List<Animation> animations)
        {
            SpawnPosition = spawnPosition;
            _controlScheme = controlScheme;
            Animations = animations;
        }

        public virtual void Update(GameTime gameTime)
        {
            GetInput(gameTime);
            //UpdateAnimation(gameTime);
        }
        
        private void GetInput(GameTime gameTime)
        {
            GetMovement(gameTime);
            GetActions();
        }

        private void GetMovement(GameTime gameTime)
        {
            // Update keyboard state
            currentKeyboardState = Keyboard.GetState();
            // Reset displacement
            Vector2 displacement = Vector2.Zero;

            // Get input
            if (currentKeyboardState.IsKeyDown(_controlScheme.MoveUp))
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
                _animationDirection = AnimationDirections.Up;
            }

            if (currentKeyboardState.IsKeyDown(_controlScheme.MoveLeft))
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
                _animationDirection = AnimationDirections.Left;
            }

            if (currentKeyboardState.IsKeyDown(_controlScheme.MoveRight))
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
                _animationDirection = AnimationDirections.Right;
            }

            if (currentKeyboardState.IsKeyDown(_controlScheme.MoveDown))
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
                _animationDirection = AnimationDirections.Down;
            }

            // if nothing happened set _animationState to Idle and return.
            if (displacement == Vector2.Zero)
            {
                _animationState = AnimationsStates.Idle;
                return;
            }
            // if something happened set _animationState to Walk
            _animationState = AnimationsStates.Walk;

            // Limit distance from other player
            // If the new distance is greater than MaxChainLength
            if (Vector2.Distance(CollidableObject.Position + displacement, InGame.GetOtherPlayerPosition(CollidableObject.Position)) > MaxChainLength)
            {
                // Don´t add displacement to position
                return; // TODO: make the remaining length used too
            }

            // Add displacement to position
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
            if (currentKeyboardState.IsKeyDown(_controlScheme.Action1))
            {
                Action1();
                // Overwrite the previous animation state (Walk) with Action1
                _animationState = AnimationsStates.Action1;
                return;
            }
            if (currentKeyboardState.IsKeyDown(_controlScheme.Action2))
            {
                Action2();
                _animationState = AnimationsStates.Action2;
                return;
            }
            if (currentKeyboardState.IsKeyDown(_controlScheme.Action3))
            {
                Action3();
                _animationState = AnimationsStates.Action3;
                return;
            }
            if (currentKeyboardState.IsKeyDown(_controlScheme.Action4))
            {
                Action4();
                _animationState = AnimationsStates.Action4;
                return;
            }
            
            _animationState = AnimationsStates.Idle;
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

        private void UpdateAnimation(GameTime gameTime)
        {
            CurrentAnimation.Animate(ref CollidableObject.SourceRectangle, ref CollidableObject.origin, gameTime);

            // Reset all other animations except from the CurrentAnimation
            foreach (Animation animation in Animations)
            {
                if (ReferenceEquals(animation, CurrentAnimation)) { return; }
                animation.Reset();
            }
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
