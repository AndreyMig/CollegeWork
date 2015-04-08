using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame2;
using YaminGame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
//========================

//========================
namespace Game.Sprites
{
    public class BallSprite : DrawableGameComponent    {
        Microsoft.Xna.Framework.Game game;
        SpriteBatch spriteBacth;
        Texture2D ballSprite;
        Vector2 ballPosition;
        SoundCenter soundCenter;
        public Vector2 ballSpeed;
        public Rectangle ballRect;

        private readonly int _maxY, _maxX, _minY;
        
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate = 0.05f;
        private int frames = 5;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;


        private float Rotation = 0f;
        private float Scale = 1f;
        private SpriteEffects SpriteEffect;
        private Vector2 Origin;
        private Color ballColor;


        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public BallSprite(Microsoft.Xna.Framework.Game game) : base(game)
        {
            this.game = game;
            ballSprite = game.Content.Load<Texture2D>("eballs5");
            spriteBacth = (SpriteBatch) game.Services.GetService(typeof (SpriteBatch));
            soundCenter = (SoundCenter) game.Services.GetService(typeof (SoundCenter));
            _minY = Consts.GoalYline; // the sides of the game board
            _maxX = game.GraphicsDevice.Viewport.Width - ballSprite.Width / frames;
            _maxY = game.GraphicsDevice.Viewport.Height - ballSprite.Height - Consts.GoalYline;
            var width = ballSprite.Width/frames;
            Rectangles = new Rectangle[frames];
            for (var i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i*width, 0, width, ballSprite.Height);
            FramesPerSecond = 10;
            InitBallParam();
        }

        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }

        public void InitBallParam()
        {
            ballColor = Color.White;
            ballPosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2 - (ballSprite.Width/5) / 2,
                game.GraphicsDevice.Viewport.Height / 2 - ballSprite.Height / 2);
            ballSpeed = new Vector2(150, 150);
            var rand = new Random();
            ballSpeed.Y *= (rand.NextDouble() > 0.5) ? 1 : -1;
        }

        protected override void Dispose(bool disposing)   {
            ballSprite.Dispose();
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)   {
            ballPosition += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ballPosition.X > _maxX || ballPosition.X < 0) ballSpeed.X *= -1;
            if (ballPosition.Y < _minY || ballPosition.Y > _maxY) ballSpeed.Y *= -1;
            //else if (ballPosition.Y > _maxY)  {
            //    Game1.P1Score -=1;
            //    soundCenter.Crash.Play();
            //    ballPosition.Y = 0;
            //    ballSpeed.X = 150;
            //    ballSpeed.Y = 150;
            //}
            if (ballSpeed.Y > 700)
                ballColor = Color.Red;
            else if (ballSpeed.Y > 500)
                ballColor = Color.Tomato;
            else if (ballSpeed.Y > 300)
                ballColor = Color.Violet;

            timeElapsed += (float)
                 gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;
                else if (IsLooping)
                    FrameIndex = 0;
            }

            ballRect = new Rectangle((int) ballPosition.X, (int) ballPosition.Y, ballSprite.Width/frames, Convert.ToInt32(ballSprite.Height));
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBacth.Draw(ballSprite, ballPosition, Color.White);
            spriteBacth.Draw(ballSprite, ballPosition, Rectangles[FrameIndex],
                ballColor, Rotation, Origin, Scale, SpriteEffect, 0f);
            base.Draw(gameTime);
        }
    }
}
