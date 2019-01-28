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

        public Goblin() : base(texture, new Vector2(400f), AiTypes.Smart)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            // Load goblin texture
            texture = content.Load<Texture2D>(@"Textures/ArcherSpriteSheet");
            animations = new List<Animation>()
            {
                new Animation("IdleDown", new List<Frame>() { new Frame(Rectangle.Empty, Int32.MaxValue)})
            };
        }
    }
}
