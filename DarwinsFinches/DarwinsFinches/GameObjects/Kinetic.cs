using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarwinsFinches
{
    public class Kinetic
    {
        public Vector2 position;
        public Vector2 prevPosition;
        public Vector2 velocity;
        Vector2 force;
        public Orientation orientations;
        public Rectangle boundingBox;
        public float maxSpeed;
        public Vector2 target;

        public Kinetic(Vector2 Position, Vector2 Velocity, Rectangle BoundingBox)
        {
            position = Position;
            prevPosition = Position;
            velocity = Velocity;
            orientations = new Orientation(velocity);
            boundingBox = BoundingBox;
            maxSpeed = 3;
        }

        public void Update()
        {
            prevPosition = position;
            if (velocity.LengthSquared() > 0){
                orientations.SetOrientation(velocity);
            }
            position += velocity + force;
            force = Vector2.Zero;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        public void SetTarget(Vector2 Target) {
            target = Target;
        }

        static public void SetBoundingBoxDimensions(ref Rectangle box, Texture2D texture)
        {
            box.Width = texture.Width;
            box.Height = texture.Height;
        }

        public void ApplyForce(Vector2 Force) {
            force += Force;
        }

        static public Kinetic ZERO()
        {
            return new Kinetic(Vector2.Zero, Vector2.Zero, new Rectangle());
        }
    }
}
