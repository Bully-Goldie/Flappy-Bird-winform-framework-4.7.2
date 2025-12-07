// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace Flappy_Bird.Game_logic
{
    public static class exit_game
    {
        private static bool result = false;

        /// <summary>
        /// Обрабатывает кнопку выхода в главное меню
        /// </summary>
        /// <param name="res">Булевое значение</param>
        public static void Exit(bool res)
        {
            result = res;
        }

        /// <summary>
        /// Выход из игры в главное меню
        /// </summary>
        /// <returns>Возвращает истину</returns>
        public static bool main_exit()
        {
            return result;
        }

        /// <summary>
        /// Меняет значение при каждом перезаходе
        /// </summary>
        public static void Reset()
        {
            result = false;
        }

    }
}

