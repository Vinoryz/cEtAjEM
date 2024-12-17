using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CeTajem;

public class Character
{
    private PictureBox _charPictureBox;
    private int _moveSpeed;

    public Character(Size screenSize)
    {
        _charPictureBox = new PictureBox
        {
            
            Size = new Size(50, 50),
            BackColor = Color.Gray,
            Location = new Point(screenSize.Width/7, screenSize.Height/2),
        };

        // Create a label inside the PictureBox
        Label nameLabel = new Label
        {
            Text = "Player",
            Font = new Font("Arial", 10), // Set the font size here
            Location = new Point(0, _charPictureBox.Size.Height / 2),
            ForeColor = Color.Black,
            BackColor = Color.Transparent
        };

        // Add the label to the PictureBox
        _charPictureBox.Controls.Add(nameLabel);
    }

    public PictureBox GetPictureBox() { return _charPictureBox; }

    public void MoveUp()
    {
        _moveSpeed = 13;
        if (_charPictureBox.Location.Y > 0)
        {
            _charPictureBox.Location = new Point(_charPictureBox.Location.X, _charPictureBox.Location.Y - _moveSpeed);
        }
    }

    public void MoveDown(int maxHeight)
    {
        _moveSpeed = 10;
        if (_charPictureBox.Location.Y + _charPictureBox.Height < maxHeight)
        {
            _charPictureBox.Location = new Point(_charPictureBox.Location.X, _charPictureBox.Location.Y + _moveSpeed);
        }
    }
    public bool IsCollidedWith(Obstacle obstacle)
    {
        return obstacle.GetPictureBox().Bounds.IntersectsWith(this.GetPictureBox().Bounds);
    }
    public bool IsCollidedWith(Coin coin)
    {
        return coin.GetPictureBox().Bounds.IntersectsWith(this.GetPictureBox().Bounds);
    }
    public bool IsCollidedWith(Piggybank piggybank)
    {
        return piggybank.GetPictureBox().Bounds.IntersectsWith(this.GetPictureBox().Bounds);
    }
}
