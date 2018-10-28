using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles the playing of sound
    /// </summary>
    public static class SoundManager
    {
        /// <summary>
        /// All the sound players
        /// </summary>
        private static readonly List<SoundPlayer> SoundPlayers = new List<SoundPlayer>();

        /// <summary>
        /// Constructor
        /// </summary>
        static SoundManager() => LoadSounds();

        /// <summary>
        /// Loads The Sounds
        /// </summary>
        private static void LoadSounds()
        {
            var uri = new Uri("../../SoundEffects/Zombie", UriKind.Relative).ToString();

            foreach (var file in Directory.GetFiles(uri))
            {
                var soundPlayer = new SoundPlayer(file);
                SoundPlayers.Add(soundPlayer);
            }
        }

        /// <summary>
        /// Plays a random sound
        /// </summary>
        public static void PlayRandom() => SoundPlayers.Random().Play();
    }
}