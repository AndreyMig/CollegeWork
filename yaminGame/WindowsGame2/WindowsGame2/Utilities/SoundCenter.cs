using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace YaminGame.Utilities
{
  public  class SoundCenter
    {
      Microsoft.Xna.Framework.Game game;

      public SoundCenter(Microsoft.Xna.Framework.Game game)
      {
          this.game = game;
          Crash = game.Content.Load<SoundEffect>("crash");
          Swish = game.Content.Load<SoundEffect>("swish");
          Beep = game.Content.Load<Song>("beep_3");
          Beep2 = game.Content.Load<Song>("button_43");
          Whip = game.Content.Load<Song>("whip_whoosh_01");
      }

      public SoundEffect Crash { get; private set; }
      public SoundEffect Swish { get; private set; }

      public Song Beep { get; private set; }
      public Song Beep2 { get; private set; }
      public Song Whip { get; private set; }
    }
}




