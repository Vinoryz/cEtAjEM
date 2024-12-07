using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem
{
    public class Background
    {
        private PictureBox _backgroundPictureBox;
        private Image _spriteSheet;

        public Background(Size screenSize)
        {
            using (MemoryStream ms = new MemoryStream(Resource.window_background))
            {
                _spriteSheet = Image.FromStream(ms);
            }

            _backgroundPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = _spriteSheet,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
        }

        public PictureBox GetPictureBox()
        {
            return _backgroundPictureBox;
        }
    }
}
