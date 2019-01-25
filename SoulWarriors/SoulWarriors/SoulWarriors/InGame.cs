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
        public static Enemy enemy = new Enemy();

        public static Camera2D Camera;

        public static Chain Chain;
        private static Texture2D _backgroundTexture;

        public static List<Enemy> enemies = new List<Enemy>();
        private static Random random = new Random();

        private static float spawn = 0;

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
            enemy.Update(gameTime);
            

            // Update camera location while clamping to bounds of _backgroundTexture.Height and LERPing between old and new position
            Camera.Location = 
                // Clamp to background bounds
                Vector2.Clamp(
                    // Interpolate to smoothen movement
                    Vector2.Lerp(
                    // Vector to interpolate
                    Camera.Location,
                    // Location to interpolate camera to.  
                    new Vector2(
                        // Unit vector from the chains rotation
                        (float) Math.Cos(Chain.Rotation), (float) Math.Sin(Chain.Rotation))
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
            //
            spawn += (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
        //Load enemy depending on the number of spawned enemies
        public static void LoadEnemies(ContentManager content)
        {
            int randY = random.Next(100, 400);

            if (spawn >= 1)
            {
                spawn = 0;
                if (enemies.Count() > 1)
                    enemies.Add(new Enemy(content.Load<Texture2D>(@"Textures/ArcherSpriteSheet"), new Vector2(100, randY)));
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Begin spriteBatch with the camera transform
            // Here the drawings effected by the camera shall be put (Enemies, player, etc.)
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
            // Heree things not effected by the camera shall be put (UI etc.)
            spriteBatch.Begin();
            // Draw UI
#if DEBUG
            spriteBatch.DrawString(DebugFont,
                $" {Camera.Location}\n {Archer.CollidableObject.Position}\n {Camera.TransformMatrix.Translation}\n {Camera.Origin}",
                Vector2.Zero,
                Color.White);
#endif
            //
            foreach (Enemy enemy in enemies)
                enemy.draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
