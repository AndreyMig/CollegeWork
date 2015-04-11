using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame2;
using Game.objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Utilities;
namespace Game.objects
{
    class Explosion : DrawableGameComponent
    {
        Microsoft.Xna.Framework.Game game;
        protected SpriteBatch spriteBatch;
        public Texture2D explosionTexture;
        public Color[,] explosionColorArray;
        public List<Particle> particleList = new List<Particle>();

        public Explosion(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.game = game;
            explosionTexture = game.Content.Load<Texture2D>("explosion");
            explosionColorArray = Toolbox.TextureTo2DArray(explosionTexture);
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = particleList.Count - 1; i >= 0; i--)
            {
                Particle particle = particleList[i];
                if (!particle.IsAlive)
                    particleList.RemoveAt(i);
            }
            base.Update(gameTime);
        }

        public void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                Particle particle = new Particle(game, explosionPos, size, maxAge, i, gameTime);
                particleList.Add(particle);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Game1.globalTransformation);
            //int cc = 0;
            //foreach (var particle in particleList)
            //{
            //    particle.Draw(gameTime);
            //    Console.WriteLine(cc++);
            //}
            DrawExplosion();
            spriteBatch.End();
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Game1.globalTransformation);
            base.Draw(gameTime);
        }
        private void DrawExplosion()
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                Particle particle = particleList[i];
                //Console.WriteLine("particle.Position = " + particle.Position);

                spriteBatch.Draw(explosionTexture, particle.Position, null, particle.ModColor, i, new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }
        }


    }
}