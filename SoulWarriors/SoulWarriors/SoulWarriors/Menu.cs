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
        private static Texture2D selectionTexture;
        private static int menuTotal;
        private static int[] bTotal;

        private static int selectedButton;
        private static int menu;

        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;


        public static void LoadContent(ContentManager content, int menuTotal, int[] buttonsTotal)
        {
            Menu.menuTotal = menuTotal;
            bTotal = buttonsTotal;

            selectionTexture = content.Load<Texture2D>("Textures/Menu/Selection");


            for (int m = 0; m < menuTotal; m++)
            {
            menuList.Add(content.Load<Texture2D>("Textures/Menu/Menu" + m + "_background"));
                for (int b = 0; b < buttonsTotal[m]; b++)
                {
                buttonList.Add(content.Load<Texture2D>("Textures/Menu/Menu" + m + "_button" + b));
                }
            }
            

        }


        public static void Update(int selectedMenu)
        {

            if (selectedMenu != menu)
                selectedButton = 0;

            menu = selectedMenu;

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
               Game1.CurrentGameState = Game1.GameState.MainMenu;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                Game1.CurrentGameState = Game1.GameState.SubMenu;
            }

            // Set previous KeyboardState
            previousKeyboardState = currentKeyboardState;
            // Get current KeyboardState 
            currentKeyboardState = Keyboard.GetState();

                if (Keyboard.GetState().SingleActivationKey(previousKeyboardState, Keys.W) && selectedButton > 0)
                    selectedButton--;
                if (Keyboard.GetState().SingleActivationKey(previousKeyboardState, Keys.S) && selectedButton < bTotal[selectedMenu] -1)
                    selectedButton++;

            // Menu manager
            if (Keyboard.GetState().SingleActivationKey(previousKeyboardState, Keys.Enter) && selectedButton > 0)
            {
                if (selectedMenu == 0)
                {
                    if (selectedButton == 0)
                    {

                    }
                    if (selectedButton == 1)
                    {

                    }
                    if (selectedButton == 2)
                    {

                    }
                }






            }




        }


        public static void Draw(SpriteBatch spriteBatch, Vector2 startPosition, int spaceGap)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuList[menu], new Rectangle(0, 0, 1920, 1080), Color.White);

            for (int b = 0; b < bTotal[menu]; b++)
            {
            spriteBatch.Draw(buttonList[b], new Rectangle((int)startPosition.X, 
                (int)startPosition.Y + b * (buttonList[b].Height + spaceGap), 
                buttonList[b].Width, buttonList[b].Height), Color.White);
            }

            spriteBatch.Draw(selectionTexture, new Rectangle((int)startPosition.X,
                (int)startPosition.Y + selectedButton * (buttonList[selectedButton].Height + spaceGap),
                buttonList[selectedButton].Width, buttonList[selectedButton].Height), Color.White);


            spriteBatch.End();
        }
    }
}
