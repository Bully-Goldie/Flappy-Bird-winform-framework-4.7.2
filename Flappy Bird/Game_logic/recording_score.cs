// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.IO;

namespace Flappy_Bird.Game_logic
{
    public class recording_score
    {
        string file = "res/data/score.txt";

        /// <summary>
        /// Записывает самый большой рекорд в txt файл
        /// </summary>
        /// <param name="score">Рекорд</param>
        public void Record(int score)
        {
            try
            {
                // Читает файл с рекордом
                string read = File.ReadAllText(file);

                // Из строки преобразовывает в число
                int max = Convert.ToInt32(read);

                // Проверка рекорда, если рекорд больше чем число в txt то число записывается в файл
                if (score > max)
                {
                    // Запись в файл txt
                    File.WriteAllText(file, score.ToString());
                }
            }
            catch
            {
                // Если файла нет или он повреждён — просто создаём новый
                File.WriteAllText(file, score.ToString());
            }
        }

        /// <summary>
        /// Читает из файла рекорд которой отображается в игре
        /// </summary>
        /// <returns>Рекорд</returns>
        public string Reading()
        {
            try
            {
                // Читает из файла и возвращает рекорд
                return File.ReadAllText(file);
            }
            catch
            {
                // Если файла нет — создаём и возвращаем 0
                File.WriteAllText(file, "0");
                return "0";
            }
        }
    }
}