// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Flappy_Bird.Game_logic;
using FlappyBird;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Flappy_Bird.Game_forms
{
    public partial class game : Form
    {
        // Метод записи рекорда в файл
        private recording_score rs = new recording_score();

        // Загрузка шрифтов
        private PrivateFontCollection load = new PrivateFontCollection();
        private Font flappyFont20;
        private Font flappyFont18;

        // Изображение птицы
        private Image birdImg;
        private Image jumpImg;

        // Параметры физики птицы
        private int fallSpeed = 0;
        private int gravity = 1;
        private int jumpPower = -12;
        private int maxSpeed = 12;

        // Параметры труб
        private int pipeSpeed = 6;
        private const int MinGap = 120;
        private const int MaxGap = 160;

        private int score = 0;

        // Флаги состояния
        private bool isJumping = false;
        private bool gameStarted = false;
        private bool pressSpace = true;
        private bool rebootGame = false;

        private Random random = new Random();

        public game()
        {
            InitializeComponent();

            // Загрузка шрифта
            try
            {
                load.AddFontFile("res/fonts/flappy-font.ttf");
                flappyFont20 = new Font(load.Families[0], 20);
                flappyFont18 = new Font(load.Families[0], 20);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки шрифта: {ex.Message}");
                flappyFont20 = DefaultFont;
                flappyFont18 = DefaultFont;
            }


            // Применение шрифтов
            label1.Font = flappyFont20;
            label2.Font = flappyFont20;
            label3.Font = flappyFont18;

            // Загрузка изображений птицы
            try
            {
                birdImg = Image.FromFile("res/img/bird.png");
                jumpImg = Image.FromFile("res/img/jump.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                Application.Exit();
            }

            bird.Image = birdImg;

            // Улучшение интерфейса
            KeyPreview = true;
            DoubleBuffered = true;

            // Отображение рекорда
            label3.Text = $"RECORD: {rs.Reading()}";

            Home.Visible = false;
            settings.Cursor = Cursors.Hand;
            Home.Cursor = Cursors.Hand;
        }

        private void game_KeyDown(object sender, KeyEventArgs e)
        {
            // Прыжок или старт игры
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                // Если игра окончена — перезапустить
                if (rebootGame)
                {
                    RestartGame();
                    return;
                }

                // Первый старт игры
                if (!gameStarted)
                {
                    gameStarted = true;
                    timer1.Start();
                }

                // Выполнить прыжок
                if (!isJumping)
                {
                    fallSpeed = jumpPower;
                    isJumping = true;
                    bird.Image = jumpImg;

                    SoundManager.PlaySound("res/sound/jump.wav");
                }

            }
        }

        private void game_KeyUp(object sender, KeyEventArgs e)
        {
            // Отпускание клавиши прыжка
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                isJumping = false;
                bird.Image = birdImg;

                // Убрать подсказку "PRESS SPACE"
                if (pressSpace)
                {
                    label2.Text = "";
                    pressSpace = false;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Применение гравитации
            fallSpeed += gravity;
            if (fallSpeed > maxSpeed) fallSpeed = maxSpeed;

            // Движение птицы
            bird.Top += fallSpeed;

            // Движение труб
            MovePipes();

            // Обновление счёта
            label1.Text = $"SCORE: {score}";

            // Проверка столкновений
            if (bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(pipeDown.Bounds) ||
                bird.Bounds.IntersectsWith(ground.Bounds) ||
                bird.Top < 0)
            {
                rs.Record(score);
                GameOver();
            }

            // Усложнение игры
            if (score == 30) pipeSpeed = 8;
            if (score == 60) pipeSpeed = 10;
            if (score == 100) pipeSpeed = 15;
        }

        // Обработка завершения игры
        private void GameOver()
        {
            timer1.Stop();
            settings.Visible = false;
            Home.Visible = true;

            label2.Text = "GAME OVER!";
            rebootGame = true;
            gameStarted = false;

            SoundManager.PlaySound("res/sound/hit.wav");
        }

        private void RestartGame()
        {
            timer1.Stop();

            // Сброс параметров
            fallSpeed = 0;
            score = 0;
            pipeSpeed = 6;
            isJumping = false;
            gameStarted = false;
            pressSpace = true;
            rebootGame = false;

            settings.Visible = true;
            Home.Visible = false;

            // Позиция птицы
            bird.Location = new Point(41, 235);

            ResetPipes();

            label1.Text = "SCORE: 0";

            Invalidate();
        }

        private void ResetPipes()
        {
            int gap = random.Next(MinGap, MaxGap);

            // Начальная позиция труб
            pipeTop.Left = 357;
            pipeTop.Top = random.Next(-250, -100);

            pipeDown.Left = 357;
            pipeDown.Top = pipeTop.Bottom + gap;
        }

        private void MovePipes()
        {
            // Перемещение влево
            pipeTop.Left -= pipeSpeed;
            pipeDown.Left -= pipeSpeed;

            // Если трубы вышли за экран — пересоздать
            if (pipeTop.Left < -pipeTop.Width)
            {
                // Новая позиция
                pipeTop.Left = ClientSize.Width + random.Next(150, 250);

                int gap = random.Next(MinGap, MaxGap);
                pipeTop.Top = random.Next(-250, -100);

                pipeDown.Left = pipeTop.Left;
                pipeDown.Top = pipeTop.Bottom + gap;

                score++;
                SoundManager.PlaySound("res/sound/point.wav");
            }
        }

        private void exit_game_settings()
        {
            if (exit_game.main_exit())
            {
                this.Close();
                exit_game.Reset();
            }
            else
            {
                timer1.Start();
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            settings open = new settings();
            open.ShowDialog();

            exit_game_settings();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
