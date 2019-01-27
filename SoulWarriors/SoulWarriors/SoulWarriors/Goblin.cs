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
        

        public Goblin() : base(new Vector2(400f))
        {
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), SpawnPosition, new Rectangle(0, 0, 100, 100), 0f);
        }

        public void Update(GameTime gameTime)
        {
            MovementAi(gameTime);

        }

        // Player keys and movement
        private void MovementAi(GameTime gameTime)
        {
            Vector2 displacement = Vector2.Zero;

            
            Vector2 targetPosition = new Vector2(960, 250);
            Vector2 currentPosition = InGame.Goblin.CollidableObject.Position;
            
            float distanceArcher = Vector2.Distance(currentPosition, InGame.Archer.CollidableObject.Position);
            float distanceKnight = Vector2.Distance(currentPosition, InGame.Knight.CollidableObject.Position);
            int target = 0;

                if (distanceKnight < 150f && target != 2)
                {
                    targetPosition = InGame.Knight.CollidableObject.Position;
                    target = 1;
                }
            
                if (distanceArcher < 150f && target != 1)
                {
                    targetPosition = InGame.Archer.CollidableObject.Position;
                    target = 2;
            }
                
            if(target == 0)
            {
                targetPosition = new Vector2(960, 250);
            }



            bool up = false,
                right = false,
                down = false,
                left = false,
                ur = false,
                rd = false,
                dl = false,
                lu = false;


            if (targetPosition.X - 50 <= currentPosition.X &&
                currentPosition.X <= targetPosition.X + 50)
            {
                up = targetPosition.Y < currentPosition.Y;
                down = currentPosition.Y < targetPosition.Y;
            }

            if (targetPosition.Y - 50 <= currentPosition.Y &&
                currentPosition.Y <= targetPosition.Y + 50)
            {
                left = targetPosition.X < currentPosition.X;
                right = currentPosition.X < targetPosition.X;
            }

            ur = targetPosition.Y < currentPosition.Y && currentPosition.X < targetPosition.X && !up && !right;
            rd = currentPosition.X < targetPosition.X && currentPosition.Y < targetPosition.Y && !right && !down;
            dl = currentPosition.Y < targetPosition.Y && targetPosition.X < currentPosition.X && !down && !left;
            lu = targetPosition.X < currentPosition.X && targetPosition.Y < currentPosition.Y && !left && !up;

            if (up)
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (ur)
            {
                displacement.Y -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }
            
            if (right)
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (rd)
            {
                displacement.X += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (down)
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (dl)
            {
                displacement.Y += (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.X -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (left)
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (lu)
            {
                displacement.X -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
                displacement.Y -= (speed / (float)Math.Sqrt(2)) * gameTime.ElapsedGameTime.Milliseconds;
            }







            AddToPosition(displacement);
        }




    }
}
