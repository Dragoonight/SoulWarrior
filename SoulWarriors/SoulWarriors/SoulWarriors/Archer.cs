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
       
        public Archer() : base(new Vector2(500f), new PlayerControlScheme(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space, Keys.LeftAlt, Keys.LeftControl, Keys.LeftShift))
        {
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), SpawnPosition, new Rectangle(0,0,100,100), 0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
