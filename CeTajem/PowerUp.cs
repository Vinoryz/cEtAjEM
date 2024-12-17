using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem;

public abstract class PowerUp : IMoveable
{
    protected Size _screenSize;
    public Random _random;
    public static int _spawnY;
    private int _speed = 8;
    // speed ke kiri, default = 8
    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public abstract void Move();
    public abstract PictureBox GetPictureBox();
}

public class Piggybank : PowerUp
{
    private PictureBox _piggybankPictureBox;
    private Image _piggybankImage;
    private int _flag = 0;
    public Piggybank(Size screenSize)
    {
        _random = new Random();
        // Import missile images
        using (MemoryStream ms = new MemoryStream(Resource.piggybank))
        {
            _piggybankImage = Image.FromStream(ms);
        }

        // Initialize screenSize client
        _screenSize = screenSize;

        // Set random spawn point Y for missile
        _spawnY = _random.Next(screenSize.Height / 5, screenSize.Height - screenSize.Height / 5);


        // Initialize picture box laser
        _piggybankPictureBox = new PictureBox()
        {
            Size = new Size(50, 50),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Location = new Point(screenSize.Width + 1, _spawnY),
            Image = _piggybankImage,
        };

        // Ganti speed piggybank jadi 5
        Speed = 5;
    }
    public override PictureBox GetPictureBox()
    {
        return _piggybankPictureBox;
    }

    public override void Move()
    {
        _piggybankPictureBox.Location = new Point(_piggybankPictureBox.Location.X - 8, _piggybankPictureBox.Location.Y);
        if (_flag == 0)
        {
            // Remove pointer syntax
            _piggybankPictureBox.Location = new Point(_piggybankPictureBox.Location.X, _piggybankPictureBox.Location.Y + Speed);

            if (_piggybankPictureBox.Location.Y >= _spawnY + 200)
            {
                _flag = 1;
            }
        }
        else
        {
            // Remove pointer syntax
            _piggybankPictureBox.Location = new Point(_piggybankPictureBox.Location.X, _piggybankPictureBox.Location.Y - Speed);
            if (_piggybankPictureBox.Location.Y <= _spawnY - 200)
            {
                _flag = 0;
            }
        }
    }
    public bool IsOutOfScreen()
    {
        return _piggybankPictureBox.Location.X < 0;
    }
}
