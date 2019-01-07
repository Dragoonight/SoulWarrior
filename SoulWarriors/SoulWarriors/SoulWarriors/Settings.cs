using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    static class Settings
    {
        private enum MenuStates
        {
            Main,
            KeyBinds
        }

        private static MenuStates _menuState;

        /// <summary>
        ///     String array of menu option names
        /// </summary>
        private static readonly string[] SettingsOptions = { "Key binds", "Back" };
        public static Dictionary<string, Keys> KeyBindOptions = new Dictionary<string, Keys> { ["Move Up"] = Keys.W};


        /// <summary>
        ///     selected menu option
        /// </summary>
        private static Vector2 selected;

        /// <summary>
        ///     MainMenu background image
        /// </summary>
        private static Texture2D _background;

        private static SpriteFont _boldMenuFont;
        private static SpriteFont _normalMenuFont;

        /// <summary>
        ///     Controls keyboard actions in menus
        /// </summary>
        private static MenuControls _mainControls = new MenuControls(new Vector2(0, SettingsOptions.Length - 1));
        private static MenuControls _keyBindsControls = new MenuControls(new Vector2(0, KeyBindOptions.Count - 1));

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
            _background = content.Load<Texture2D>(@"Textures/MainMenuBackground"); // TODO: Update settings background and fonts
            _boldMenuFont = content.Load<SpriteFont>(@"Fonts/BoldMenuFont");
            _normalMenuFont = content.Load<SpriteFont>(@"Fonts/NormalMenuFont");
            _viewport = viewport;
        }

        /// <summary>
        ///     Updates GameState logic
        /// </summary>
        public static void Update()
        {
            // Set PreviousKeyboardState to CurrentKeyboardState
            _previousKeyboardState = _currentKeyboardState;
            // Update CurrentKeyboardState
            _currentKeyboardState = Keyboard.GetState();

            switch (_menuState)
            {
                case MenuStates.Main:
                    // Update selected menu option
                    _mainControls.UpdateSelected(ref selected, _currentKeyboardState, _previousKeyboardState); 

                    // If enter is pressed 
                    if (_currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        switch ((int)selected.Y)
                        {
                            // KeyBinds
                            case 0:
                                _menuState = MenuStates.KeyBinds;
                                break;
                            // Settings
                            case 1:
                                Game1.GameState = Game1.GameStates.MainMenu;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;

                case MenuStates.KeyBinds:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
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

            switch (_menuState)
            {
                case MenuStates.Main:
                    // Iterate through every entry in menuOptionsStr array
                    for (int i = 0; i < SettingsOptions.Length; i++)
                    {
                        // If selected menu option is int i have bold font else normal font
                        spriteBatch.DrawString((int)selected.X == i ? _boldMenuFont : _normalMenuFont, SettingsOptions[i],
                            new Vector2(200 + 200 * i, 400),
                            Color.White);
                    }
                    break;

                case MenuStates.KeyBinds:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            spriteBatch.End();
        }
    }

}
