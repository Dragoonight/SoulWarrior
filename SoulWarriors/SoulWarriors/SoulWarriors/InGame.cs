using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shadows2D;

namespace SoulWarriors
{
    public static class InGame
    {
        // Players
        public static Archer Archer;
        public static Knight Knight;
        public static Chain Chain;

        public static List<Enemy> Enemies;

        public static Camera2D Camera;

        private static Texture2D _backgroundTexture;

        private static List<LightArea> lightAreas;
        private static Color _ambientLight = new Color(32,32,32,255);
        public static ShadowmapResolver shadowmapResolver;

        private static RenderTarget2D screenShadows;

        public static Vector2 MousePos => new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        public static Texture2D Recticle;

        private static Random random = new Random();

        /// <summary>
        /// The area in _backgroundTexture that is ground
        /// </summary>
        public static Rectangle PlayArea => new Rectangle(32, 272, _backgroundTexture.Width - 64, _backgroundTexture.Height - 272);

        public static Vector2 GetOtherPlayerPosition(Vector2 yourPosition)
        {
            if (yourPosition == Archer.CollidableObject.Position)
            {
                return Knight.CollidableObject.Position;
            }
            else if (yourPosition == Knight.CollidableObject.Position)
            {
                return Archer.CollidableObject.Position;
            }
            else
            {
                throw new ArgumentException();
            }
        }


        


        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Camera = new Camera2D(graphicsDevice.Viewport);

            _backgroundTexture = content.Load<Texture2D>(@"Textures/WorldBackground");

            LoadLightAreas(content, graphicsDevice);

            shadowmapResolver = new ShadowmapResolver(graphicsDevice, Game1.quadRender, ShadowmapSize.Size1024, ShadowmapSize.Size1024);
            shadowmapResolver.LoadContent(content);

            screenShadows = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            LoadPlayers(content);
            LoadEnemies(content);

            Chain = new Chain(content.Load<Texture2D>(@"Textures/Chain"));

            Recticle = content.Load<Texture2D>(@"Textures/Chain");
        }

        private static void LoadPlayers(ContentManager content)
        {
            // Load archer spritesheet
            Texture2D archerTexture = content.Load<Texture2D>(@"Textures/ArcherSpriteSheet");
            // Load archer animations
            List<Animation> archerAnimations = new List<Animation>()
            {
                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,100,100), new Vector2(0,0), Int32.MaxValue)
                })
            };
            // Load arrow Texture
            Texture2D arrowTexture = content.Load<Texture2D>(@"Textures/ArcherSpriteSheet");
            // Create archer with archer sprite sheet, arrow texture and archer 
            Archer = new Archer(archerTexture, arrowTexture, archerAnimations);

            Texture2D knightTexture = content.Load<Texture2D>(@"Textures/KnightSpriteSheet");
            List<Animation> knightAnimations = new List<Animation>()
            {
                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,100,100), Int32.MaxValue)
                })
            };

            Knight = new Knight(knightTexture, knightAnimations);
        }

        private static void LoadEnemies(ContentManager content)
        {
            Enemies = new List<Enemy>();
            Goblin.LoadContent(content);
            Enemies.Add(new Goblin(SpawnAreas.Left));
            Enemies.Add(new Goblin(SpawnAreas.Middle));
            Enemies.Add(new Goblin(SpawnAreas.Right));
        }

        private static void LoadLightAreas(ContentManager content, GraphicsDevice graphicsDevice)
        {
            lightAreas = new List<LightArea>();
            lightAreas.Add(new LightArea(graphicsDevice, ShadowmapSize.Size1024, new Vector2(500), new Color(255, 255,255, 100)));
        }





        public static void Update(GameTime gameTime)
        {
            Archer.Update(gameTime);
            Knight.Update(gameTime);
            UpdateChain();
            UpdateCamera();
            ClampMouse();

#if DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Enemies.Add(new Goblin(SpawnAreas.Left));
                Enemies.Add(new Goblin(SpawnAreas.Middle));
                Enemies.Add(new Goblin(SpawnAreas.Right));
            }
