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

namespace YaminGame.Sprites
{
    class Bird : DrawableGameComponent  
    {

        Microsoft.Xna.Framework.Game game;
        SpriteBatch spriteBacth;
        Texture2D birdSprite;
        Vector2 birdPosition;
        protected int FrameIndex = 0;
        private int frames = 3;
        private int SPRITE_RECT_LEN = 50;
        protected Rectangle[] Rectangles;
        private float timeToUpdate = 0.05f;
        private const String LTR_STRING = "ltr";
        private const String RTL_STRING = "rtl";

        private float timeElapsed;
        public bool IsLooping = true;

        int screenWidth;
        int screenHeight; 
        public Vector2 birdSpeed;
        public Rectangle birdRect;
        private Color birdColor;
        private String flightDirection;
        private float Rotation = 0f;
        private float Scale = 1f;
        private SpriteEffects SpriteEffect;
        private Vector2 Origin;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Bird(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight)
            : base(game)
        {
            this.game = game;
            
            birdSprite = game.Content.Load<Texture2D>("dragon-ltr-sprite-sm");
            spriteBacth = (SpriteBatch) game.Services.GetService(typeof (SpriteBatch));
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            //_minY = Consts.GoalYline; // the sides of the game board
            //_maxX = game.GraphicsDevice.Viewport.Width - ballSprite.Width / frames;
            //_maxY = game.GraphicsDevice.Viewport.Height - ballSprite.Height - Consts.GoalYline;
            var width = birdSprite.Width / frames;
            Rectangles = new Rectangle[frames];
            for (var i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, birdSprite.Height);
            FramesPerSecond = 10;
            InitBallParam();
        }


        public void InitBallParam()
        {
            randomizeBirdFlight();
            birdColor = Color.White;
            //birdPosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2 - (birdSprite.Width / 5) / 2,
            //    game.GraphicsDevice.Viewport.Height / 2 - birdSprite.Height / 2);
            birdSpeed = new Vector2(90, 30);
            var rand = new Random();
            birdSpeed.Y *= (rand.NextDouble() > 0.5) ? 1 : -1;
        }

        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }


        private void randomizeBirdFlight()
        {
            Random randomizer = new Random();
            int someRandom = randomizer.Next(10);
           
            if (someRandom > 5)
            {
                this.flightDirection = LTR_STRING;
                this.birdPosition.X = 0 - SPRITE_RECT_LEN;
            }
            else if (someRandom <= 5)
            {
                this.flightDirection = "rtl";
                this.birdPosition.X = this.screenWidth + SPRITE_RECT_LEN;
            }
            



        }


        private void changeBirdYSpeed()
        {
            Random randomizer = new Random();
            int someRandom = randomizer.Next(-50, 50);
            this.birdSpeed.Y = someRandom;
           
        }

        public override void Update(GameTime gameTime)
        {

            changeBirdYSpeed();
            birdPosition.Y += birdSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (this.flightDirection)
            {
                case LTR_STRING:

                    birdPosition.X += birdSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    break;
                case RTL_STRING:


                    birdPosition.X -= birdSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    break;

            }

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

            birdRect = new Rectangle((int)birdPosition.X, (int)birdPosition.Y, birdSprite.Width / frames, Convert.ToInt32(birdSprite.Height));
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBacth.Draw(ballSprite, ballPosition, Color.White);
            spriteBacth.Draw(birdSprite, birdPosition, Rectangles[FrameIndex],
                birdColor, Rotation, Origin, Scale, SpriteEffect, 0f);
            base.Draw(gameTime);
        }



    }
}
