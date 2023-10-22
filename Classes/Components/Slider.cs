using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Physics_engine.Classes;

namespace Physics_engine.Classes
{
    public class Slider
    {
        private float value;
        private Vector2 positionLine;
        private Vector2 positionCircle;
        private int lengthLine;
        private Texture2D textureCircle;
        private Texture2D textureLine;

        KeyboardState state;
        KeyboardState prevState;

        private bool onOff = false;

        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private float maxValue = 1000;

        private float minValue = -1000;

        public float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public Slider()
        {
        }

        public Slider(Vector2 position, int length)
        {
            positionLine = position;
            lengthLine = length;   
        }

        public void Update(Wind wind)
        {
            #region CheckButton
            state = Keyboard.GetState();
            if (state.IsKeyUp(Keys.S) && prevState.IsKeyDown(Keys.S))
            {
                onOff = !onOff;
            }
            prevState = Keyboard.GetState();
            #endregion

            if (onOff)
            {
                MouseState mouseState = Mouse.GetState();
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y);
                    if((mouseLocation.Y >= positionLine.Y - textureCircle.Height/2 && mouseLocation.Y <= positionLine.Y + textureCircle.Height / 2 + textureLine.Height)&&
                        (mouseLocation.X >= positionLine.X && mouseLocation.X <= positionLine.X + textureLine.Width - textureCircle.Width / 2))
                    {
                        positionCircle.X = mouseLocation.X - textureCircle.Width / 2;
                        value = (positionCircle.X + textureCircle.Width/2 - positionLine.X ) / (positionLine.X + textureLine.Width - positionLine.X);
                        value = minValue * (1 - value) + maxValue * value; 
                        wind.speed.X = value;
                    }
                }
            }
        }
        public void LoadContent(ContentManager content)
        {
            textureCircle = content.Load<Texture2D>("sliderCircle");
            textureLine = content.Load<Texture2D>("sliderLine");
            positionCircle = new Vector2(positionLine.X - textureCircle.Width / 2, positionLine.Y - textureCircle.Height / 2 + 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (onOff)
            {
                spriteBatch.Draw(textureLine, positionLine, Color.White);
                spriteBatch.Draw(textureCircle, positionCircle, Color.White);
            }

        }
    }
}