#endif
            // Update enemies
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        /// <summary>
        /// Updates chain start and end positions
        /// </summary>
        private static void UpdateChain()
        {
            Chain.StartPosition = Archer.CollidableObject.Position;
            Chain.EndPosition = Knight.CollidableObject.Position;
        }

        /// <summary>
        /// Updates camera location
        /// </summary>
        private static void UpdateCamera()
        {
            // Update camera location while clamping to bounds of _backgroundTexture.Height and interpolating between old and new position
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

        private static void ClampMouse()
        {
            // If the mouse is outside the camera view
            if (Camera.CameraWorldRect.Contains(new Point((int)MousePos.X, (int)MousePos.Y)) == false)
            {
                // Move the mouse to inside the cameras view
                Mouse.SetPosition(
                    (int)MathHelper.Clamp(MousePos.X, (float)Camera.CameraWorldRect.Left, (float)Camera.CameraWorldRect.Right),
                    (int)MathHelper.Clamp(MousePos.Y, (float)Camera.CameraWorldRect.Top, (float)Camera.CameraWorldRect.Bottom)); 
            }
        }






        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Update light Areas
            foreach (LightArea lightArea in lightAreas)
            {
                lightArea.LightPosition = MousePos; // TODO: move position update to Update
                lightArea.BeginDrawingShadowCasters();
                DrawCasters(lightArea, spriteBatch);
                lightArea.EndDrawingShadowCasters();
                shadowmapResolver.ResolveShadows(lightArea.RenderTarget, lightArea.RenderTarget, lightArea.LightPosition);
            }

            // generate Shadows
            graphicsDevice.SetRenderTarget(screenShadows);
            // Apply ambient light
            graphicsDevice.Clear(_ambientLight);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            foreach (LightArea lightArea in lightAreas)
            {
                spriteBatch.Draw(lightArea.RenderTarget, lightArea.LightPosition - lightArea.LightAreaSize * 0.5f, lightArea.Color);
            }
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);


            graphicsDevice.Clear(Color.Black);

            // Draw things affected by shadows here
            DrawBackgroundObjects(spriteBatch);

            DrawForegroundObjects(spriteBatch);

            BlendState blendState = new BlendState();
            blendState.ColorSourceBlend = Blend.DestinationColor;
            blendState.ColorDestinationBlend = Blend.SourceColor;
            // Draw shadows
            spriteBatch.Begin(SpriteSortMode.Immediate, blendState, null, null, null, null, Camera.TransformMatrix);
            spriteBatch.Draw(screenShadows, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Draw things NOT affected by shadows here
            DrawOverlay(spriteBatch);
        }

        /// <summary>
        /// Draw the things which should cast shadows,
        /// Object position must be relative to the LightArea
        /// </summary>
        /// <param name="lightArea"></param>
        /// <param name="spriteBatch"></param>
        private static void DrawCasters(LightArea lightArea, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Enemies
            foreach (Enemy enemy in Enemies)
            {
                enemy.DrawAsShadowCaster(spriteBatch, lightArea.ToRelativePosition(enemy.CollidableObject.Position));
            }

            spriteBatch.End();
        }


        private static void DrawBackgroundObjects(SpriteBatch spriteBatch)
        {
            // Here the drawings effected by the camera shall be put (Enemies, player, etc.)
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);

            // Draw World
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            spriteBatch.End();
        }

        private static void DrawForegroundObjects(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, Camera.TransformMatrix);
            // Chain
            Chain.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);

            // Enemies
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            // Players
            Archer.Draw(spriteBatch);
            Knight.Draw(spriteBatch);


            spriteBatch.End();

        }

        private static void DrawOverlay(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);
            spriteBatch.Draw(Recticle, MousePos, null, Color.Wheat, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0.2f);
            spriteBatch.End();

#if DEBUG
            spriteBatch.Begin();
            spriteBatch.DrawString(Game1.DebugFont,
                $" Camera:{Camera.Location}\n Archer:{Archer.CollidableObject.Position}\n Knight:{Knight.CollidableObject.Position}\n Mouse:{MousePos}\n ArchAniIdentifier:{Archer._animationSet.AnimationState.ToString() + Archer._animationSet.AnimationDirection}\n KnigAniIdentifier:{Knight._animationSet.AnimationState.ToString() + Knight._animationSet.AnimationDirection}\n Arrows:{Archer.arrows.Count}",
                Vector2.Zero,
                Color.White);
            spriteBatch.End();
#endif

        }
    }
}
