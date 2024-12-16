using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CeTajem
{
    public class GameForm : Form
    {
        private Background _background;
        private Character _character;
        private Missile _missile;
        private SoundPlayer _missileSound;

        // Replace pointer with boolean fields
        private bool _goUp;
        private bool _goDown;

        public GameForm()
        {
            // Initialize Game Form
            this.WindowState = FormWindowState.Maximized;
            this.Text = "JetMenyenangkan";

            // Initialize collision missile sound with character
            MemoryStream _missileSoundStream = new MemoryStream(Resource.missile_boom);
            _missileSound = new SoundPlayer(_missileSoundStream);

            // Mendapatkan maksimal screen size klien
            var screenBounds = Screen.PrimaryScreen.WorkingArea;

            // Set the form's size to match the client screen size
            this.Size = screenBounds.Size;

            // Initialize Character
            _character = new Character(this.ClientSize);
            this.Controls.Add(_character.GetPictureBox());

            // Initialize obstacle
            _missile = new Missile(this.ClientSize);
            this.Controls.Add(_missile.GetPictureBox());

            // Initialize Background (paling bawah)
            _background = new Background(this.ClientSize);
            this.Controls.Add(_background.GetPictureBox());

            // Initialize Keyboard Event Handling
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            // Initialize loop game and FPS
            Timer timer = new Timer();
            timer.Interval = 1000 / 150;
            timer.Tick += UpdatePosition;
            timer.Start();
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            // Menentukan tombol mana yang ditekan dan mengubah nilai boolean sesuai tombol
            if (e.KeyCode == Keys.Space)
            {
                _goUp = true;
                _goDown = false;
            }
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            // Mengatur kembali nilai boolean saat tombol dilepaskan
            if (e.KeyCode == Keys.Space)
            {
                _goUp = false;
                _goDown = true;
            }
        }

        private void UpdatePosition(object? sender, EventArgs e)
        {
            // Looping naik turun obstacle
            _missile.Move();

            // Move the object based on which keys are pressed
            if (_goUp) _character.MoveUp();
            if (_goDown) _character.MoveDown(this.ClientSize.Height);  // Use ClientSize.Height

            // Checking if the character is collide with missile
            if (_character.IsCollidedWith(_missile))
            {
                _missileSound.Play();

                // Destroy missile picture box
                this.Controls.Remove(_missile.GetPictureBox());

                // Close game form
                this.Close();
            }
            if (_missile.IsOutOfSky(this.ClientSize))
            {
                GenerateMissile();
            }
        }
        public void GenerateMissile()
        {
            if (_missile != null)
            {
                // Remove missile
                this.Controls.Remove(_missile.GetPictureBox());\
                // Remove background
                this.Controls.Remove(_background.GetPictureBox());
            }
            // Spawn new missile
            _missile = new Missile(this.ClientSize);
            this.Controls.Add(_missile.GetPictureBox());
            // Spawn background again
            _background = new Background(this.ClientSize);
            this.Controls.Add(_background.GetPictureBox());
        }
    }
}