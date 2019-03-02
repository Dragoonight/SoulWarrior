using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    class Health
    {
        Texture2D healthTexture;
        private Rectangle healthRectangle;
        private Vector2 healthPosition;
        public int health;
        public float healthPoints;

        public Texture2D HealthTexture
        {
            get { return healthTexture; }
            set { healthTexture = value; }
        }

        public Rectangle HealthRectangle
        {
            get { return healthRectangle; }
            set { healthRectangle = value; }
        }

        public Vector2 HealthPosition
        {
            get { return healthPosition; }
            set { healthPosition = value; }
        }

        public int HealthBar
        {
            get { return health; }
            set { health = value; }
        }

        public float HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }


        public Health(Texture2D newTexture, Vector2 newPosition, int newHealth, float newhealthPoints)
        {
            this.healthTexture = newTexture;
            this.healthPosition = newPosition;

            this.health = newHealth;
            this.healthPoints = newhealthPoints;
        }

       

        public void Update()
        {
            healthRectangle = new Rectangle((int)healthPosition.X, (int)HealthPosition.Y, healthTexture.Width, healthTexture.Height);
        }

        //Example in draw
        public void Draw(SpriteBatch spritebatch)
        {
            //If health is below zero something happens
            //if (health > 0)
               
        }
    }
}
