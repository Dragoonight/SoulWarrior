using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoulWarriors
{
    public class Camera2D
    {
        public float Zoom { get; set; } = 1f;
        public Vector2 Location { get; set; } = Vector2.Zero;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; private set; }


        public Matrix TransformMatrix =>  Matrix.CreateTranslation(new Vector3(-Location.X, -Location.Y, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Zoom) *
            Matrix.CreateTranslation(new Vector3(Origin, 0));

        public Camera2D(Viewport viewport)
        {
            Origin = new Vector2(viewport.Width, viewport.Height) / 2f;
        }
    }
}
