using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics_engine.Classes;
using System.Collections.Generic;
using Physics_engine.Classes.Components;
using Microsoft.Xna.Framework.Media;

namespace Physics__engine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Ground ground = new Ground();
        private int screenWidth = 1920;
        private int screenHeight = 1080;
        private MouseState prevMouseState = Mouse.GetState();
        private MouseState mouseState = Mouse.GetState();
        private KeyboardState keyboardState = Keyboard.GetState();
        private KeyboardState prevKeyboardState = Keyboard.GetState();
        private Slider slider;
        private Wind wind;
        private Label toolBox;
        private List<Label> tools;
        public static GameModes mode = GameModes.GameMenu;
        public static GameMenu gameMenu;
        private static AboutMenu aboutMenu;
        private Label lbl;
        Song music;

        private List<Magnet> magnets;
        private List<Circles> circles;

        public int i;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            circles = new List<Circles>();
            slider = new Slider(new Vector2(300, 300), 500);
            wind = new Wind();
            toolBox = new Label("Press T to open toolbox", Vector2.One, Color.Black);
            tools = new List<Label>();
            tools.Add(new Label("Slider - s", Vector2.One, Color.Black));
            tools.Add(new Label("Magnet - m", Vector2.One, Color.Black));
            foreach (var tool in tools)
            {
                tool.IsAlive = false;
            }
            gameMenu = new GameMenu();
            aboutMenu = new AboutMenu();
            lbl = new Label("ESC - menu", new Vector2(10, 10), Color.White);
            magnets = new List<Magnet>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            slider.LoadContent(Content);
            ground.LoadContent(Content);
            toolBox.LoadContent(Content, "GameFont");
            foreach (var tool in tools)
            {
                tool.LoadContent(Content, "GameFont");
                tool.Position = new Vector2(1920 - (tool.Width + 30), tools.IndexOf(tool) * 50);
            }
            
            gameMenu.LoadContent(Content);
            aboutMenu.LoadContent(Content);
            lbl.LoadContent(Content, "GameFont");
            toolBox.Position = new Vector2(1920 - (toolBox.Width + 30), 10);
            music = Content.Load<Song>("The Sandbox Game music Official - Sandbox music (1)");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {

            if (MediaPlayer.State == MediaState.Stopped) MediaPlayer.Play(music);
            else if (MediaPlayer.State == MediaState.Paused) MediaPlayer.Resume();
            // TODO: Add your update logic here
            switch (mode)
            {
                case GameModes.GameMenu:
                    gameMenu.Update();
                    break;
                case GameModes.GamePlay:
                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {
                        toolBox.IsAlive = false;
                        foreach (var tool in tools)
                        {
                            tool.IsAlive = true;
                        }

                    }
                    else if (Keyboard.GetState().IsKeyUp(Keys.T))
                    {
                        foreach (var tool in tools)
                        {
                            tool.IsAlive = false;
                        }
                        
                    }

                    slider.Update(wind);
                    ground.Update();
                    circlesUpdate(gameTime);
                    magnetsUpdate(gameTime);
                    break;
                case GameModes.GameExit:
                    Exit();
                    break;
                case GameModes.GameAbout:
                    aboutMenu.Update(gameTime);
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mode = GameModes.GameMenu;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (mode)
            {
                case GameModes.GameMenu:
                    gameMenu.Draw(_spriteBatch);
                    break;
                case GameModes.GamePlay:
                    foreach (Circles c in circles)
                    {
                        c.Draw(_spriteBatch);
                    }
                    slider.Draw(_spriteBatch);
                    ground.Draw(_spriteBatch);
                    toolBox.Draw(_spriteBatch);
                    foreach (var tool in tools)
                    {
                        tool.Draw(_spriteBatch);
                    }
                    foreach(var magnet in magnets)
                    {
                        magnet.Draw(_spriteBatch);
                    }
                    break;
                case GameModes.GameAbout:
                    aboutMenu.Draw(_spriteBatch);
                    break;
            }
            if(mode != GameModes.GameMenu && mode != GameModes.GameExit)
            {
                lbl.Draw(_spriteBatch);
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void circlesUpdate(GameTime gameTime)
        {
            #region Spawn
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                circles.Add(new Circles(mouseState.X, mouseState.Y));
                circles[circles.Count - 1].LoadContent(Content);
            }
            prevMouseState = mouseState;
            #endregion

            foreach (Circles c in circles)
            {
                //if (!c.Done)
                //{
                    c.Update(ground, gameTime, wind, magnets);
                //}
                
            }
        }

        public void magnetsUpdate(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.M) && prevKeyboardState.IsKeyDown(Keys.M))
            {
                if (checkIntersect(magnets))
                {
                    magnets.RemoveAt(i);
                }
                else
                {
                    magnets.Add(new Magnet(new Vector2(mouseState.X, mouseState.Y), 50f));
                    magnets[magnets.Count - 1].LoadContent(Content);
                }
                
            }
            prevKeyboardState = Keyboard.GetState();
        }

        public bool checkIntersect(List<Magnet> magnets)
        {
            foreach (Magnet magnet in magnets)
            {
                if (Mouse.GetState().X <= magnet.Position.X + magnet.Width && Mouse.GetState().X >= magnet.Position.X)
                {
                    if((Mouse.GetState().Y <= magnet.Position.Y + magnet.Height && Mouse.GetState().Y >= magnet.Position.Y)){
                        i = magnets.IndexOf(magnet);
                        return true;
                    }
                }
            }

            return false;
        }

    }
}