using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem;

public abstract class Obstacle : IMoveable
{
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

public class Laser : Obstacle, IMoveable
{
    private PictureBox _laserPictureBox;
    private Size _screenSize;
    public Laser(Size screenSize) 
    {
        // Initialize screenSize client
        _screenSize = screenSize;

        // Initialize picture box laser
        _laserPictureBox = new PictureBox()
        {
            Size = new Size(50, 50),
            BackColor = Color.Gray,
            Location = new Point(_screenSize.Width-(_screenSize.Width / 7), _screenSize.Height / 2),
        };
    }

    public override PictureBox GetPictureBox()
    {
        return _laserPictureBox;
    }

    public override void Move()
    {
        if(_laserPictureBox.Location.Y == _screenSize.Height / 5)
        {
            _laserPictureBox.Location = new Point(_laserPictureBox.Location.X, _laserPictureBox.Location.Y + Speed);
        }
        if (_laserPictureBox.Location.Y == _screenSize.Height - _screenSize.Height / 5)
        {
            _laserPictureBox.Location = new Point(_laserPictureBox.Location.X, _laserPictureBox.Location.Y - Speed);
        }
    }

    public override void _damagePlayer()
    {
        throw new NotImplementedException();
    }
}