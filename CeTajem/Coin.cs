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
        private int _spawnY;

        public Coin(Size screenSize, int obstacleSpawnY, int obstacleHeight)
        {
            _random = new Random();

            // Import coin image
            using (MemoryStream ms = new MemoryStream(Resource.coin))
            {
                _coinImage = Image.FromStream(ms);
            }

            // Initialize screenSize client
            _screenSize = screenSize;

            // Generate spawn Y that avoids obstacle completely
            _spawnY = GenerateUniqueSpawnY(obstacleSpawnY, obstacleHeight, screenSize);

            // Initialize picture box coin
            _coinPictureBox = new PictureBox()
            {
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(screenSize.Width + 1, _spawnY),
                Image = _coinImage,
            };
        }

        private int GenerateUniqueSpawnY(int obstacleSpawnY, int obstacleHeight, Size screenSize)
        {
            int coinSpawnY;
            int attempts = 0;
            const int maxAttempts = 10;

            do
            {
                // Generate a random spawn Y
                coinSpawnY = _random.Next(0, screenSize.Height - 50); // Subtract coin height

                // Check if the coin completely avoids the obstacle
                bool isOverlapping =
                    (coinSpawnY < obstacleSpawnY + obstacleHeight) &&
                    (coinSpawnY + 50 > obstacleSpawnY);

                // If not overlapping, return the spawn Y
                if (!isOverlapping)
                {
                    return coinSpawnY;
                }

                attempts++;
            } while (attempts < maxAttempts);

            // Fallback to a different section of the screen
            return (obstacleSpawnY + obstacleHeight + 100) % screenSize.Height;
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