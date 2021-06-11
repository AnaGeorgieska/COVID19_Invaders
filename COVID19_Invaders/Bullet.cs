using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
    public class Bullet
    {
        public PictureBox pictureBox { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public string tag { get; set; }
        public Image image { get; set; }
        public Bullet(int width, int height, int top, int left, Image image, string tag)
        {
            this.pictureBox = new PictureBox();
            this.width = width;
            this.height = height;
            this.top = top;
            this.left = left;
            this.image = image;
            this.tag = tag;
            init();
        }
        public void init()
        {
            pictureBox.Image = image;
            pictureBox.Size = new Size(width, height);
            pictureBox.Tag = tag;
            pictureBox.Left = left;
            pictureBox.Top = top;
        }

        internal void move()
        {
            if (this.pictureBox.Tag.Equals("bullet"))
                this.pictureBox.Top -= 20;
            else if (this.pictureBox.Tag.Equals("bullet1"))
            {
                this.pictureBox.Top -= 15;
                this.pictureBox.Left -= 4;
            }
            else
            {
                this.pictureBox.Top -= 15;
                this.pictureBox.Left += 4;
            }
        }
        public void draw(Graphics g)
        {
            g.DrawImage(this.pictureBox.Image, this.pictureBox.Location.X, this.pictureBox.Location.Y, this.pictureBox.Width, this.pictureBox.Height);
        }
    }
}
