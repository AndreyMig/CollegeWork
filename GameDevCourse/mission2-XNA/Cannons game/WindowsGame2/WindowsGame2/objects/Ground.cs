using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Utilities;
using Game.objects;



namespace Game.objects
{
    class Ground : DrawableGameComponent
    {
        Texture2D groundTexture;
        Texture2D snowTexture;
        Texture2D foregroundTexture;

       

        //GraphicsDeviceManager graphics;
        //Color[,] foregroundColorArray;
        public int[] terrainContour { get; set; }
        int screenWidth;
        int screenHeight;
        int SNOW_LIM = 30;
        int sceneNum;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        Microsoft.Xna.Framework.Game game;

        public Color[,] foregroundColorArray { get; set; }
        public Ground(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight, int sceneNum)
            : base(game)
        {
            this.game = game;

            Console.WriteLine("scene num = " + sceneNum);

            device = game.GraphicsDevice;
            if(sceneNum == 1)
            {
                groundTexture = game.Content.Load<Texture2D>("ground");
                snowTexture = game.Content.Load<Texture2D>("snow-texture");

            }
            if (sceneNum == 2)
            {
                groundTexture = game.Content.Load<Texture2D>("lava");
                snowTexture = game.Content.Load<Texture2D>("lava");
            }


            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));


            GenerateTerrainContour();
           
           
        }

        public void setupGround(Player[] players){
            
            FlattenTerrainBelowPlayers(players);
            CreateForeground();
        }

        public int[] getTerrainContour()
        {
            return terrainContour;
        }

        private void GenerateTerrainContour()
        {
            Random randomizer = new Random();
            terrainContour = new int[screenWidth];

            double rand1 = randomizer.NextDouble() + 1;
            double rand2 = randomizer.NextDouble() + 2;
            double rand3 = randomizer.NextDouble() + 3;

            float offset = screenHeight / 2;
            float peakheight = 100;
            float flatness = 70;

            for (int x = 0; x < screenWidth; x++)
            {
                double height = peakheight / rand1 * Math.Sin((float)x / flatness * rand1 + rand1);
                height += peakheight / rand2 * Math.Sin((float)x / flatness * rand2 + rand2);
                height += peakheight / rand3 * Math.Sin((float)x / flatness * rand3 + rand3);
                height += offset;
                terrainContour[x] = (int)height;
            }
        }


        public void AddCrater(Color[,] tex, Matrix mat)
        {
            int width = tex.GetLength(0);
            int height = tex.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (tex[x, y].R > 10)
                    {
                        Vector2 imagePos = new Vector2(x, y);
                        Vector2 screenPos = Vector2.Transform(imagePos, mat);

                        int screenX = (int)screenPos.X;
                        int screenY = (int)screenPos.Y;

                        if ((screenX) > 0 && (screenX < this.screenWidth))
                            if (terrainContour[screenX] < screenY)
                                terrainContour[screenX] = screenY;
                    }
                }
            }
        }

        public void reMovePlayers(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
                players[i].Position.Y = terrainContour[(int)players[i].Position.X];
        }

        public void CreateForeground()
        {
            Color[,] groundColors = Toolbox.TextureTo2DArray(groundTexture);
            Color[,] snowColors = Toolbox.TextureTo2DArray(snowTexture);
            Color[] foregroundColors = new Color[screenWidth * screenHeight];
            Random randomizer = new Random();

            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    if (y > terrainContour[x])
                    {
                        int someRandom = randomizer.Next(150);
                        if (y >= terrainContour[x]+someRandom)
                            foregroundColors[x + y * screenWidth] = groundColors[x % groundTexture.Width, y % groundTexture.Height];
                        else
                            foregroundColors[x + y * screenWidth] = snowColors[x % snowTexture.Width, y % snowTexture.Height];
                    }
                        
                    else
                        foregroundColors[x + y * screenWidth] = Color.Transparent;
                }
            }

            foregroundTexture = new Texture2D(device, screenWidth, screenHeight, false, SurfaceFormat.Color);
            foregroundTexture.SetData(foregroundColors);

            foregroundColorArray = Toolbox.TextureTo2DArray(foregroundTexture);
        }



        public Vector2 CheckTerrainCollision(Rocket r)
        {
            Matrix rocketMat = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(r.rocketAngle) * Matrix.CreateScale(r.rocketScaling) * Matrix.CreateTranslation(r.rocketPosition.X, r.rocketPosition.Y, 0);
            Matrix terrainMat = Matrix.Identity;
            Vector2 terrainCollisionPoint = Toolbox.TexturesCollide(r.rocketColorArray, rocketMat, foregroundColorArray, terrainMat);
            return terrainCollisionPoint;
        }
      

        public void FlattenTerrainBelowPlayers(Player[] players)
        {
            foreach (Player player in players)
                if (player.IsAlive)
                    for (int x = 0; x < 40; x++)
                        terrainContour[(int)player.Position.X + x] = terrainContour[(int)player.Position.X];
        }


        public override void Draw(GameTime gameTime)
        {
            //spriteBacth.Draw(ballSprite, ballPosition, Rectangles[FrameIndex],
            //    ballColor, Rotation, Origin, Scale, SpriteEffect, 0f);
            DrawScenery();
            base.Draw(gameTime);
        }



        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            //spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            spriteBatch.Draw(foregroundTexture, screenRectangle, Color.White);
        }




    }
}
