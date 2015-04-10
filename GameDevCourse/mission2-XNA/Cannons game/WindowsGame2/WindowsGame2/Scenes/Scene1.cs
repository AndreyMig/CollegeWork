using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame2;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using YaminGame.Utilities;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Scenes
{
    public class Scene1 : Scene
    {
        protected Microsoft.Xna.Framework.Game game;
        protected SpriteBatch spriteBatch;
        //protected BallSprite ballSprite;
        Ground bg;
        //Bird bird;
        protected SoundCenter soundCenter;
        protected SpriteFont font;
        protected int screenWidth, screenHeight;
        protected Texture2D back;
        public Rectangle goal1Rect, goal2Rect;
        public int P1Score = 0, P2Score = 0;
        private bool endScene;
        private string endText = "";
        private float timeElapsed, timeToUpdate = 1f;
        private Explosion _explosion;
        private Vector2 textSize;
        private Rocket _rocket;
        //List<int> vf = new List<int>();
         //List<Bird> birds = new List<Bird>();
        //Bird[] birds;
        Flock flock;
        private float playerExplosionSize = 80.0f, playerExplosionMaxAge = 2000.0f, terrainExplosionSize = 30.0f, terrainExplosionMaxAge = 1000.0f;
        private int playerExplosionParticles = 10, terrainExplosionParticles = 4;
        private int sceneMum = 1;
        private const int NUM_OF_PLAYERS = 2;
        int currentPlayer = 0;
        public override int State { get; set; }
        private Player[] players = new Player[NUM_OF_PLAYERS];

        public Scene1(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight, int scene): base(game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.sceneMum = scene;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            soundCenter = (SoundCenter)game.Services.GetService(typeof(SoundCenter));
            font = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            
            Initialize();
        }

        public override void Initialize()
        {

            bg = new Ground(this.game, this.screenWidth, this.screenHeight, this.sceneMum);

           
                
            for (int i = 0; i < NUM_OF_PLAYERS; i++)
            {
                players[i] = new Player(game, i);
                players[i].Position = new Vector2();
                players[i].Position.X = this.screenWidth / (NUM_OF_PLAYERS + 1) * (i + 1);
                int[] terrainContour = bg.getTerrainContour();
                players[i].Position.Y = bg.terrainContour[(int)players[i].Position.X];
            }
            //for (int i = 0; i < NUM_OF_PLAYERS; i++)
            //{
               
            //}
            bg.setupGround(players);
            //bg.FlattenTerrainBelowPlayers(players);
            

            

            _rocket = new Rocket(game, this.screenWidth, this.screenHeight);
            _explosion = new Explosion(game);
            flock = new Flock(game, this.screenWidth, this.screenHeight);
            //generateFlock();
            //bird = new Bird(this.game, this.screenWidth, this.screenHeight);
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

            if (players.Length == 1)
                _showEndScene();

            // for managing the scenes
            if (endScene)
            {
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsed > timeToUpdate)
                    State = 1;
                return;
            }

            //birds update
            flock.update(gameTime);
            
            if ((!Rocket.rocketFlying))
                ProcessKeyboard();

            if (Rocket.rocketFlying)
            {
                _rocket.Update(gameTime);
                CheckCollisions(gameTime);
            }

            if (_explosion.particleList.Count > 0)
                UpdateParticles(gameTime);

            base.Update(gameTime);
        }
        private void UpdateParticles(GameTime gameTime)
        {
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            for (int i = _explosion.particleList.Count - 1; i >= 0; i--)
            {
                Particle particle = _explosion.particleList[i];
                float timeAlive = now - particle.BirthTime;

                if (timeAlive > particle.MaxAge)
                {
                    _explosion.particleList.RemoveAt(i);
                }
                else
                {
                    float relAge = timeAlive / particle.MaxAge;
                    particle.Position = 0.5f * particle.Accelaration * relAge * relAge + particle.Direction * relAge + particle.OrginalPosition;

                    float invAge = 1.0f - relAge;
                    particle.ModColor = new Color(new Vector4(invAge, invAge, invAge, invAge));

                    Vector2 positionFromCenter = particle.Position - particle.OrginalPosition;
                    float distance = positionFromCenter.Length();
                    particle.Scaling = (50.0f + distance) / 200.0f;

                    _explosion.particleList[i] = particle;
                }
            }
        }
        private Vector2 checkBirdsCollision()
        {
            return flock.checkBirdsCollision(_rocket);
        }
        private void CheckCollisions(GameTime gameTime)
        {
            Vector2 terrainCollisionPoint = bg.CheckTerrainCollision(_rocket);
            Vector2 playerCollisionPoint = Toolbox.CheckPlayersCollision(this.players, this.currentPlayer, _rocket);
            Vector2 birdCollisionPoint = checkBirdsCollision();
           
            bool rocketOutOfScreen = CheckOutOfScreen();

            if (playerCollisionPoint.X > -1)
            {
                Rocket.rocketFlying = false;

                _rocket.smokeList = new List<Vector2>();
                AddExplosion(playerCollisionPoint, 10, 80.0f, 2000.0f, gameTime);
                
                NextPlayer();
            }

            if (terrainCollisionPoint.X > -1)
            {
                Rocket.rocketFlying = false;

                _rocket.smokeList = new List<Vector2>();
                AddExplosion(terrainCollisionPoint, 4, 30.0f, 1000.0f, gameTime);
                soundCenter.HitTerrain.Play();
                NextPlayer();
            }

            if (birdCollisionPoint.X > -1)
            {
                Rocket.rocketFlying = false;
                Console.WriteLine("bird collision");
                AddExplosion(birdCollisionPoint, 4, 30.0f, 1000.0f, gameTime);
                soundCenter.HitTerrain.Play();
                NextPlayer();
            }

            if (rocketOutOfScreen)
            {
                Rocket.rocketFlying = false;
                _rocket.smokeList = new List<Vector2>();
                NextPlayer();
            }
        }



        private void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime)
        {
            Random randomizer = new Random();
            for (int i = 0; i < numberOfParticles; i++)
                AddExplosionParticle(explosionPos, size, maxAge, gameTime, i);

            float rotation = (float)randomizer.Next(10);
            Matrix mat = Matrix.CreateTranslation(-_explosion.explosionTexture.Width / 2, -_explosion.explosionTexture.Height / 2, 0) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(size / (float)_explosion.explosionTexture.Width * 2.0f) * Matrix.CreateTranslation(explosionPos.X, explosionPos.Y, 0);
            bg.AddCrater(_explosion.explosionColorArray, mat);

            for (int i = 0; i < players.Length; i++)
                players[i].Position.Y = bg.terrainContour[(int)players[i].Position.X];
            bg.FlattenTerrainBelowPlayers(players);
            bg.CreateForeground();
        }

        private void AddExplosionParticle(Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime, int rotation)
        {
            Particle particle = new Particle(this.game, explosionPos, explosionSize, maxAge, rotation, gameTime);
            Random randomizer = new Random();
            particle.OrginalPosition = explosionPos;
            particle.Position = particle.OrginalPosition;

            particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            particle.MaxAge = maxAge;
            particle.Scaling = 0.25f;
            particle.ModColor = Color.White;

            float particleDistance = (float)randomizer.NextDouble() * explosionSize;
            Vector2 displacement = new Vector2(particleDistance, 0);
            float angle = MathHelper.ToRadians(randomizer.Next(360));
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

            particle.Direction = displacement * 2.0f;
            particle.Accelaration = -particle.Direction;

            _explosion.particleList.Add(particle);
        }



        private bool CheckOutOfScreen()
        {
            bool rocketOutOfScreen = _rocket.rocketPosition.Y > this.screenHeight;
            rocketOutOfScreen |= _rocket.rocketPosition.X < 0;
            rocketOutOfScreen |= _rocket.rocketPosition.X > this.screenWidth;
            Console.WriteLine("rocketOutOfScreen" + rocketOutOfScreen);
            return rocketOutOfScreen;
        }

        private void NextPlayer()
        {
            // + set the color of the rocket
            int endGameCounter = 0;
            currentPlayer = currentPlayer + 1;
            currentPlayer = currentPlayer % NUM_OF_PLAYERS;
            while (!players[currentPlayer].IsAlive)
            {
                currentPlayer = ++currentPlayer % NUM_OF_PLAYERS;
                endGameCounter++;
            }
            if (endGameCounter >= NUM_OF_PLAYERS - 1)
                _showEndScene();
             
        }

        private void restartGame(){

            Initialize();

        }

        private void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();

          
            if (keybState.IsKeyDown(Keys.Left))
            {
                players[currentPlayer].Angle -= 0.01f;
            }
            
            if (keybState.IsKeyDown(Keys.Right))
                players[currentPlayer].Angle += 0.01f;

            if (players[currentPlayer].Angle > MathHelper.PiOver2)
                players[currentPlayer].Angle = -MathHelper.PiOver2;
            if (players[currentPlayer].Angle < -MathHelper.PiOver2)
                players[currentPlayer].Angle = MathHelper.PiOver2;

            if (keybState.IsKeyDown(Keys.Down))
                players[currentPlayer].Power -= 1;
            if (keybState.IsKeyDown(Keys.Up))
                players[currentPlayer].Power += 1;
            if (keybState.IsKeyDown(Keys.PageDown))
                players[currentPlayer].Power -= 20;
            if (keybState.IsKeyDown(Keys.PageUp))
                players[currentPlayer].Power += 20;

            if (players[currentPlayer].Power > 1000)
                players[currentPlayer].Power = 1000;
            if (players[currentPlayer].Power < 0)
                players[currentPlayer].Power = 0;

            if (keybState.IsKeyDown(Keys.Space))
            {
                Rocket.rocketFlying = true;
                soundCenter.Launch.Play();

                _rocket.rocketPosition = players[currentPlayer].Position;
                _rocket.rocketPosition.X += 20;
                _rocket.rocketPosition.Y -= 10;
                _rocket.rocketAngle = players[currentPlayer].Angle;
                Vector2 up = new Vector2(0, -1);
                Matrix rotMatrix = Matrix.CreateRotationZ(_rocket.rocketAngle);
                _rocket.rocketDirection = Vector2.Transform(up, rotMatrix);
                _rocket.rocketDirection *= players[currentPlayer].Power / 50.0f;
            }
        }


        private void _showEndScene()
        {
            endScene = true;
            endText = players[currentPlayer].name+ " wins!";
            textSize = font.MeasureString(endText)/2;
        }

     
        public override void Draw(GameTime gameTime)
        {

            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Game1.globalTransformation);
            bg.Draw(gameTime);

            flock.draw(gameTime);
            foreach(Player p in players)
            {
                p.Draw(gameTime);
            }
            DrawText();
            if (Rocket.rocketFlying)
                 _rocket.Draw(gameTime);
            //DrawSmoke();
            _explosion.Draw(gameTime);
            
            //spriteBatch.End();

            base.Draw(gameTime);

        }

       

        private void DrawText()
        {
            
            Player player = players[currentPlayer];
            
            int currentAngle = (int)MathHelper.ToDegrees(player.Angle);
            //Console.WriteLine(currentAngle);
            spriteBatch.DrawString(font, "Cannon angle: " + currentAngle.ToString(), new Vector2(20, 20), player.Color);
            spriteBatch.DrawString(font, "Cannon power: " + player.Power.ToString(), new Vector2(20, 45), player.Color);
            spriteBatch.DrawString(font, endText, new Vector2(50, 150), player.Color);
        }
    }
}
