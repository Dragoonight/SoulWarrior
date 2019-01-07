using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    /// <summary>
    ///     Draws and does logic for the gameState mainMenu
    /// </summary>
    static class MainMenu
    {

        /// <summary>
        ///     String array of menu option names
        /// </summary>
        private static readonly string[] MenuOptionsStr = {"Play", "Settings", "Exit"};

        /// <summary>
        ///     selected menu option
        /// </summary>
        private static Vector2 _selected;

        /// <summary>
        ///     MainMenu background image
        /// </summary>
        private static Texture2D _background;

        private static SpriteFont _boldMenuFont;
        private static SpriteFont _normalMenuFont;

        /// <summary>
        ///     Controls keyboard actions in menus
        /// </summary>
        private static MenuControls _menuControl = new MenuControls(new Vector2(MenuOptionsStr.Length - 1, 0));

        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;

        private static Viewport _viewport;


        /// <summary>
        /// Load MainMenu
        /// </summary>
        /// <param name="content"></param>
        /// <param name="viewport"></param>
        public static void LoadContent(ContentManager content, Viewport viewport)
        {
            _background = content.Load<Texture2D>(@"Textures/MainMenuBackground");
            _boldMenuFont = content.Load<SpriteFont>(@"Fonts/BoldMenuFont");
            _normalMenuFont = content.Load<SpriteFont>(@"Fonts/NormalMenuFont");
            _viewport = viewport;
        }

        /// <summary>
        ///     Updates MainMenu GameState logic
        /// </summary>
        public static void Update()
        {
            // Set PreviousKeyboardState to CurrentKeyboardState
            _previousKeyboardState = _currentKeyboardState;
            // Update CurrentKeyboardState
            _currentKeyboardState = Keyboard.GetState();
            // Update selected menu option
            _menuControl.UpdateSelected(ref _selected, _currentKeyboardState, _previousKeyboardState); 

            // If enter is pressed 
            if (_currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                // Then change GameState
                switch ((int)_selected.X)
                {
                    // Play
                    case 0:
                        Game1.GameState = Game1.GameStates.InGame;
                        break;
                    // Settings
                    case 1:
                        Game1.GameState = Game1.GameStates.Settings; 
                        break;
                    // Exit
                    case 2:
                        Game1.GameState = Game1.GameStates.Exit;
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///     Draws the MainMenu GameState
        /// </summary>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw background in whole window
            spriteBatch.Draw(_background, _viewport.Bounds, Color.White);

            // Iterate through every entry in menuOptionsStr array
            for (int i = 0; i < MenuOptionsStr.Length; i++)
            {
                // If selected menu option is int i have bold font else normal font
                spriteBatch.DrawString((int)_selected.X == i ? _boldMenuFont : _normalMenuFont, MenuOptionsStr[i],
                    new Vector2(200 + 200 * i, 400),
                    Color.White);
            }

            spriteBatch.End();
        }
    }
}