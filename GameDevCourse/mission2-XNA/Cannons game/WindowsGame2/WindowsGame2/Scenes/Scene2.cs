using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Game.Utilities;
using System.Timers;

namespace Game.Scenes
{
    internal class Scene2 : Scene1
    {
        private float timeElapsed, timeToUpdate = 0.2f;
        // Create an instance of Texture2D that will
        // contain the background texture.
        Texture2D background;

        // Create a Rectangle that will define
        // the limits for the main game screen.
        Rectangle mainFrame;

        public Scene2(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight)
            : base(game, screenWidth, screenHeight, 2)
        {
            background = game.Content.Load<Texture2D>("hell");
            mainFrame = new Rectangle(0, 0, screenWidth, screenHeight);

            _initialize();
        }

        public override void Update(GameTime gameTime)
        {
 
            base.Update(gameTime);
        }

        private void _initialize()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
            timeElapsed = 0;
            State = 0;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
            base.AddRandomExplosion();
        }

        public override void Draw(GameTime gameTime)
        {
            //var screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            //spriteBatch.Draw(back, screenRectangle, Color.White);
            spriteBatch.Draw(background, mainFrame, Color.White);
            base.Draw(gameTime);
        }
    }
}
