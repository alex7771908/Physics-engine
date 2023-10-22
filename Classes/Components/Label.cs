using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Physics_engine.Classes.Components
{
    public class Label
    {
        private SpriteFont spriteFont;
        private Vector2 position;
        private Color color;
        private string text;

        private double timeTotalSeconds = 0;
        private double duration = 0.5;
        private bool isAlive = true;

        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }

        public float Width
        {
            get { return spriteFont.MeasureString(text).X; }
        }

        public float Height
        {
            get { return spriteFont.MeasureString(text).Y; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public string Text
        {
            set { text = value; }
        }

        public Color Color
        {
            set { color = value; }
        }

        public Label(string text, Vector2 position, Color color)
        {
            spriteFont = null;
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public Label(string text, Vector2 position, Color color, SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public void LoadContent(ContentManager content, string sprite)
        {
            spriteFont = content.Load<SpriteFont>(sprite);
        }

        public void Update(GameTime gameTime)
        {
            timeTotalSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeTotalSeconds > duration)
            {
                isAlive = false;
                timeTotalSeconds = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.DrawString(spriteFont, text, position, color);
            }
            
        }

    }
}
