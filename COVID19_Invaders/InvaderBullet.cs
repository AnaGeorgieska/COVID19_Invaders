using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public class InvaderBullet
    {
        public PictureBox pictureBox { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public Image image { get; set; }
        public InvaderBullet(int width, int height, int top, int left, Image image)
        {
            this.pictureBox = new PictureBox();
            this.width = width;
            this.height = height;
            this.top = top;
            this.left = left;
            this.image = image;
            init();
        }
        public void init()
        {   
            pictureBox.Image = image;
            pictureBox.Size = new Size(width, height);
            pictureBox.Left = left;
            pictureBox.Top = top;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        internal void move()
        {
            this.pictureBox.Top += 10;
        }
        public void draw(Graphics g)
        {
            g.DrawImage(this.pictureBox.Image, this.pictureBox.Location.X, this.pictureBox.Location.Y, this.pictureBox.Width, this.pictureBox.Height);
        }
    }
}
