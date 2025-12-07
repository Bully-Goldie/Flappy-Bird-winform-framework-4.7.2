// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Flappy_Bird.Game_logic;
using System;
using System.Windows.Forms;

namespace Flappy_Bird.Game_forms
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();

            Home.Cursor = Cursors.Hand;
            OK.Cursor = Cursors.Hand;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            // Переход в главное меню
            exit_game.Exit(true);

            this.Close();
        }
    }
}
