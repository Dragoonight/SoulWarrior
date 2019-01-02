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
        public static Knight Knight;
        public static Archer Archer;

        private static Chain _chain;

        public static Camera2D Camera;

        private static Texture2D _backgroundTexture;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            Camera = new Camera2D(graphicsDevice.Viewport);
        }

        public static void LoadContent(ContentManager contentManager)
        {
            _backgroundTexture = contentManager.Load<Texture2D>(@"Textures/WorldBackground");
            _chain = new Chain(contentManager.Load<Texture2D>(@"Textures/Chain"));
        }

        public static void Update(GameTime gameTime)
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw World
            
            // Draw Enemies
            // Draw chain between players
            _chain.Draw(spriteBatch, Archer.CollidableObject.Position, Knight.CollidableObject.Position);
            // Draw Player
            Archer.Draw(spriteBatch);
            Knight.Draw(spriteBatch);
            // Draw UI
        }
    }
}
