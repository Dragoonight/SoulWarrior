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
    public class Goblin : Enemy
    {
        private const float targetingRange = 150f;
        private const float attackingRange = 40f;

        bool attacking = false; // TODO: create a way to decide which direction to attack in.

        public Goblin() : base(new Vector2(400f), Targets.Door)
        {
        }

        public void LoadContent(ContentManager content)
        {
            // TODO: Move CollidableObject creation to Enemy constructor
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), SpawnPosition, new Rectangle(0, 0, 100, 100), 0f); 
        }

        public void Update(GameTime gameTime)
        {
            UpdateTargetingAi();
            MovementAi(gameTime);

        }

        private void UpdateTargetingAi()
        {
            // Get distances to Targets
            float distanceToKnight = Vector2.Distance(CollidableObject.Position, InGame.Knight.CollidableObject.Position);
            float distanceToArcher = Vector2.Distance(CollidableObject.Position, InGame.Archer.CollidableObject.Position);
            float distanceToDoor = Vector2.Distance(CollidableObject.Position, new Vector2(960, 250));

            attacking = false;

            // If Door is within attacking range
            if (distanceToDoor < attackingRange)
            {
                SetTarget(Targets.Door);
                attacking = true;
            }

            // If the knight is within 150 units and is not already targeting Archer
            if (distanceToKnight < targetingRange && target != Targets.Archer)
            {
                // Set target to knight
                SetTarget(Targets.Knight);
                // If Knight is within attacking range
                if (distanceToKnight < attackingRange)
                {
                    attacking = true;
                }
                return;
            }

            // If the Archer is within 150 units and is not already targeting Knight
            if (distanceToArcher < targetingRange && target != Targets.Knight)
            {
                // Set target to Archer
                SetTarget(Targets.Archer);
                // If Archer is within attacking range
                if (distanceToArcher < attackingRange)
                {
                    attacking = true;
                }
                return;
            }

            // Else no players are within 150 units
            // Set target to Door
            SetTarget(Targets.Door);
        }

        // Player keys and movement
        private void MovementAi(GameTime gameTime)
        {
            // If attacking then don´t move
            if (attacking) { return;}

            // Reset displacement
            Vector2 displacement = Vector2.Zero;

            // Reset bools
            bool up = false,
                right = false,
                down = false,
                left = false,
                rightUp = false,
                rightDown = false,
                leftDown = false,
                leftUp = false;

            const int margin = 12;

            // If target is within the margin in X-axis
            if (targetPosition.X - margin <= CollidableObject.Position.X &&
                CollidableObject.Position.X <= targetPosition.X + margin)
            {
                // if target is above
                up = targetPosition.Y < CollidableObject.Position.Y;
                // If target is below
                down = CollidableObject.Position.Y < targetPosition.Y;
            }

            // If target is within the margin in Y-axis
            if (targetPosition.Y - margin <= CollidableObject.Position.Y &&
                CollidableObject.Position.Y <= targetPosition.Y + margin)
            {
                // if target is to the left
                left = targetPosition.X < CollidableObject.Position.X;
                // if target is to the right
                right = CollidableObject.Position.X < targetPosition.X;
            }

            rightUp = targetPosition.Y < CollidableObject.Position.Y && CollidableObject.Position.X < targetPosition.X && !up && !right;
            rightDown = CollidableObject.Position.X < targetPosition.X && CollidableObject.Position.Y < targetPosition.Y && !right && !down;
            leftDown = CollidableObject.Position.Y < targetPosition.Y && targetPosition.X < CollidableObject.Position.X && !down && !left;
            leftUp = targetPosition.X < CollidableObject.Position.X && targetPosition.Y < CollidableObject.Position.Y && !left && !up;

            float sqrt2 = (float)Math.Sqrt(2);

            if (up)
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (rightUp)
            {
                displacement.Y -= (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X += (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
            }
            
            if (right)
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (rightDown)
            {
                displacement.X += (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y += (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (down)
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (leftDown)
            {
                displacement.Y += (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X -= (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (left)
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (leftUp)
            {
                displacement.X -= (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y -= (speed / sqrt2) * gameTime.ElapsedGameTime.Milliseconds;
            }

            AddToPosition(displacement);
        }
    }
}
