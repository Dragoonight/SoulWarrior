﻿using System;
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
    public class Goblin : Enemy
    {
        private static Texture2D texture;
        private static List<Animation> animations;

        public Goblin(Vector2 spawnPosition) : base(texture, spawnPosition, AiTypes.Smart, 0.2f, animations)
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
                    new Frame(new Rectangle(1,1,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(100, 1, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(200, 1, 84, 72 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(300,1,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(400, 1, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(500, 1, 84, 72 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Down.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,100,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(100, 100, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(200, 100, 84, 72 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(300,100,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(400, 100, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(500, 100, 84, 72 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Right.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,200,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(100, 200, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(200, 200, 84, 72 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(300,200,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(400, 200, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(500, 200, 84, 72 ), new Vector2(7, 21), 100)
                }),

                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,300,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(100, 300, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(200, 300, 84, 72 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(300,300,84,76), new Vector2(7,21), 100),
                    new Frame(new Rectangle(400, 300, 84, 80 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(500, 300, 84, 72 ), new Vector2(7, 21), 100)
                }),
            };
        }
    }
}
