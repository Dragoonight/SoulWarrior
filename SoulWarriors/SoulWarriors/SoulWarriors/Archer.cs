﻿using System;
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

        public void Update()
        {
            GetInput();

            InGame.Chain.StartPosition = CollidableObject.Position;
        }

        private void GetInput()
        {          

            if (currentKeyboardState.IsKeyDown(Keys.A) == true && previousKeyboardState != currentKeyboardState)
            {
                CollidableObject.Position.X--;

            }

            if (currentKeyboardState.IsKeyDown(Keys.D) == true && previousKeyboardState != currentKeyboardState)
            {
                CollidableObject.Position.X++;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S) == true && previousKeyboardState != currentKeyboardState)
            {
                CollidableObject.Position.Y++;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W) == true && previousKeyboardState != currentKeyboardState)
            {
                CollidableObject.Position.Y--;
            }

        }
    }
}
