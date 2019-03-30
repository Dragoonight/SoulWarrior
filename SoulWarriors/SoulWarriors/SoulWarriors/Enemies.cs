using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    public sealed class Goblin : Enemy
    {
        private static Texture2D texture;
        private static List<Animation> animations;
        private static int aniSpeed = 1000/6; //6fps

        public Goblin(SpawnAreas spawnAreas) : base(texture, spawnAreas, AiTypes.Smart, 0.15f, animations)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            // Load goblin texture
            texture = content.Load<Texture2D>(@"Textures/Goblin_SpriteSheet");
            animations = new List<Animation>()
            {
                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(200, 0, 84, 72 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(300,0,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(400, 0, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(500, 0, 84, 72 ), new Vector2(7, 21), aniSpeed)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,100,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(100, 100, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(200, 100, 84, 72 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(300,100,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(400, 100, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(500, 100, 84, 72 ), new Vector2(7, 21), aniSpeed)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,200,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(100, 200, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(200, 200, 84, 72 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(300,200,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(400, 200, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(500, 200, 84, 72 ), new Vector2(7, 21), aniSpeed)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,300,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(100, 300, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(200, 300, 84, 72 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(300,300,84,76), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(400, 300, 84, 80 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(500, 300, 84, 72 ), new Vector2(7, 21), aniSpeed)
                }),
            };
        }
    }

    public sealed class BatteringRam : Enemy
    {
        private static Texture2D texture;
        private static List<Animation> animations;
        private static int aniSpeed = 1000 / 6; //6fps

        public BatteringRam(SpawnAreas spawnAreas) : base(texture, spawnAreas, AiTypes.Smart, 0.15f, animations)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            // Load batteringRam texture
            texture = content.Load<Texture2D>(@"Textures/BatteringRam_SpriteSheet");
            animations = new List<Animation>()
            {
                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,0,280,400), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(280, 0, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(560,0,280,400), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(820, 0, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(1100, 0, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(1380, 0, 280, 400 ), new Vector2(7, 21), aniSpeed)
                }),

                new Animation(AnimationStates.Action1.ToString() + AnimationDirections.Up.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(0,180,280,400), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(280, 180, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(560,180,280,400), new Vector2(7,21), aniSpeed),
                    new Frame(new Rectangle(820, 180, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(1100, 180, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(1380, 180, 280, 400 ), new Vector2(7, 21), aniSpeed),
                    new Frame(new Rectangle(1660, 180, 280, 400 ), new Vector2(7, 21), aniSpeed)
                })
            };
        }
    }

    //Insert new enemy here

}
