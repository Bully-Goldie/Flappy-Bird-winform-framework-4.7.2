// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Flappy_Bird.Game_forms;
using FlappyBird;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class Main_menu : Form
    {
        public static bool MutedSound = false;
        public Main_menu()
        {
            InitializeComponent();

            exit.Cursor = Cursors.Hand;
            start.Cursor = Cursors.Hand;
            info.Cursor = Cursors.Hand;
            Sound.Cursor = Cursors.Hand;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            game open = new game();

            this.Visible = false;
            open.ShowDialog();
            this.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            info open = new info();

            open.ShowDialog();
        }

        private void Sound_Click(object sender, EventArgs e)
        {
            // Изменение иконки и звука на включенный
            if(MutedSound)
            {
                Sound.Image = Image.FromFile("res/img/volumeOn.png");
                MutedSound = false;
                SoundManager.UpdateAllVolume();
                return;
            }

            // Изменение иконки и звука на выключенный
            else
            {
                Sound.Image = Image.FromFile("res/img/volumeOff.png");
                MutedSound = true;
                SoundManager.UpdateAllVolume();
                return;
            }
        }
    }
}