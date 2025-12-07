// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Flappy_Bird.Game_forms
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();

            // Загрузка шрифтов
            PrivateFontCollection load = new PrivateFontCollection();
            load.AddFontFile("res/fonts/flappy-font.ttf");
            FontLoader.LoadFont("Flappy_Bird.res.fonts.flappy-font.ttf");
            

            var flappy = FontLoader.PFC.Families[0];
            name.Font = new Font(flappy, 15);
            group.Font = new Font(flappy, 18);

            exit.Cursor = Cursors.Hand;
        }

        private void exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
