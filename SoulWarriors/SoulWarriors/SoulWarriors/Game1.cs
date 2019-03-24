using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNAGameConsole;

namespace SoulWarriors
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public enum GameState
        {
            InGame,
            MainMenu,
            HighScore,
            Exit
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameConsole _console;

#if DEBUG
        public static SpriteFont DebugFont;
#endif

        public static GameState CurrentGameState = GameState.MainMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Calculate new window size
            Tuple<int, int> windowSize = CalculateWindowResolution();

            // Set window size 
            _graphics.PreferredBackBufferWidth = windowSize.Item1;
            _graphics.PreferredBackBufferHeight = windowSize.Item2;

            IsFixedTimeStep = false;
            IsMouseVisible = true;
        }

        private static Tuple<int, int> CalculateWindowResolution()
        {
            const float acceptableAspectRatio = 16f / 9f;

            int displayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int displayheight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Return error if screen is too small
            if (displayWidth < 1024 || displayheight < 576)
            {
                throw new Exception("Display resolution is too small, minimum size is 1024x576");
            }
            // if screen is 16:9 use that resolution
            if (Math.Abs(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio - acceptableAspectRatio) < 0.000001)
            {
                return new Tuple<int, int>(displayWidth, displayheight);
            }

            // Calculate largest possible resolution
            int newWidth = (int) (displayheight / acceptableAspectRatio);

            // Return that if it fits within the screen
            if (newWidth <= displayWidth) return new Tuple<int, int>(newWidth, displayheight);
            // Else calculate by how much it was too large
            float scalar = (float)newWidth / displayWidth;
            // Scale window
            newWidth = (int)(newWidth * scalar);
            int newHeight = (int) (displayheight * scalar);

            return new Tuple<int, int>(newWidth, newHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            HighScore.Initilize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);
            GameConsoleOptions consoleOptions = new GameConsoleOptions()
            {
                ToggleKey = Keys.Insert,
                Padding = 10,
                OpenOnWrite = true,
            };
            IConsoleCommand[] commands = new IConsoleCommand[] {new BeepCommand() };
            

            _console = new GameConsole(this, _spriteBatch, commands, consoleOptions);

            InGame.LoadContent(Content, GraphicsDevice);
            Main.LoadContent(Content, GraphicsDevice.Viewport);
            HighScore.LoadContent(Content, GraphicsDevice.Viewport);
#if DEBUG
            DebugFont = Content.Load<SpriteFont>(@"Fonts/DebugFont");
#endif

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.End) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // If console is opened, skip updating game
            if (_console.Opened)
            {
                base.Update(gameTime);
                return;
            }

            //Fullscreen
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            switch (CurrentGameState)
            {
                case GameState.InGame:
                    if (IsMouseVisible == true)
                    {
                        IsMouseVisible = false;
                    }
                    InGame.Update(gameTime);

                    break;

                case GameState.MainMenu:
                    Main.Update();

                    break;

                case GameState.HighScore:
                    HighScore.Update();
                    break;

                case GameState.Exit:
                    this.Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentGameState)
            {
                case GameState.InGame:
                    InGame.Draw(_spriteBatch);
                    break;
                case GameState.MainMenu:
                    Main.Draw(_spriteBatch);
                    break;
                case GameState.Exit:
                    break;
                case GameState.HighScore:
                    HighScore.Draw(_spriteBatch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            base.Draw(gameTime);
        }
    }
}
