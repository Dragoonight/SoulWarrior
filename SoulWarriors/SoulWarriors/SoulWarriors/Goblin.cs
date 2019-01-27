using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public class Goblin :Enemy
    {
        // Goblin has default target of door
        private Targets target = Targets.Door;

        Vector2 targetPosition = new Vector2(960, 250);

        public Goblin() : base(new Vector2(400f))
        {
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), SpawnPosition, new Rectangle(0, 0, 100, 100), 0f);
        }

        public void Update(GameTime gameTime)
        {
            UpdateTargetingAi();
            MovementAi(gameTime);

        }

        private void UpdateTargetingAi()
        {
            // Get distances to players
            float distanceToKnight = Vector2.Distance(CollidableObject.Position, InGame.Knight.CollidableObject.Position);
            float distanceToArcher = Vector2.Distance(CollidableObject.Position, InGame.Archer.CollidableObject.Position);

            // If the knight is within 150 units and is not already targeting Archer
            if (distanceToKnight < 150f && target != Targets.Archer)
            {
                target = Targets.Knight;
                targetPosition = InGame.Knight.CollidableObject.Position;
                return;
            }

            // If the Archer is within 150 units and is not already targeting Knight
            if (distanceToArcher < 150f && target != Targets.Knight)
            {
                target = Targets.Archer;
                targetPosition = InGame.Archer.CollidableObject.Position;
                return;
            }

            // Else no players are within 150 units
            // Set target to Door
            target = Targets.Door;
            targetPosition = new Vector2(960, 250);
        }

        // Player keys and movement
        private void MovementAi(GameTime gameTime)
        {
            // Reset displacement
            Vector2 displacement = Vector2.Zero;

            bool up = false,
                right = false,
                down = false,
                left = false,
                rightUp = false,
                rightDown = false,
                leftDown = false,
                leftUp = false;

            // If target is within 50 units
            if (targetPosition.X - 50 <= CollidableObject.Position.X &&
                CollidableObject.Position.X <= targetPosition.X + 50)
            {
                up = targetPosition.Y < CollidableObject.Position.Y;
                down = CollidableObject.Position.Y < targetPosition.Y;
            }

            if (targetPosition.Y - 50 <= CollidableObject.Position.Y &&
                CollidableObject.Position.Y <= targetPosition.Y + 50)
            {
                left = targetPosition.X < CollidableObject.Position.X;
                right = CollidableObject.Position.X < targetPosition.X;
            }

            rightUp = targetPosition.Y < CollidableObject.Position.Y && CollidableObject.Position.X < targetPosition.X && !up && !right;
            rightDown = CollidableObject.Position.X < targetPosition.X && CollidableObject.Position.Y < targetPosition.Y && !right && !down;
            leftDown = CollidableObject.Position.Y < targetPosition.Y && targetPosition.X < CollidableObject.Position.X && !down && !left;
            leftUp = targetPosition.X < CollidableObject.Position.X && targetPosition.Y < CollidableObject.Position.Y && !left && !up;

            if (up)
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (rightUp)
            {
                displacement.Y -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }
            
            if (right)
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (rightDown)
            {
                displacement.X += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (down)
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (leftDown)
            {
                displacement.Y += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (left)
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (leftUp)
            {
                displacement.X -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }

            AddToPosition(displacement);
        }
    }
}
