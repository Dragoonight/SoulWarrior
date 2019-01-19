﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    public static class InGame
    {
        public static Archer Archer = new Archer();
        public static Knight Knight = new Knight();

        public static Camera2D Camera;

        public static Chain Chain;
        private static Texture2D _backgroundTexture;

        /// <summary>
        /// The area in _backgroundTexture that is ground
        /// </summary>
        public static Rectangle PlayArea => new Rectangle(32, 272, _backgroundTexture.Width - 64, _backgroundTexture.Height - 272);

#if DEBUG
        private static SpriteFont DebugFont;
#endif

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            Camera = new Camera2D(graphicsDevice.Viewport);
        }

        public static void LoadContent(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>(@"Textures/WorldBackground");
            Chain = new Chain(content.Load<Texture2D>(@"Textures/Chain"));

            Archer.LoadContent(content);
            Knight.LoadContent(content);
#if DEBUG
            DebugFont = content.Load<SpriteFont>(@"Fonts/DebugFont");
#endif
        }

        public static void Update(GameTime gameTime)
        {
            // Update keyboard state
            Player.UpdateKeyboard();
            // Update players
            Archer.Update(gameTime);
            Knight.Update(gameTime);

            // Update camera location while clamping to bounds of _backgroundTexture.Height and LERPing between old and new position
            Camera.Location = Vector2.Clamp(
                    Vector2.Lerp(Camera.Location,
                    new Vector2((float) Math.Cos(Chain.Rotation), (float) Math.Sin(Chain.Rotation)) * (Chain.Length / 2f) + Archer.CollidableObject.Position,
                    0.2f),
                Camera.Origin,
                new Vector2(_backgroundTexture.Width - Camera.Origin.X, _backgroundTexture.Height - Camera.Origin.Y)); 
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Begin spriteBatch with the camera transform
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);

            // Draw World
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
            // Draw Enemies
            // Draw chain between players
            Chain.Draw(spriteBatch);
            // Draw Player
            Archer.Draw(spriteBatch);
            Knight.Draw(spriteBatch);
            spriteBatch.End();

            // Begin new spriteBatch without a transform
            spriteBatch.Begin();
            // Draw UI
#if DEBUG
            spriteBatch.DrawString(DebugFont,
                $" {Camera.Location}\n {Archer.CollidableObject.Position}\n {Camera.TransformMatrix.Translation}\n {Camera.Origin}",
                Vector2.Zero,
                Color.White);
#endif
            spriteBatch.End();
        }
    }
}
