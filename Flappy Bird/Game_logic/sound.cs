// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.IO;
using System.Windows.Media;
using Flappy_Bird;

namespace FlappyBird
{
    public static class SoundManager
    {
        // Хранит все MediaPlayer — один на каждый звук.
        private static readonly System.Collections.Generic.Dictionary<string, MediaPlayer> players
            = new System.Collections.Generic.Dictionary<string, MediaPlayer>();


        /// <summary>
        /// Проигрывает звук по относительному пути.
        /// Если звук уже был открыт ранее — используется существующий MediaPlayer.
        /// </summary>
        public static void PlaySound(string relativePath)
        {
            try
            {
                // Получаем полный путь к файлу звука
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

                // 0 — звук выключен, 1 — включен
                double volume = Main_menu.MutedSound ? 0.0 : 1.0;

                MediaPlayer player;

                // Если этот звук уже создавали — берем готовый плеер
                if (players.TryGetValue(fullPath, out player))
                {
                    player.Volume = volume;
                    player.Position = TimeSpan.Zero; // Начать с начала
                    player.Play();
                    return;
                }

                // Создаем новый плеер для этого звука
                player = new MediaPlayer
                {
                    Volume = volume
                };

                // Открываем файл
                player.Open(new Uri(fullPath));

                // После окончания — стоп и перемотка
                player.MediaEnded += (sender, e) =>
                {
                    MediaPlayer p = (MediaPlayer)sender;
                    p.Stop();
                    p.Position = TimeSpan.Zero;
                };

                player.Play();

                // Сохраняем созданный плеер в словарь
                players.Add(fullPath, player);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Ошибка воспроизведения звука {relativePath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновляет громкость всех уже созданных звуков.
        /// </summary>
        public static void UpdateAllVolume()
        {
            double volume = Main_menu.MutedSound ? 0.0 : 1.0;

            foreach (var player in players.Values)
            {
                player.Volume = volume;
            }
        }
    }
}
