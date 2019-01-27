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
        public Knight()
            : base(new Vector2(100f, 500f), new PlayerControlScheme(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.RightControl, Keys.RightShift, Keys.Enter, Keys.Back), new List<Animation>())
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/KnightSpriteSheet"), SpawnPosition, new Rectangle(0, 0, 100, 100), 0f);

            Animations = new List<Animation>()
            {
                new Animation(AnimationTypes.IdleDown, new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,100,100), 100)
                })
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }

        protected override void Action1()
        {
        }
        protected override void Action2()
        {
        }
        protected override void Action3()
        {
        }
        protected override void Action4()
        {
        }

    }
}
