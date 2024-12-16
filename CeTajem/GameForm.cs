using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using Label = System.Windows.Forms.Label;

namespace CeTajem
{
    public class GameForm : Form
    {
        private Label _scoreLabel;
        private Background _background;
        private Character _character;
        private Missile _missile;
        private Coin _coin;
        private SoundPlayer _missileSound;
        private SoundPlayer _coinSound;
        private int _score;

        // Replace pointer with boolean fields
        private bool _goUp;
        private bool _goDown;
        public GameForm()
        {
            // Initialize Game Form
            this.WindowState = FormWindowState.Maximized;
            this.Text = "JetMenyenangkan";

            // Intialize score label
            _score = 0;
            _scoreLabel = new Label
            {
                Text = "Score: 0",
                Location = new Point(10, 10),
                Font = new Font("Arial", 16),
                ForeColor = Color.Black,
                Size = new Size(200, 50),
                BackColor = Color.Transparent,
            };
            this.Controls.Add(_scoreLabel);

            // Initialize collision missile sound with character
            MemoryStream _missileSoundStream = new MemoryStream(Resource.missile_boom);
            _missileSound = new SoundPlayer(_missileSoundStream);

            // Initialize collision missile sound with character
            MemoryStream _coinSoundStream = new MemoryStream(Resource.coin_sound);
            _coinSound = new SoundPlayer(_coinSoundStream);

            // Mendapatkan maksimal screen size klien
            var screenBounds = Screen.PrimaryScreen.WorkingArea;

            // Set the form's size to match the client screen size
            this.Size = screenBounds.Size;

            // Initialize Character
            _character = new Character(this.ClientSize);
            this.Controls.Add(_character.GetPictureBox());

            // Initialize Missile
            _missile = new Missile(this.ClientSize);
            this.Controls.Add(_missile.GetPictureBox());

            // Initialize Coin
            _coin = new Coin(this.ClientSize);
            this.Controls.Add(_coin.GetPictureBox());

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

            // Looping gerak coin
            _coin.Move();

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

            // Checking if the character is collide with coin
            if (_character.IsCollidedWith(_coin))
            {
                // Destroy coin picture box
                this.Controls.Remove(_coin.GetPictureBox());
                _coinSound.Play();
                UpdateScore();
            }

            // Cek apakah missile sudah keluar dari layar
            if (_missile.IsOutOfScreen())
            {
                GenerateMissile();
            }

            // Cek apakah coin sudah keluar
            if (_coin.IsOutOfScreen())
            {
                GenerateCoin();
            }
        }
        public void GenerateMissile()
        {
            if (_missile != null)
            {
                // Remove missile
                this.Controls.Remove(_missile.GetPictureBox());
            }

            // Spawn new missile
            _missile = new Missile(this.ClientSize);
            this.Controls.Add(_missile.GetPictureBox());
        }
        public void GenerateCoin()
        {
            if (_coin != null)
            {
                // Remove coin
                this.Controls.Remove(_coin.GetPictureBox());
            }

            // Spawn new coin
            _coin = new Coin(this.ClientSize);
            this.Controls.Add(_coin.GetPictureBox());
        }
        
        public void UpdateScore()
        {
            _score++;
            _scoreLabel.Text = "Score: " + _score;
        }
    }
}