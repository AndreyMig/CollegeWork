﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Game.Utilities;

namespace Game.objects
{
    class Flock
    {

        int screenWidth;
        int screenHeight;
        Bird[] birds;
        Microsoft.Xna.Framework.Game game;
        private const String LTR_STRING = "ltr";
        private const String RTL_STRING = "rtl";
        String flightDirection;
        int sceneNum;
        public Flock(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight, int sceneNum)
        {
            this.game = game;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.sceneNum = sceneNum;
            generateFlock();
        }


        public Vector2 checkBirdsCollision(Rocket _rocket)
        {
            Vector2 birdCollisionPoint;
            for (int i = 0; i < birds.Length; i++)
            {
                birdCollisionPoint = birds[i].CheckBirdCollision(_rocket);
                if (birdCollisionPoint.X > -1)
                {
                    Console.WriteLine("checkBirdsCollision");
                    birds[i] = null;
                    birds = birds.Where(s => s != null).ToArray();
                    return birdCollisionPoint;
                }
            }
            return new Vector2(-1, -1);
        }

        private void generateFlock()
        {
            Random r = new Random();
            int num = r.Next(1,4);
            randomizeBirdFlight();
            this.birds = new Bird[num];
            Console.WriteLine("generateFlock() " + num);

            for (int i = 0; i < num; i++)
            {
                Bird b = new Bird(this.game, this.screenWidth, this.screenHeight, this.sceneNum,  this.flightDirection, i);
                this.birds[i] = b;
            }



        }


        public void update(GameTime gameTime)
        {
            //Console.WriteLine("flock update() birds.len = " + birds.Length);
            for (int i = 0; i < birds.Length; i++)
            {

                birds[i].Update(gameTime);

                if (!birds[i].isAlive)
                {
                    birds[i] = null;
                }
            }

            birds = birds.Where(s => s != null).ToArray();
            if (birds.Length == 0)
                generateFlock();
            //Console.WriteLine(birds.Length);

        }
        public void draw(GameTime gameTime)
        {
            for (int i = 0; i < birds.Length; i++)
            {
                //Console.WriteLine("flock draw() bird num  = " + i);
                birds[i].Draw(gameTime);
            }
        }

        private void randomizeBirdFlight()
        {
            Random randomizer = new Random();
            int someRandom = randomizer.Next(10);

            if (someRandom > 5)
            {
                this.flightDirection = LTR_STRING;
            }
            else if (someRandom <= 5)
            {
                this.flightDirection = RTL_STRING;
            }

        }

    }
}
