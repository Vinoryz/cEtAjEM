using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem;

public abstract class Obstacle : IMoveable
{
    public abstract void Move();
    private int _speed;

    public abstract void _damagePlayer();
}   

public class Laser : Obstacle, IMoveable
{
    private PictureBox _laserPictureBox;
    public Laser() 
    {
        _
    }

    public override void Move()
    {
        throw new NotImplementedException();
    }

    public override void _damagePlayer()
    {
        throw new NotImplementedException();
    }
}