using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Physics_engine.Classes
{
    public class Ground
    {
        private Texture2D texture;
        //private Texture2D collisionTexture;
        private Rectangle destinationRectangle;
        private Rectangle collision;
        public Rectangle GroundCollision
        {
            get { return collision; }
        }
        
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ground");
            //collisionTexture = contentManager.Load<Texture2D>("collision");
            destinationRectangle = new Rectangle(0, 900, 1920, 180);
            collision = destinationRectangle;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
            //spriteBatch.Draw(collisionTexture, collision, Color.Green);
        }
    }
}
