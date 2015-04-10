using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame2;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using YaminGame.Utilities;
using Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YaminGame.Sprites;

namespace Game.Scenes
{
    public class Scene1 : Scene
    {
        protected Microsoft.Xna.Framework.Game game;
        protected SpriteBatch spriteBatch;
        //protected BallSprite ballSprite;
        Ground bg;
        Bird bird;
        private PlayerSprite _player1Sprite, _player2Sprite;
        protected SoundCenter soundCenter;
        protected SpriteFont font;
        protected int screenWidth, screenHeight;
        protected Texture2D back;
        public Rectangle goal1Rect, goal2Rect;
        public int P1Score = 0, P2Score = 0;
        private bool endScene;
        private string endText = "";
        private float timeElapsed, timeToUpdate = 1f;
        private Vector2 textSize;

        public override int State { get; set; }

        public Scene1(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight): base(game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            //back = game.Content.Load<Texture2D>("hockey_ice");
            //spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            //soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            //font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            //ballSprite = new BallSprite(game);
            //_player1Sprite = new PlayerSprite(game, true);
            //_player2Sprite = new PlayerSprite(game, false);
            //SceneComponents.Add(ballSprite);
            //SceneComponents.Add(_player1Sprite);
            //SceneComponents.Add(_player2Sprite);
            Initialize();
        }

        public override void Initialize()
        {
            bg = new Ground(this.game, this.screenWidth, this.screenHeight);
            bird = new Bird(this.game, this.screenWidth, this.screenHeight);
            //goal1Rect = new Rectangle(125, Consts.GoalYline, 200, 1);
            //goal2Rect = new Rectangle(125, screenHeight - Consts.GoalYline - 5, 200, 1);
            //State = 0;
            //P1Score = 0;
            //P2Score = 0;
            //endScene = false;
            //ballSprite.InitBallParam();
        }



        public override void Update(GameTime gameTime)
        {
            CheckCollisions(gameTime);

            bird.Update(gameTime);

            //if (endScene)
            //{
            //    timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    if (timeElapsed > timeToUpdate)
            //        State = 1;
            //    return;
            //}
            //if (ballSprite.ballRect.Intersects(_player1Sprite.playerRect) && ballSprite.ballSpeed.Y > 0)
            //{
            //    soundCenter.Swish.Play();
            //    ballSprite.ballSpeed.Y += 50;
            //    if (ballSprite.ballSpeed.X < 0) ballSprite.ballSpeed.X -= 50;
            //    else ballSprite.ballSpeed.X += 50;
            //    ballSprite.ballSpeed.Y *= -1;
            //}

            //if (ballSprite.ballRect.Intersects(_player2Sprite.playerRect) && ballSprite.ballSpeed.Y < 0)
            //{
            //    soundCenter.Swish.Play();
            //    ballSprite.ballSpeed.Y -= 50;
            //    if (ballSprite.ballSpeed.X < 0) ballSprite.ballSpeed.X -= 50;
            //    else ballSprite.ballSpeed.X += 50;
            //    ballSprite.ballSpeed.Y *= -1;
            //}
            //if (ballSprite.ballRect.Intersects(goal1Rect))
            //{
            //    P1Score += 1;
            //    ballSprite.InitBallParam();
            //    MediaPlayer.Play(soundCenter.Beep);
            //}
            //else if (ballSprite.ballRect.Intersects(goal2Rect))
            //{
            //    P2Score += 1;
            //    MediaPlayer.Play(soundCenter.Beep);
            //    ballSprite.InitBallParam();
            //}
            //if (P2Score > 9 || P1Score > 9)
            //{
            //    _showEndScene();
            //}
            //var keyState = Keyboard.GetState(); 
            //if (keyState.IsKeyDown(Keys.Space))
            //{
            //    _showEndScene();
            //}
            base.Update(gameTime);
        }

        private void CheckCollisions(GameTime gameTime)
        {
            //Vector2 terrainCollisionPoint = CheckTerrainCollision();
            //Vector2 playerCollisionPoint = CheckPlayersCollision();
            //bool rocketOutOfScreen = CheckOutOfScreen();

            //if (playerCollisionPoint.X > -1)
            //{
            //    rocketFlying = false;

            //    smokeList = new List<Vector2>(); AddExplosion(playerCollisionPoint, 10, 80.0f, 2000.0f, gameTime);

            //    NextPlayer();
            //}

            //if (terrainCollisionPoint.X > -1)
            //{
            //    rocketFlying = false;

            //    smokeList = new List<Vector2>(); AddExplosion(terrainCollisionPoint, 4, 30.0f, 1000.0f, gameTime);

            //    NextPlayer();
            //}

            //if (rocketOutOfScreen)
            //{
            //    rocketFlying = false;

            //    smokeList = new List<Vector2>();
            //    NextPlayer();
            //}
        }



        private void _showEndScene()
        {
            endScene = true;
            if (P1Score == P2Score)
                endText = "Tie";
            else endText = (P1Score > P2Score) ? "Player 1 Wins" : "Player 2 Wins";
            textSize = font.MeasureString(endText)/2;
        }

     
        public override void Draw(GameTime gameTime)
        {
            bg.Draw(gameTime);
            bird.Draw(gameTime);
            //var screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            //spriteBatch.Draw(back, screenRectangle, Color.White);
            //spriteBatch.DrawString(font, "P1 Score:" + P1Score + "   P2 Score:" + P2Score, new Vector2(5, 5), Color.Red);   
            //base.Draw(gameTime);
            //if (endScene)
            //{
            //    spriteBatch.DrawString(font, endText, new Vector2(screenWidth / 2, screenHeight / 2), Color.Blue, 0, textSize, 2.0f, SpriteEffects.None, 0.5f);
            //}
        }
    }
}
