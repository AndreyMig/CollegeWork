using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YaminGame.Utilities;

namespace YaminGame.Sprites
{
    internal class WallSprite : DrawableGameComponent
    {
        private Microsoft.Xna.Framework.Game game;
        private SpriteBatch spriteBacth;
        private Texture2D wallSprite;
        private Vector2 wallPosition;
        private SoundCenter soundCenter;
        public Vector2 wallSpeed;
        public Rectangle wallRect;
        public bool IsIntersects;

        public WallSprite(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.game = game;
            wallSprite = game.Content.Load<Texture2D>("brick");
            spriteBacth = (SpriteBatch) game.Services.GetService(typeof (SpriteBatch));
            soundCenter = (SoundCenter) game.Services.GetService(typeof (SoundCenter));
            wallSpeed = new Vector2(100,0);
            wallPosition = new Vector2(0, game.GraphicsDevice.Viewport.Height/2 - wallSprite.Height/2);
			IsIntersects = false;
        }

        protected override void Dispose(bool disposing)
        {
            wallSprite.Dispose();
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            wallPosition += wallSpeed*(float) gameTime.ElapsedGameTime.TotalSeconds;
            var maxX = game.GraphicsDevice.Viewport.Width - wallSprite.Width;

            if (wallPosition.X > maxX || wallPosition.X < 0) wallSpeed.X *= -1;
            
            wallRect = new Rectangle((int) wallPosition.X, (int) wallPosition.Y, wallSprite.Width, wallSprite.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBacth.Draw(wallSprite, wallPosition, Color.White);
            base.Draw(gameTime);
        }



    }
}
