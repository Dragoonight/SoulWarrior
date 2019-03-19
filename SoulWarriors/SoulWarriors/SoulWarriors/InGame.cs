using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public static class InGame
    {
        // Players
        public static Archer Archer;
        public static Knight Knight;

        public static List<Enemy> Enemies;

        public static Camera2D Camera;

        public static Chain Chain;
        private static Texture2D _backgroundTexture;
        
        private static Random random = new Random();
        
        static int frameTime = 1000/6;

        public static Vector2 MousePos => new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

#if DEBUG
        public static Texture2D mousepostest;
#endif

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


        public static void LoadContent(ContentManager content, Viewport viewport)
        {
            Camera = new Camera2D(viewport);

            _backgroundTexture = content.Load<Texture2D>(@"Textures/WorldBackground");

            LoadPlayers(content);
            LoadEnemies(content);

            Chain = new Chain(content.Load<Texture2D>(@"Textures/Chain"));

            mousepostest = content.Load<Texture2D>(@"Textures/Chain");
        }

        private static void LoadPlayers(ContentManager content)
        {
            Texture2D archerTexture = content.Load<Texture2D>(@"Textures/Archer_SpriteSheet");
            List<Animation> archerAnimations = new List<Animation>()
            #region AnimationArcher

                {
                
                    new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,0,79,120), new Vector2(28,58), frameTime),
                        new Frame(new Rectangle(80, 0, 79,120 ), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160, 0, 79,120 ), new Vector2(28, 58), frameTime)               
                    }),
                    new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,120,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80, 120, 79,120 ), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160, 120, 79,120 ), new Vector2(28, 58), frameTime),
                    }),

                    new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,240, 79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80, 240,  79,120 ), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160, 240,  79,120 ), new Vector2(28, 58), frameTime),
                    }),

                    new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,360, 79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80, 360,  79,120 ), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160, 360,  79,120 ), new Vector2(28, 58), frameTime),
                    }),

                    new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,480,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80,480,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160,480,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(240,480,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(320,480,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(400,480,79,120), new Vector2(28, 58), frameTime),
                       
                    }),

                    new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,600,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80,600,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160,600,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(240,600,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(320,600,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(400,600,79,120), new Vector2(28, 58), frameTime),
                        
                    }),

                    new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,720,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80,720,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160,720,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(240,720,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(320,720,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(400,720,79,120), new Vector2(28, 58), frameTime),
                     
                    }),

                    new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(0,840,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(80,840,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(160,840,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(240,840,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(320,840,79,120), new Vector2(28, 58), frameTime),
                        new Frame(new Rectangle(400,840,79,120), new Vector2(28, 58), frameTime),
                        
                    }),


                    new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    }),

                    new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                    {
                        new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                        new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                        new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                    })

                };

            #endregion

            Texture2D arrowTexture = content.Load<Texture2D>(@"Textures/Chain");

            Archer = new Archer(archerTexture, arrowTexture, archerAnimations);


            Texture2D knightTexture = content.Load<Texture2D>(@"Textures/Knight_SpriteSheet");
            List<Animation> knightAnimations = new List<Animation>()
            #region AnimationKnight

            {
                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,79,119), new Vector2(30,65), frameTime),
                    new Frame(new Rectangle(80, 0, 79,119 ), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160, 0, 79,119 ), new Vector2(30, 65), frameTime)
                }),

                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,120,79,119), new Vector2(30,65), frameTime),
                    new Frame(new Rectangle(80, 120, 79,119 ), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160, 120, 79,119 ), new Vector2(30, 65), frameTime)
                }),

                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,240,79,119), new Vector2(30,65), frameTime),
                    new Frame(new Rectangle(80, 240, 79,119 ), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160, 240, 79,119 ), new Vector2(30, 65), frameTime)
                }),

                new Animation(AnimationStates.Idle.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,360,79,119), new Vector2(30,65), frameTime),
                    new Frame(new Rectangle(80, 360, 79,119 ), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160, 360, 79,119 ), new Vector2(30, 65), frameTime)
                }),

                 new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,480,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(80,480,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160,480,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(240,480,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(320,480,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(400,480,79,119), new Vector2(30, 65), frameTime),
                 

                }),

                 new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,600,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(80,600,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160,600,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(240,600,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(320,600,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(400,600,79,119), new Vector2(30, 65), frameTime),
                    

                }),

                 new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,720,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(80,720,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160,720,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(240,720,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(320,720,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(400,720,79,119), new Vector2(30, 65), frameTime),
                  

                }),

                 new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,840,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(80,840,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(160,840,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(240,840,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(320,840,79,119), new Vector2(30, 65), frameTime),
                    new Frame(new Rectangle(400,840,79,119), new Vector2(30, 65), frameTime),
                   

                }),


                new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Action2.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,13,24), new Vector2(7,21), 100),
                    new Frame(new Rectangle(16, 1, 13, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(31, 1, 13, 24 ), new Vector2(7, 21), 100)
                })

                
            };

            #endregion

            Knight = new Knight(knightTexture, knightAnimations);
        }

        private static void LoadEnemies(ContentManager content)
        {
            Enemies = new List<Enemy>();
            Goblin.LoadContent(content);
            Enemies.Add(new Goblin(new Vector2(400f)));
        }

        public static void Update(GameTime gameTime)
        {
            // Update players
            Archer.Update(gameTime);
            Knight.Update(gameTime);
            UpdateChainAndCamera();
            ClampMouse();

            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        private static void UpdateChainAndCamera()
        {
            Chain.StartPosition = Archer.CollidableObject.Position;
            Chain.EndPosition = Knight.CollidableObject.Position;

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
            if (Camera.CameraWorldRect.Contains(new Point((int)MousePos.X, (int)MousePos.Y)) == false)
            {
                Mouse.SetPosition(
                    (int)MathHelper.Clamp(MousePos.X, (float)Camera.CameraWorldRect.Left, (float)Camera.CameraWorldRect.Right),
                    (int)MathHelper.Clamp(MousePos.Y, (float)Camera.CameraWorldRect.Top, (float)Camera.CameraWorldRect.Bottom)); 
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
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
            // Draw chain between players
            Chain.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.TransformMatrix);
            // Draw Player
            Archer.Draw(spriteBatch);
            Knight.Draw(spriteBatch);

            spriteBatch.Draw(mousepostest, MousePos, null, Color.Wheat, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0.2f);

            spriteBatch.End();

            // Begin new spriteBatch without a transform
            // Here things not effected by the camera shall be put (UI etc.)
            spriteBatch.Begin();
            // Draw UI
#if DEBUG
            spriteBatch.DrawString(Game1.DebugFont,
                $" Camera:{Camera.Location}\n Archer:{Archer.CollidableObject.Position}\n Knight:{Knight.CollidableObject.Position}\n Mouse:{MousePos}\n ArchAniIdentifier:{Archer._animationSet.AnimationState.ToString() + Archer._animationSet.AnimationDirection}\n KnigAniIdentifier:{Knight._animationSet.AnimationState.ToString() + Knight._animationSet.AnimationDirection}  Arrows:{Archer.arrows.Count}",
                Vector2.Zero,
                Color.White);
#endif

            spriteBatch.End();
        }
    }
}
