using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using WindowsGame2;
using Game.Scenes;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Utilities;

namespace Game.objects
{
    internal class Rocket : DrawableGameComponent
    {
        private Microsoft.Xna.Framework.Game game;
        private SpriteBatch spriteBatch;
        private SoundCenter soundCenter;

        public Color Color { get; set; }

        Texture2D rocketTexture;
        Texture2D smokeTexture;
        public static bool rocketFlying = false;
        public Vector2 rocketPosition;
        public Vector2 rocketDirection;
        public float rocketAngle;
        public float rocketScaling = 0.1f;
        public List<Vector2> smokeList = new List<Vector2>();
        Random randomizer = new Random();
        public Color[,] rocketColorArray;
        int screenHeight;
        int screenWidth;

        public Rocket(Microsoft.Xna.Framework.Game game, int height, int width)
            : base(game)
        {
            this.game = game;
            this.screenHeight = height;
            this.screenWidth = width;
            rocketTexture = game.Content.Load<Texture2D>("rocket");
            smokeTexture = game.Content.Load<Texture2D>("smoke");
            rocketColorArray = Toolbox.TextureTo2DArray(rocketTexture);
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("update rocket");
            if (rocketFlying)
            {
                Vector2 gravity = new Vector2(0, 1);
                rocketDirection += gravity / 10.0f;
                rocketPosition += rocketDirection;
                rocketAngle = (float)Math.Atan2(rocketDirection.X, -rocketDirection.Y);

                for (int i = 0; i < 5; i++)
                {
                    Vector2 smokePos = rocketPosition;
                    smokePos.X += randomizer.Next(10) - 5;
                    smokePos.Y += randomizer.Next(10) - 5;
                    smokeList.Add(smokePos);
                }

                if (CheckOutOfScreen())
                {
                    rocketFlying = false;
                    smokeList = new List<Vector2>();
                }
            }
            base.Update(gameTime);
        }

        public bool CheckOutOfScreen()
        {
            bool rocketOutOfScreen = rocketPosition.Y > this.screenHeight;
            rocketOutOfScreen |= rocketPosition.X < 0;
            rocketOutOfScreen |= rocketPosition.X > this.screenWidth;

            return rocketOutOfScreen;
        }



        public Vector2 CheckPlayersCollision(Player carriage)
        {
            Matrix rocketMat = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(rocketAngle) * Matrix.CreateScale(rocketScaling) * Matrix.CreateTranslation(rocketPosition.X, rocketPosition.Y, 0);
            int xPos = (int)carriage.Position.X;
            int yPos = (int)carriage.Position.Y;

            Matrix carriageMat = Matrix.CreateTranslation(0, -carriage.carriageTexture.Height, 0) * Matrix.CreateScale(carriage.playerScaling) * Matrix.CreateTranslation(xPos, yPos, 0);
            Vector2 carriageCollisionPoint = Toolbox.TexturesCollide(carriage.carriageColorArray, carriageMat, rocketColorArray, rocketMat);

            if (carriageCollisionPoint.X > -1)
            {
                rocketHit();
                carriage.IsAlive = false;
                return carriageCollisionPoint;
            }

            Matrix cannonMat = Matrix.CreateTranslation(-11, -50, 0) * Matrix.CreateRotationZ(carriage.Angle) * Matrix.CreateScale(carriage.playerScaling) * Matrix.CreateTranslation(xPos + 20, yPos - 10, 0);
            Vector2 cannonCollisionPoint = Toolbox.TexturesCollide(carriage.cannonColorArray, cannonMat, rocketColorArray, rocketMat);
            if (cannonCollisionPoint.X > -1)
            {
                rocketHit();
                carriage.IsAlive = false;
                return cannonCollisionPoint;
            }

            return new Vector2(-1, -1);
        }

        private void rocketHit()
        {
            rocketFlying = false;
            smokeList = new List<Vector2>();
        }




        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Game1.globalTransformation);
            if (Rocket.rocketFlying)
            {
                Console.WriteLine("rocket is flying");
                spriteBatch.Draw(rocketTexture, rocketPosition, null, Color.Black, rocketAngle, new Vector2(42, 240), 0.1f, SpriteEffects.None, 1);
            }
                
            foreach (Vector2 smokePos in smokeList)
                spriteBatch.Draw(smokeTexture, smokePos, null, Color.White, 0, new Vector2(40, 35), 0.2f, SpriteEffects.None, 1);

            //spriteBatch.End();
            base.Draw(gameTime);
        }








    }
}