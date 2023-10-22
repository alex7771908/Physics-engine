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
    public class AboutMenu
    {
        private List<Label> labels = new List<Label>();

        public Vector2 position = new Vector2(960, 480);
        private float yChange = 100;

        public AboutMenu()
        {
            labels.Add(new Label("Newton's sandbox", position, Color.White));
            labels.Add(new Label("Project by", position, Color.White));
            labels.Add(new Label("Creator - Sasha", position, Color.White));
            labels.Add(new Label("Coach - Ilya", position, Color.White));
            labels.Add(new Label("Moral Support - Borya", position, Color.White));
            labels.Add(new Label("Vladik", position, Color.White));
            labels.Add(new Label("Lesha", position, Color.White));
            labels.Add(new Label("BearShark", position, Color.White));
            labels.Add(new Label("Vasilevsky", position, Color.White));
            labels.Add(new Label("All the content is protected", position, Color.White));
            labels.Add(new Label("by copyright law about", position, Color.White));
            labels.Add(new Label("fair use", position, Color.White));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var label in labels)
            {
                label.Position = position;
                position.Y += yChange;
                label.LoadContent(content, "MontserratBold");
            }

        }

        public void Update(GameTime gameTime)
        {
            foreach(var label in labels)
            {
                float length = label.Width;
                label.Position = new Vector2((1920 - length) / 2, label.Position.Y - (float)(50 * gameTime.ElapsedGameTime.TotalSeconds));

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var label in labels)
            {
                label.Draw(spriteBatch);
            }
        }
    }
}
