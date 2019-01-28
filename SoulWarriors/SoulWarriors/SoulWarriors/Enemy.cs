using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SoulWarriors
{
    public enum Targets
    {
        Door,
        Archer,
        Knight
    }

    public class Enemy
    {

        public CollidableObject CollidableObject;
        protected readonly Vector2 SpawnPosition;

        protected Targets target;
        protected Vector2 targetPosition;

        /// <summary>
        /// movement speed in pixels per millisecond
        /// </summary>
        protected float speed = .1f;

        protected Enemy(Vector2 spawnPosition, Targets defaultTarget)
        {
            this.SpawnPosition = spawnPosition;
            SetTarget(defaultTarget);
        }


        /// <summary>
        /// adds velocity to position while clamping to PlayArea
        /// </summary>
        protected void AddToPosition(Vector2 valueToAdd)
        {
            CollidableObject.Position = Vector2.Clamp(
                CollidableObject.Position + valueToAdd,
                new Vector2(InGame.PlayArea.Left, InGame.PlayArea.Top),
                new Vector2(InGame.PlayArea.Right, InGame.PlayArea.Bottom));
        }

        /// <summary>
        /// Sets target and targetPosition to a new target
        /// </summary>
        /// <param name="newTarget"></param>
        protected void SetTarget(Targets newTarget)
        {
            this.target = newTarget;
            switch (newTarget)
            {
                case Targets.Door:
                    targetPosition = new Vector2(960, 250);
                    break;
                case Targets.Archer:
                    targetPosition = InGame.Archer.CollidableObject.Position;
                    break;
                case Targets.Knight:
                    targetPosition = InGame.Knight.CollidableObject.Position;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newTarget), newTarget, null);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CollidableObject.Texture,
                CollidableObject.Position,
                CollidableObject.SourceRectangle,
                Color.White,
                CollidableObject.Rotation,
                CollidableObject.origin,
                1,
                SpriteEffects.None,
                0);

        }

        // Key Rectangles/Areas (Spawn and target)



        // Go directly for the door



        // Mainly attack player (check who is closed to the enemy)


        // Go for door, attack player if near by (Check which circle is colliding first)




    }
}
