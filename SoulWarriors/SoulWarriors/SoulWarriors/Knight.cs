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
    public class Knight : Player
    {
        public Knight() : base(new Vector2(100f, 500f))
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/KnightSpriteSheet"), SpawnPosition, new Rectangle(0, 0, 100, 100), 0f);
        }

        // Player Update
        public void Update(GameTime gameTime)
        {
            GetInput(gameTime);
        }

        // Player keys and movement
        private void GetInput(GameTime gameTime)
        {
            Vector2 displacement = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                displacement.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                displacement.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                displacement.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                displacement.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }


            AddToPosition(displacement);
        }


    }
}
