using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using YaminGame.Utilities;



namespace YaminGame.Sprites
{
    class Ground : DrawableGameComponent
    {
        Texture2D groundTexture;
        Texture2D foregroundTexture;
        //GraphicsDeviceManager graphics;
        Color[,] foregroundColorArray;
        int[] terrainContour;
        int screenWidth;
        int screenHeight;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        Microsoft.Xna.Framework.Game game;


        public Ground(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight)
            : base(game)
        {
            this.game = game;

            device = game.GraphicsDevice;
            groundTexture = game.Content.Load<Texture2D>("ground");
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));



            GenerateTerrainContour();
            CreateForeground();
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


        private void CreateForeground()
        {
            Color[,] groundColors = TextureTo2DArray(groundTexture);
            Color[] foregroundColors = new Color[screenWidth * screenHeight];

            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    if (y > terrainContour[x])
                        foregroundColors[x + y * screenWidth] = groundColors[x % groundTexture.Width, y % groundTexture.Height];
                    else
                        foregroundColors[x + y * screenWidth] = Color.Transparent;
                }
            }

            foregroundTexture = new Texture2D(device, screenWidth, screenHeight, false, SurfaceFormat.Color);
            foregroundTexture.SetData(foregroundColors);

            foregroundColorArray = TextureTo2DArray(foregroundTexture);
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        //public Vector2 CheckTerrainCollision()
        //{
        //    Matrix rocketMat = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(rocketAngle) * Matrix.CreateScale(rocketScaling) * Matrix.CreateTranslation(rocketPosition.X, rocketPosition.Y, 0);
        //    Matrix terrainMat = Matrix.Identity;
        //    Vector2 terrainCollisionPoint = TexturesCollide(rocketColorArray, rocketMat, foregroundColorArray, terrainMat);
        //    return terrainCollisionPoint;
        //}
        private Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        //private void FlattenTerrainBelowPlayers()
        //{
        //    foreach (PlayerData player in game.players)
        //        if (player.IsAlive)
        //            for (int x = 0; x < 40; x++)
        //                terrainContour[(int)player.Position.X + x] = terrainContour[(int)player.Position.X];
        //}


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
