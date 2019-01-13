using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SoulWarriors
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum GameState
        {
            InGame,
            MainMenu
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static GameState CurrentGameState = GameState.InGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set window size to 1920 * 1080
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            InGame.Initialize(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            InGame.LoadContent(Content);
            Menu.LoadContent(Content, 1, new []{3});
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Player.currentKeyboardState.IsKeyDown(Keys.End))
                this.Exit();

            switch (CurrentGameState)
            {
                case GameState.InGame:
                    InGame.Update(gameTime);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))
                        CurrentGameState = GameState.MainMenu;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    break;
                case GameState.MainMenu:
                    Menu.Update(gameTime);


                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Keyboard.GetState().IsKeyDown(Keys.D2))
                        CurrentGameState = GameState.InGame;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    InGame.Draw(spriteBatch);
                    break;
                case GameState.MainMenu:
                    Menu.Draw(spriteBatch, 0, new Vector2(30,50), 10);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            base.Draw(gameTime);
        }
    }
}
