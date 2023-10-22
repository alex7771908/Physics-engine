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
    public class GameMenu
    {
        private int selected;
        private List<Label> buttons;
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;
        private int screenWidth = 1920;
        private int screenHeight = 1080;
        private Label name;
        public ContentManager content;

        public GameMenu()
        {
            selected = 0;
            buttons = new List<Label>();
            buttons.Add(new Label("Play", new Vector2(100, screenHeight/2), Color.Black));
            buttons.Add(new Label("Exit", new Vector2(500, screenHeight / 2), Color.Black));
            buttons.Add(new Label("Tutorial", new Vector2(900, screenHeight / 2), Color.Black));
            buttons.Add(new Label("About", new Vector2(1300, screenHeight / 2), Color.Black));
            name = new Label("Newton's Sandbox", new Vector2(0, 0), Color.Black);
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            foreach(var button in buttons)
            {
                button.LoadContent(content, "GameFont");
                
            }
            name.LoadContent(content, "MontserratBold");

            for(int i = 0; i < buttons.Count; i++)
            {
                float length = buttons[i].Width;
                int segment = 1920 / 4;
                buttons[i].Position = new Vector2((segment - length) / 2 + segment * i, screenHeight / 2);
            }

            name.Position = new Vector2((1920 - name.Width) /2, 100);
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            if ((prevKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.Right)) || prevKeyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.D))
            {
                if(selected < buttons.Count - 1)
                {
                    selected++;
                }
                
            }
            if ((prevKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Left)) || prevKeyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A))
            {
                if (selected > 0)
                {
                    selected--;
                }

            }

            if (prevKeyboardState.IsKeyDown(Keys.Enter) && keyboardState.IsKeyUp(Keys.Enter))
            {
                if (selected == 0)
                {
                    Game1.mode = GameModes.GamePlay;
                } else if (selected == 1)
                {
                    Game1.mode = GameModes.GameExit;
                } else if (selected == 2)
                {
                    Game1.mode = GameModes.GameTutorial;
                 } else if(selected == 3)
                {
                    Game1.mode = GameModes.GameAbout;
                }

            }

            prevKeyboardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                Color color;
                if (selected == i)
                {
                    color = Color.Black;
                    buttons[i].LoadContent(content, "MontserratRegular");
                }
                else
                {
                    color = Color.White;
                    buttons[i].LoadContent(content, "GameFont");
                }

                buttons[i].Color = color;
                buttons[i].Draw(spriteBatch);
            }
            name.Draw(spriteBatch);
        }
    }
}
