using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public class Invader
    {
        public static int enemySpeed { get; set; }
        public PictureBox pictureBox { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public Image image { get; set; }

        public Invader(int width, int height, int top, int left, Image image)
        {
            Invader.enemySpeed = 8;
            pictureBox = new PictureBox();
            this.width = width;
            this.height = height;
            this.top = top;
            this.left = left;
            this.image = image;
            init();
        }
        public void init()
        {
            pictureBox.Size = new Size(this.width, this.height);
            pictureBox.Image = image;
            pictureBox.Top = this.top;
            pictureBox.Left = this.left;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public void moveInvader(int width)
        {
            if(Scene.level==3)
            {
                this.pictureBox.Left += enemySpeed/2;
                this.pictureBox.Top += enemySpeed/2;
            }

            if(Scene.level!=3)
            {
                this.pictureBox.Left += enemySpeed;
                if (this.pictureBox.Left > width)
                {
                    this.pictureBox.Top += (Scene.level * 20) + 45;
                    this.pictureBox.Left = -80;
                }
            }
               
        }
        public void draw(Graphics g)
        {
            g.DrawImage(this.pictureBox.Image, this.pictureBox.Location.X, this.pictureBox.Location.Y, this.pictureBox.Width, this.pictureBox.Height);
        }
    }
}
