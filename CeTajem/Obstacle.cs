using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem;

public abstract class Obstacle : IMoveable
{
    public Random _random;
    public abstract void Move();
    // speed ke kiri, default = 10
    private int _speed = 10;
    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public abstract void _damagePlayer();
    public abstract PictureBox GetPictureBox();
}

public class Missile : Obstacle, IMoveable
{
    private PictureBox _missilePictureBox;
    private Size _screenSize;
    private int _flag = 0;
    private Image _spriteSheet;
    private int _spawnY;

    public int Flag
    {
        get { return this._flag; }
        set { _flag = value; }
    }
    public Missile(Size screenSize)
    {
        _random = new Random();
        // Import missile images
        using (MemoryStream ms = new MemoryStream(Resource.missile_kudanil))
        {
            _spriteSheet = Image.FromStream(ms);
        }

        // Initialize screenSize client
        _screenSize = screenSize;

        // Set random spawn point Y for missile
        _spawnY = _random.Next(screenSize.Height / 5, screenSize.Height - screenSize.Height / 5);


        // Initialize picture box laser
        _missilePictureBox = new PictureBox()
        {
            Size = new Size(50, 50),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Location = new Point(screenSize.Width + 1, _spawnY),
            Image = _spriteSheet,
        };

        // Ganti speed laser jadi 20
        Speed = 5;
    }

    public override PictureBox GetPictureBox()
    {
        return _missilePictureBox;
    }

    public override void Move()
    {
        _missilePictureBox.Location = new Point(_missilePictureBox.Location.X - 5, _missilePictureBox.Location.Y);
        if (_flag == 0)
        {
            // Remove pointer syntax
            _missilePictureBox.Location = new Point(_missilePictureBox.Location.X, _missilePictureBox.Location.Y + Speed);

            if (_missilePictureBox.Location.Y >= _spawnY + 200)
            {
                _flag = 1;
            }
        }
        else
        {
            // Remove pointer syntax
            _missilePictureBox.Location = new Point(_missilePictureBox.Location.X, _missilePictureBox.Location.Y - Speed);
            if (_missilePictureBox.Location.Y <= _spawnY - 200)
            {
                _flag = 0;
            }
        }
    }

    public override void _damagePlayer()
    {
        throw new NotImplementedException();
    }
    public bool IsOutOfScreen()
    {
        return _missilePictureBox.Location.X < 0;
    }
}