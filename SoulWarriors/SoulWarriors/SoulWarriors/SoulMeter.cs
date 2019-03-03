using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
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
    class SoulMeter
    {
        


        public Texture2D container, soulBar;
        public Vector2 position;
        private int fullSoul;
        private int currentSoul;
        private int rateOfChange = 1;
        private Color barColor;
        private float timer = 0;
        private float defaultTimer = 0;
        private float slowDrain = 1;
        private bool keyboardState = true;

        public SoulMeter(ContentManager content)
        {
            position = new Vector2(100, 100);
            LoadContent(content);
            fullSoul = soulBar.Width;
            currentSoul = fullSoul;
        }

        public void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>(@"Container");
            soulBar = content.Load<Texture2D>(@"Gauge");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && keyboardState == true)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                slowDrain += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0)
                {
                    currentSoul -= rateOfChange * 10^(int)slowDrain;
                    timer = defaultTimer;

                    if (currentSoul <= 700)
                    {
                        //PlayerLife comes
                        keyboardState = false;
                    }

                    //if player life gets one
                    //{
                    //keyboardState = true;
                    //}


                }


            }
                      
            SoulColor();
        }

        public void SoulColor()
        {

            if (currentSoul >= soulBar.Width * 0.75)
            {
                barColor = Color.White;
            }
            else if (currentSoul <= soulBar.Width * 0.25)
            {
                barColor = Color.Gray;
            }               
            else
            {
                barColor = Color.DarkGray;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {         
            spriteBatch.Draw(soulBar, new Rectangle((int)position.X + 3, (int)position.Y, currentSoul, soulBar.Height), barColor);
            spriteBatch.Draw(container, position, Color.White);       
        }
    }
}
