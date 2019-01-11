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
            InGame.Chain.EndPosition = CollidableObject.Position;
        }
        // Player keys and movement
        private void GetInput(GameTime gameTime)
        { 
          if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                CollidableObject.Position.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                CollidableObject.Position.X -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                CollidableObject.Position.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                CollidableObject.Position.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

        }
    }
}
