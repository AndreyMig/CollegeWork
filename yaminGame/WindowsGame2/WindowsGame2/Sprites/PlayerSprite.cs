using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YaminGame.Utilities;

namespace Game.Sprites
{
    public class PlayerSprite : DrawableGameComponent
    {
        private bool PLAYER1 = true, PLAYER2 = false;
        private bool _player;
        Microsoft.Xna.Framework.Game game;
        SpriteBatch spriteBatch;
        Texture2D playerSprite;
        private Vector2 playerPosition;
        public Rectangle playerRect;

        public PlayerSprite(Microsoft.Xna.Framework.Game game, bool player): base(game)
        {
            this.game = game;
            _player = player;
            var avatar = "";
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            float x, y;

            avatar = (_player.Equals(PLAYER1)) ? "yoda1" : "darthvader1";
            playerSprite = game.Content.Load<Texture2D>(avatar);
            
            if (_player.Equals(PLAYER1))
            {
                x = game.GraphicsDevice.Viewport.Width/2 - playerSprite.Width/2;
                y = game.GraphicsDevice.Viewport.Height - Consts.GoalYline - playerSprite.Height;
            }
            else
            {
                x = game.GraphicsDevice.Viewport.Width / 2 - playerSprite.Width / 2;
                y = Consts.GoalYline;
            }
            playerPosition = new Vector2(x,y);
            game.IsMouseVisible = true;
        }

        protected override void Dispose(bool disposing)
        {
            playerSprite.Dispose();
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerSprite.Width, playerSprite.Height);
            var keyState = Keyboard.GetState();
            if (_player.Equals(PLAYER1))
            {
                if (keyState.IsKeyDown(Keys.Right)) playerPosition.X += 5;
                if (keyState.IsKeyDown(Keys.Left)) playerPosition.X -= 5;    
            }
            else
            {
                if (keyState.IsKeyDown(Keys.D)) playerPosition.X += 5;
                if (keyState.IsKeyDown(Keys.A)) playerPosition.X -= 5; 
            }
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(playerSprite, playerPosition, Color.White);
            base.Draw(gameTime);
        }
    }
}
