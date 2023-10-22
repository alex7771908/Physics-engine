using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Physics_engine.Classes;

namespace Physics_engine.Classes
{
    public class Circles
    {
        private Texture2D texture;
        private Texture2D collisionTexture;
        private Rectangle destinationRectangle;
        private Rectangle collision;
        bool done = false;
        public bool Done { get { return done; } }
        public Rectangle CircleCollision
        {
            get { return collision; }
        }
        private double ySpeed;
        private double yAcceleration;
        private double xAcceleration;

        private double xSpeed;

        private float rotation = 0;

        float prevX;

        public Circles(int x, int y)
        {
            destinationRectangle = new Rectangle(x - 50, y - 50, 100, 100);
            ySpeed = 0;
            yAcceleration = 20;
            xAcceleration = 0;
            collision = destinationRectangle;
            destinationRectangle = collision;
            prevX = destinationRectangle.X;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("circle");
            collisionTexture = content.Load<Texture2D>("collision");
        }

        public void Update(Ground ground, GameTime gameTime, Wind wind, List<Magnet> magnets)
        {
            
            if (!this.CircleCollision.Intersects(ground.GroundCollision))
            {
                destinationRectangle = new Rectangle((int)Math.Floor(destinationRectangle.X + wind.speed.X * gameTime.ElapsedGameTime.TotalSeconds), (int)Math.Floor(destinationRectangle.Y + ySpeed * gameTime.ElapsedGameTime.TotalSeconds), 100, 100);
                collision = destinationRectangle;
                ySpeed += yAcceleration;

            }
            else
            {
                int inside = collision.Y + 100 - ground.GroundCollision.Y;
                if (inside >= 0)
                {
                    destinationRectangle.Y -= inside - 2;
                    collision = destinationRectangle;
                    done = true;
                }
                xSpeed  = wind.speed.X * gameTime.ElapsedGameTime.TotalSeconds;
                destinationRectangle.X += (int)xSpeed;

            }

            foreach (Magnet magnet in magnets)
            {
                Vector2 dist = new Vector2(magnet.Position.X - this.destinationRectangle.X, magnet.Position.Y - this.destinationRectangle.Y);
                double length = Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);
                Vector2 dir = new Vector2(dist.X / (float)length, dist.Y / (float)length);
                xSpeed = ((dir.X * 20 * magnet.Strength) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                ySpeed = ((dir.Y * 20 * magnet.Strength) * (float)gameTime.ElapsedGameTime.TotalSeconds);
               // Debug.Write("xSpeed: " + Math.Round(xSpeed) + " ySpeed" + Math.Round(ySpeed));
                destinationRectangle = new Rectangle(destinationRectangle.X += (int)xSpeed, destinationRectangle.Y += (int)ySpeed, destinationRectangle.Width, destinationRectangle.Height);
            }
            rotation = (destinationRectangle.X - prevX) / (destinationRectangle.Width * (float)Math.PI) * 360;  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, destinationRectangle, Color.Black);
            Rectangle dest = new Rectangle(destinationRectangle.X + destinationRectangle.Width / 2, destinationRectangle.Y + destinationRectangle.Height / 2, destinationRectangle.Width, destinationRectangle.Height);
            spriteBatch.Draw(texture, dest, null, Color.White, MathHelper.ToRadians(rotation),
                new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);
            //spriteBatch.Draw(collisionTexture, collision, Color.Green);
        }
    }
}
