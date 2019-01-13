using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public static class Menu
    {
        private static List<Texture2D> menuList = new List<Texture2D>();
        private static List<Texture2D> buttonList = new List<Texture2D>();
        private static int menuTotal;
        private static int[] bTotal;
        private static int selectedButton;
        private static bool isKeyUp;

        private static KeyboardState currentKeyboard;
        private static KeyboardState previousKeyboard;


        public static void LoadContent(ContentManager content, int menuTotal, int[] buttonsTotal)
        {
            Menu.menuTotal = menuTotal;
            bTotal = buttonsTotal;

            

            for (int m = 0; m < menuTotal; m++)
            {
            menuList.Add(content.Load<Texture2D>("Textures/Menu/Menu" + m + "_background"));
                for (int b = 0; b < buttonsTotal[m]; b++)
                {
                buttonList.Add(content.Load<Texture2D>("Textures/Menu/Menu" + m + "_button" + b));
                }
            }
            

        }


        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
               Game1.CurrentGameState = Game1.GameState.MainMenu;
            }
        

            if (Keyboard.GetState().GetPressedKeys().Length == 0)
            {
                isKeyUp = true;
            }
            else
            {
                isKeyUp = false;
            }

            if (isKeyUp == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    selectedButton--;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    selectedButton++;
            }


        }


        public static void Draw(SpriteBatch spriteBatch, int menu, Vector2 startPosition, int spaceGap)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuList[menu], new Rectangle(0, 0, 1920, 1080), Color.White);

            for (int b = 0; b < bTotal[menu]; b++)
            {
            spriteBatch.Draw(buttonList[b], new Rectangle((int)startPosition.X, 
                (int)startPosition.Y + b * (buttonList[b].Height + spaceGap), 
                buttonList[b].Width, buttonList[b].Height), Color.White);
            }




            spriteBatch.End();
        }
    }
}
