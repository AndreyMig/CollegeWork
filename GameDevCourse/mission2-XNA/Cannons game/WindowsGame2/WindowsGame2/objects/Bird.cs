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
using Game.objects;

namespace Game.objects
{
    class Bird : DrawableGameComponent  
    {

        Microsoft.Xna.Framework.Game game;
        SpriteBatch spriteBacth;
        Texture2D birdSprite;
        Vector2 birdPosition;
        protected int FrameIndex = 0;
        private int frames = 3;
        private int SPRITE_RECT_LEN = 80;
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
        public Boolean isAlive = true;
        Color[,] birdColorArray;
        //private List<Bird> currentlyFlyingBirds = new List<Bird>();

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Bird(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight, String flightDirection, int offset)
            : base(game)
        {
            this.game = game;
            this.flightDirection = flightDirection;
            //randomizeBirdFlight();
            Random randommm = new Random();
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;


            birdPosition.Y = 50;

            switch (this.flightDirection)
            {
                case LTR_STRING:

                    birdSprite = game.Content.Load<Texture2D>("dragon-ltr-sprite-sm");
                    birdPosition.X = 0 - SPRITE_RECT_LEN * (1 + offset);

                    break;
                case RTL_STRING:
                    birdSprite = game.Content.Load<Texture2D>("dragon-rtl-sprite-sm");
                    Console.WriteLine("screenwidth = " + this.screenWidth);
                    birdPosition.X = this.screenWidth + SPRITE_RECT_LEN*(1+offset);
                    break;

            }
           
            spriteBacth = (SpriteBatch) game.Services.GetService(typeof (SpriteBatch));
          

            var width = birdSprite.Width / frames;
            Rectangles = new Rectangle[frames];
            for (var i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, birdSprite.Height);
            FramesPerSecond = 10;
            InitBirdParam();
        }


        public void InitBirdParam()
        {
            
            birdColor = Color.White;
           
            birdSpeed = new Vector2(90, 80);
            var rand = new Random();
            birdSpeed.Y *= (rand.NextDouble() > 0.5) ? 1 : -1;
            birdColorArray = Toolbox.TextureTo2DArray(birdSprite);


        }

        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }


        //private void randomizeBirdFlight()
        //{
        //    Random randomizer = new Random();
        //    int someRandom = randomizer.Next(10);
           
        //    if (someRandom > 5)
        //    {
        //        this.flightDirection = LTR_STRING;
        //        this.birdPosition.X = 0 - SPRITE_RECT_LEN;
        //    }
        //    else if (someRandom <= 5)
        //    {
        //        this.flightDirection = "rtl";
        //        this.birdPosition.X = this.screenWidth + SPRITE_RECT_LEN;
        //    }
            



        //}


        private void changeBirdYSpeed()
        {
            Random randomizer = new Random();
            int someRandom = randomizer.Next(-50, 50);
            this.birdSpeed.Y = someRandom;
           
        }
        private void checkScreenLimits()
        {
            switch (this.flightDirection)
            {
                case LTR_STRING:

                    if (birdPosition.X > this.screenWidth)
                        this.isAlive = false;
                    
                    break;
                case RTL_STRING:
                    if (birdPosition.X < 0)
                        this.isAlive = false;


                    break;

            }
        }
        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine("bird Update, isAlive = " + isAlive);
            if (!isAlive)
                return;
            checkScreenLimits();
           

            changeBirdYSpeed();
            birdPosition.Y += birdSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (this.flightDirection)
            {
                case LTR_STRING:

                    birdPosition.X += birdSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //Console.WriteLine(LTR_STRING);
                    break;
                case RTL_STRING:


                    birdPosition.X -= birdSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //Console.WriteLine(RTL_STRING);
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


        public Vector2 CheckBirdCollision(Rocket r)
        {
            Matrix rocketMat = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(r.rocketAngle) * Matrix.CreateScale(r.rocketScaling) * Matrix.CreateTranslation(r.rocketPosition.X, r.rocketPosition.Y, 0);

            if (this.isAlive)
                {

                    int xPos = (int)birdPosition.X;
                    int yPos = (int)birdPosition.Y;

                    Matrix birdMat = Matrix.CreateTranslation(0, -birdSprite.Height, 0) * Matrix.CreateScale(1) * Matrix.CreateTranslation(xPos, yPos, 0);
                    Vector2 birdCollisionPoint = Toolbox.TexturesCollide(birdColorArray, birdMat, r.rocketColorArray, rocketMat);

                    if (birdCollisionPoint.X > -1)
                        {
                            this.isAlive = false;
                            return birdCollisionPoint;
                        }

                        //Matrix cannonMat = Matrix.CreateTranslation(-11, -50, 0) * Matrix.CreateRotationZ(player.Angle) * Matrix.CreateScale(playerScaling) * Matrix.CreateTranslation(xPos + 20, yPos - 10, 0);
                        //Vector2 cannonCollisionPoint = TexturesCollide(cannonColorArray, cannonMat, rocketColorArray, rocketMat);
                        //if (cannonCollisionPoint.X > -1)
                        //{
                        //    players[i].IsAlive = false;
                        //    return cannonCollisionPoint;
                        //}
                }
            
            return new Vector2(-1, -1);
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
