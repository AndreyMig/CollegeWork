using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YaminGame.Utilities;

namespace YaminGame.Scenes
{
    class Scene0 : Scene
    {
        private Microsoft.Xna.Framework.Game game;
        private SoundCenter soundCenter;
        private SpriteBatch spriteBatch;
        private SpriteFont font;



        private string GAME_HELP = "\nPlayer 1 is yoda " +
                                        "\n    he moves with the arrows, " +
                                        "\n    left arrow for left, right arrow for right. " +
                                        "\nPlayer 2 is darth vader " +
                                        "\n    he moves with the letters 'A' and 'D', " +
                                        "\n    'A' for left, 'D' for right." +
                                        "\nPress ENTER to start the game." +
                                        "\npress SPACE to jump to scence 2.";
    

        public override int State { get; set; }

        public Scene0(Microsoft.Xna.Framework.Game game) : base(game)
        {
            this.game = game;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont)); 
            _initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Right))
            {
                
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                State = 1;
            }
            base.Update(gameTime);
        }

        private void _initialize()
        {
            State = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, GAME_HELP, new Vector2(5, 5), Color.Red);
            base.Draw(gameTime);
        }
    }
}
