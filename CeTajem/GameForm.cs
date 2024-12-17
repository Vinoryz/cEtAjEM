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
        private Character _character;
        private Missile _missile;
        private Laser _laser;
        private Coin _coin;
        private Piggybank _piggybank;
        private SoundPlayer _missileSound;
        private SoundPlayer _laserSound;
        private SoundPlayer _coinSound;
        private SoundPlayer _piggybankSound;
        private int _score = 0;
        private bool _coinCollected = false;
        private bool _piggybankCollected = false;

        // Replace pointer with boolean fields
        private bool _goUp;
        private bool _goDown;
        public GameForm()
        {
            // Initialize Game Form
            this.WindowState = FormWindowState.Maximized;
            this.Text = "JetMenyenangkan";
            this.BackColor = ColorTranslator.FromHtml("#34e1eb");

            // Intialize score label
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

            // Initialize collision laser sound with character
            MemoryStream _laserSoundStream = new MemoryStream(Resource.laser_zap);
            _laserSound = new SoundPlayer(_laserSoundStream);

            // Initialize collision coin sound with character
            MemoryStream _coinSoundStream = new MemoryStream(Resource.coin_sound);
            _coinSound = new SoundPlayer(_coinSoundStream);

            // Initialize collision coin sound with character
            MemoryStream _piggybankSoundStream = new MemoryStream(Resource.piggybank_sound);
            _piggybankSound = new SoundPlayer(_piggybankSoundStream);

            // Mendapatkan maksimal screen size client
            var screenBounds = Screen.PrimaryScreen.WorkingArea;

            // Set the form's size to match the client screen size
            this.Size = screenBounds.Size;

            // Initialize Character
            _character = new Character(this.ClientSize);
            this.Controls.Add(_character.GetPictureBox());

            // Initialize Missile
            _missile = new Missile(this.ClientSize);
            this.Controls.Add(_missile.GetPictureBox());

            // Initialize Laser
            _laser = new Laser(this.ClientSize);
            this.Controls.Add(_laser.GetPictureBox());

            // Initialize piggybank
            _piggybank = new Piggybank(this.ClientSize);
            this.Controls.Add(_piggybank.GetPictureBox());

            // Initialize Coin
            _coin = new Coin(this.ClientSize, 0, 0);
            this.Controls.Add(_coin.GetPictureBox());

            // Initialize Keyboard Event Handling
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            // Buat player jatuh pertama kali
            _goDown = true;

            // _goDown = true;
            // this.FormClosed +=

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
            // Looping naik turun dan maju missile
            _missile.Move();

            // Looping maju laser
            _laser.Move();

            // Looping gerak coin
            _coin.Move();

            // Looping naik turun dan maju piggybank
            _piggybank.Move();

            // Move the object based on which keys are pressed
            if (_goUp) _character.MoveUp();
            if (_goDown) _character.MoveDown(this.ClientSize.Height);  // Use ClientSize.Height

            // Checking if the character is collide with missile
            if (_character.IsCollidedWith(_missile))
            {
                // play missile sound
                _missileSound.Play();

                // Destroy missile picture box
                this.Controls.Remove(_missile.GetPictureBox());

                // panggil fungsi end game
                EndGame();
            }

            // Checking if the character is collide with laser
            if (_character.IsCollidedWith(_laser))
            {
                // play laser sound
                _laserSound.Play();

                // Destroy missile picture box
                this.Controls.Remove(_laser.GetPictureBox());

                // panggil fungsi end game
                EndGame();
            }

            // Checking if the character is collide with coin
            if (_character.IsCollidedWith(_coin) && !_coinCollected)
            {
                // Destroy coin picture box
                this.Controls.Remove(_coin.GetPictureBox());
                _coinSound.Play();
                UpdateScore(1);

                _coinCollected = true;
            }

            // Checking if the character is collide with piggybank
            if (_character.IsCollidedWith(_piggybank) && !_piggybankCollected)
            {
                // Destroy coin picture box
                this.Controls.Remove(_piggybank.GetPictureBox());
                _piggybankSound.Play();
                UpdateScore(100);

                _piggybankCollected = true;
            }

            // Cek apakah missile sudah keluar dari layar
            if (_missile.IsOutOfScreen())
            {
                GenerateMissile();
            }

            // Cek apakah laser sudah keluar dari layar
            if (_laser.IsOutOfScreen())
            {
                GenerateLaser();
            }

            // Cek apakah coin sudah keluar dari layar
            if (_coin.IsOutOfScreen() || _coinCollected)
            {
                GenerateCoin();
                _coinCollected = false; // Reset the flag when generating a new coin
            }

            // Cek apakah piggybank sudah keluar dari layar
            if (_piggybank.IsOutOfScreen() || _piggybankCollected)
            {
                GeneratePiggybank();
                _piggybankCollected = false; // Reset the flag when generating a new piggybank
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
        public void GenerateLaser()
        {
            if (_laser != null)
            {
                // Remove laser
                this.Controls.Remove(_laser.GetPictureBox());
            }

            // Spawn new laser
            _laser = new Laser(this.ClientSize);
            this.Controls.Add(_laser.GetPictureBox());
        }
        public void GeneratePiggybank()
        {
            if (_piggybank != null)
            {
                // Remove laser
                this.Controls.Remove(_piggybank.GetPictureBox());
            }

            // Spawn new laser
            _piggybank = new Piggybank(this.ClientSize);
            this.Controls.Add(_piggybank.GetPictureBox());
        }
        public void GenerateCoin()
        {
            if (_coin != null)
            {
                // Remove coin
                this.Controls.Remove(_coin.GetPictureBox());
            }

            // Determine which obstacle to use based on current state
            int obstacleSpawnY;
            int obstacleHeight;

            // Use laser if it exists and is more recently spawned
            if (_laser.GetPictureBox().Location.X > _missile.GetPictureBox().Location.X)
            {
                obstacleSpawnY = _laser.GetPictureBox().Location.Y;
                obstacleHeight = _laser.GetPictureBox().Height;
            }
            else
            {
                obstacleSpawnY = _missile.GetPictureBox().Location.Y;
                obstacleHeight = _missile.GetPictureBox().Height;
            }

            // Spawn new coin, passing obstacle spawn Y and height
            _coin = new Coin(this.ClientSize, obstacleSpawnY, obstacleHeight);
            this.Controls.Add(_coin.GetPictureBox());
        }

        public void UpdateScore(int score)
        {
            _score += score;
            _scoreLabel.Text = "Score: " + _score;
        }
        public void EndGame()
        {
            // tutup game form
            this.Close();

            HighScoreForm.AddHighscore(_score);
        }
    }
}