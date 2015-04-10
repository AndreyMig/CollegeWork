//====================
using Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YaminGame.Scenes;
using YaminGame.Utilities;

//====================
namespace WindowsGame2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene0 scene0;
        Scene1 scene1;
        Scene2 scene2;
        SoundCenter soundCenter;
        SpriteFont font, bigFont;
        private Texture2D back;
        private int screenWidth, screenHeight;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = screenHeight = 675,
                PreferredBackBufferWidth = screenWidth = 800
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent()    {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundCenter = new SoundCenter(this);
            font = Content.Load<SpriteFont>("MyFont");
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(SoundCenter), soundCenter);
            Services.AddService(typeof(SpriteFont), font);
            scene0 = new Scene0(this);
            scene1 = new Scene1(this, screenWidth, screenHeight);
            scene2 = new Scene2(this, screenWidth, screenHeight);
            scene2.Hide();
            scene1.Hide();
            scene0.Show();
            Components.Add(scene0);
            Components.Add(scene1);
            Components.Add(scene2);
        }
        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
           
            switch (scene0.State + scene1.State + scene2.State)
            {
                case 0:
                    scene0.Show();
                    scene1.Hide();
                    scene2.Hide();
                    break;
                case 1:
                    scene0.Hide();
                    scene1.Show();
                    scene2.Hide();
                    break;
                case 2:
                    scene0.Hide();
                    scene1.Hide();
                    scene2.Show();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
