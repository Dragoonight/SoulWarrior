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
            texture = content.Load<Texture2D>(@"Textures/ArcherSpriteSheet");
            animations = new List<Animation>()
            {
                new Animation(AnimationStates.Walk.ToString() + AnimationDirections.Left.ToString(), new List<Frame>()
                {
                    new Frame(new Rectangle(1,1,15,25), new Vector2(7,21), 100),
                    new Frame(new Rectangle(17, 1, 15, 24 ), new Vector2(7, 21), 100),
                    new Frame(new Rectangle(33, 1, 15, 24 ), new Vector2(7, 21), 100)
                }),
                //new Animation("IdleDown", new List<Frame>() { new Frame(Rectangle.Empty, Int32.MaxValue)})
            };
        }
    }
}
