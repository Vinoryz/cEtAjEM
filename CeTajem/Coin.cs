using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem
{
    public class Coin : IMoveable
    {
        private PictureBox _coinPictureBox;
        private Image _coinImage;
        private Size _screenSize;
        private int _speed = 5;
        private Random _random;
        public Coin(Size screenSize) // Screensize 1904 x 1041
        {
            _random = new Random();
            // Import coin image
            using (MemoryStream ms = new MemoryStream(Resource.coin))
            {
                _coinImage = Image.FromStream(ms);
            }
            // Initialize screenSize client
            _screenSize = screenSize;

            // Initialize picture box coin
            _coinPictureBox = new PictureBox()
            {
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(screenSize.Width + 1, _random.Next(0, screenSize.Height)),
                Image = _coinImage,
            };
        }

        public void Move()
        {
            _coinPictureBox.Location = new Point(_coinPictureBox.Location.X - _speed, _coinPictureBox.Location.Y);
        }

        public PictureBox GetPictureBox()
        {
            return _coinPictureBox;
        }
        public bool IsOutOfScreen()
        {
            return _coinPictureBox.Location.X < 0;
        }
    }
}
