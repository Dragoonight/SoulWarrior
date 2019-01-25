using System;
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
        }

        public static void Update(GameTime gameTime)
        {
            // Update keyboard state
            Player.UpdateKeyboard();
            // Update players
            Archer.Update(gameTime);
            Knight.Update(gameTime);

            UpdateChainAndCamera();
        }

        private static void UpdateChainAndCamera()
        {
            Chain.StartPosition = Archer.CollidableObject.Position;
            Chain.EndPosition = Knight.CollidableObject.Position;

            // Update camera location while clamping to bounds of _backgroundTexture.Height and LERPing between old and new position
            Camera.Location =
                // Clamp to background bounds
                Vector2.Clamp(
                    // Interpolate to smoothen movement
                    Vector2.SmoothStep(
                        // Vector to interpolate
                        Camera.Location,
                        // Location to interpolate camera to.
                        new Vector2(
                            // Unit vector from the chains rotation
                            (float)Math.Cos(Chain.Rotation), (float)Math.Sin(Chain.Rotation))
                        // multiplied by half of chains length
                        * (Chain.Length / 2f)
                        // plus the chain´s stating position
                        + Chain.StartPosition,
                        // Interpolation speed
                        0.2f),
                    // Min
                    Camera.ZoomedOrigin,
                    // Max
                    new Vector2(_backgroundTexture.Width - Camera.ZoomedOrigin.X, _backgroundTexture.Height - Camera.ZoomedOrigin.Y));

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Begin spriteBatch with the camera transform
            // Here the drawings effected by the camera shall be put (Enemies, player, etc.)
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);

            // Draw World
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
            // Draw Enemies
            spriteBatch.End();
            // Draw chain between players
            Chain.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);
            // Draw Player
            Archer.Draw(spriteBatch);
            Knight.Draw(spriteBatch);
            spriteBatch.End();

            // Begin new spriteBatch without a transform
            // Here things not effected by the camera shall be put (UI etc.)
            spriteBatch.Begin();
            // Draw UI
#if DEBUG
            spriteBatch.DrawString(Game1.DebugFont,
                $" {Camera.Location}\n {Archer.CollidableObject.Position}\n {Camera.TransformMatrix.Translation}",
                Vector2.Zero,
                Color.White);
#endif
            spriteBatch.End();
        }
    }
}
