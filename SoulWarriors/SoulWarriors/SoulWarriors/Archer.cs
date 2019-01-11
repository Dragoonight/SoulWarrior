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
    public class Archer : Player
    {       
       
        public Archer() : base(new Vector2(500f))
        {
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), SpawnPosition, new Rectangle(0,0,100,100), 0f);
        }

        public void Update(GameTime gameTime)
        {
            GetInput(gameTime);

            InGame.Chain.StartPosition = CollidableObject.Position;
        }

        // Player keys and movement
        private void GetInput(GameTime gameTime)
        {          

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                CollidableObject.Position.X -= speed * gameTime.ElapsedGameTime.Milliseconds;

            }

            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                CollidableObject.Position.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                CollidableObject.Position.Y += speed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                CollidableObject.Position.Y -= speed * gameTime.ElapsedGameTime.Milliseconds;
            }

        }
    }
}
