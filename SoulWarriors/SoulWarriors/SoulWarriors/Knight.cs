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
        public List<Projectile> swoosh = new List<Projectile>();
        private Texture2D _swooshTexture;

        public Knight(Texture2D texture, Texture2D swooshTexture, List<Animation> animations)
            : base(texture, new Vector2(100f, 500f), new PlayerControlScheme(Keys.Up, Keys.Down, Keys.Left, Keys.Right, 
                Keys.Space, Keys.RightShift, Keys.M, Keys.Back), animations)
        {
            _swooshTexture = swooshTexture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateSwoosh(gameTime);

        }

        private void UpdateSwoosh(GameTime gameTime)
        {
            List<int> swooshToBeRemoved = new List<int>();
            for (int i = 0; i < swoosh.Count; i++)
            {
                // Update swoosh
                swoosh[i].Update(gameTime);
                // If swoosh is no longer active add its index number to the swooshToBeRemoved list
                if (swoosh[i].Active == false)
                {
                    swooshToBeRemoved.Add(i);
                }
            }
            // Sort swoosh swooshToBeRemoved in ascending order
            swooshToBeRemoved.Sort();
            // Reverse swooshToBeRemoved to descending order
            swooshToBeRemoved.Reverse();
            // Try to remove swoosh
            try
            {
                // Remove swoosh
                for (int index = 0; index < swooshToBeRemoved.Count; index++)
                {
                    swoosh.RemoveAt(swooshToBeRemoved[index]);
                }
            }
            finally
            {
                // Always clear swooshToBeRemoved
                swooshToBeRemoved.Clear();
            }

        }

        /// <summary>
        /// Fire Arrow
        /// </summary>
        protected override void Action1()
        {
            swoosh.Add(new Projectile(_swooshTexture, CollidableObject.Position, InGame.MousePos, 0.5f, InGame.PlayArea));
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Projectile swoosh in swoosh)
            {
                swoosh.Draw(spriteBatch);
            }
        }
    }
}
