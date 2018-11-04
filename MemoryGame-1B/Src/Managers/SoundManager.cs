using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles the playing of sound
    /// </summary>
    public static class SoundManager
    {
        /// <summary>
        /// Should it be muted
        /// </summary>
        private static bool _mute;

        /// <summary>
        /// All the sound players
        /// </summary>
        private static readonly List<SoundPlayer> SoundPlayers = new List<SoundPlayer>();

        /// <summary>
        /// Sound player for background music
        /// </summary>
        private static readonly SoundPlayer SoundPlayer = new SoundPlayer();

        /// <summary>
        /// Loads The Sounds
        /// </summary>
        public static void LoadSounds()
        {
            SoundPlayer.SoundLocation = new Uri("SoundEffects/Background/Background.wav", UriKind.Relative).ToString();
            SoundPlayer.PlayLooping();

            var uri = new Uri("SoundEffects/Zombie", UriKind.Relative).ToString();

            foreach (var file in Directory.GetFiles(uri))
            {
                var soundPlayer = new SoundPlayer(file);
                SoundPlayers.Add(soundPlayer);
            }
        }

        public static void ToggleSound()
        {
            _mute = !_mute;

            if (_mute)
            {
                SoundPlayer.Stop();
            }
            else
            {
                SoundPlayer.Play();
            }

        }

        /// <summary>
        /// Plays a random sound
        /// </summary>
        public static void PlayRandom()
        {
            if (!_mute) SoundPlayers.Random().Play();
        }
    }
}