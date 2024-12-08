using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CeTajem
{
    public class MainForm : Form
    {
        private Background _background;
        private Character _character;
        private Laser _obstacle;
        private bool _goUp, _goDown;

        public MainForm()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Kudanil Terbang";

            // Mendapatkan maksimal screen size klien
            var screenBounds = Screen.PrimaryScreen.WorkingArea;

            // Set the form's size to match the client screen size
            this.Size = screenBounds.Size;

            // Initialize Character
            _character = new Character(this.ClientSize);
            this.Controls.Add(_character.GetPictureBox());

            // Initialize obstacle
            _obstacle = new Laser(this.ClientSize);
            this.Controls.Add(_obstacle.GetPictureBox());

            // Initialize Background (paling bawah)
            _background = new Background(this.ClientSize);
            this.Controls.Add(_background.GetPictureBox());

            // Initialize Keyboard Event Handling
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            Timer timer = new Timer();
            timer.Interval = 1000/150;
            timer.Tick += new EventHandler(UpdatePosition);
            timer.Start();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Menentukan tombol mana yang ditekan dan mengubah nilai boolean sesuai tombol
            if (e.KeyCode == Keys.Space)
            {
                _goUp = true;
                _goDown = false;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Mengatur kembali nilai boolean saat tombol dilepaskan
            if (e.KeyCode == Keys.Space)
            {
                _goUp = false;
                _goDown = true;
            }
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            // Looping naik turun obstacle
            _obstacle.Move();

            // Move the object based on which keys are pressed
            if (_goUp) _character.MoveUp();
            if (_goDown) _character.MoveDown(this.ClientSize.Height);  // Use ClientSize.Height
        }
    }
}
