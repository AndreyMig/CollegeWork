using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using YaminGame.Utilities;

namespace Game.Scenes
{
    internal class Scene2 : Scene1
    {
        private float timeElapsed, timeToUpdate = 0.2f;

        public Scene2(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight)
            : base(game, screenWidth, screenHeight)
        {
            //this.game = game;
            //this.screenHeight = screenHeight;
            //this.screenWidth = screenWidth;
            //back = game.Content.Load<Texture2D>("hockey_ice");
            //spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            //soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            //font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            //ballSprite = new BallSprite(game);
            _initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //timeElapsed += (float)
            //    gameTime.ElapsedGameTime.TotalSeconds;
            //Debug.WriteLine("time: " + timeElapsed);
            //if (ballSprite.ballRect.Intersects(WallSprite.wallRect) && !WallSprite.IsIntersects)
            //{
            //    WallSprite.IsIntersects = true;
            //    MediaPlayer.Play(soundCenter.Whip);
            //    ballSprite.ballSpeed.Y *= -1;
            //}
            //if (WallSprite.IsIntersects && !ballSprite.ballRect.Intersects(WallSprite.wallRect))
            //{
            //    WallSprite.IsIntersects = false;
            //}
            base.Update(gameTime);
        }

        private void _initialize()
        {
            timeElapsed = 0;
            State = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            //var screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            //spriteBatch.Draw(back, screenRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
