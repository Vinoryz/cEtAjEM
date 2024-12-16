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
        public Coin(Size screenSize)
        {
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
                Location = new Point(_screenSize.Width - (_screenSize.Width / 7), _screenSize.Height / 2 + 50),
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
