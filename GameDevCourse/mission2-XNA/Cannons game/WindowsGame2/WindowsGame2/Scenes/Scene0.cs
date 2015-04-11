using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Utilities;

namespace Game.Scenes
{
    class Scene0 : Scene
    {
        private Microsoft.Xna.Framework.Game game;
        private SoundCenter soundCenter;
        private SpriteBatch spriteBatch;
        private SpriteFont font;



        private string GAME_HELP = "\n " +
                                        "\nPress ENTER to start the game." +
                                        "\n  \n  \n  PRESS H TO START GAME IN HELL, ";

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
            
            if (keyState.IsKeyDown(Keys.H))
            {
                State = 2;
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
