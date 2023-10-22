using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics_engine.Classes.Components;
using Physics__engine;

namespace Physics_engine.Classes
{
    public class Magnet
    {
        private Texture2D texture;
        private Vector2 position;
        private float strengthConst;
        private float strength;
        private int width = 100;
        private int height = 100;
        private Rectangle destinationRectangle;

        public float StrengthConst
        {
            get { return strengthConst; }
        }

        public float Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Width
        {

            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Magnet(Vector2 pos)
        {
            position = pos;
            texture = null;
            strengthConst = 1;
            strength = strengthConst;
        }

        public Magnet(Vector2 pos, float str)
        {
            position = pos;
            texture = null;
            strengthConst = str;
            strength = strengthConst;
        }

        public void Update()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("magnet");
            position = new Vector2(position.X - width/2, position.Y - height/2);
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }
    }
}
