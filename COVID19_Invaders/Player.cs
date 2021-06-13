using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public class Player
    {
        public bool goLeft { get; set; } //дали се движи во лево
        public bool goRight { get; set; } //дали се движи во десно
        public static int playerSpeed { get; set; } //брзина на движенје при секој такт
        public int lives { get; set; } //број на животи
        public PictureBox[] livesImages { get; set; } //низа со контроли за приказ на животите
        public bool fullPower { get; set; } //дали оружјето е во полна моќ
        public PictureBox pictureBox { get; set; } //контрола за приказ на играчот
        public int formWidth { get; set; } //ширина на прозорецот за движење на играчот
        public int formHeight { get; set; } //висина на прозорецот за приказ на животите
        public int numPlayer { get; set; } //број на играч
        public Player( int num, int formWidth, int formHeight)
        {
            this.numPlayer = num;
            this.formWidth = formWidth;
            this.formHeight = formHeight;
            this.lives = 3;
            this.fullPower = false;
            Player.playerSpeed = 12;
            livesImages = new PictureBox[5];
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(77, 64);
            pictureBox.Image = Properties.Resources.syringe;
            pictureBox.Top = formHeight - 130 ;
            pictureBox.Left = 300*numPlayer;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.setLivesImages();
        }
        public void setLivesImages()
        {
            int left = 0;
            if (numPlayer == 2)
                left = formWidth - 180;
            for (int i = 0; i < livesImages.Length; i++)
            {
                livesImages[i] = new PictureBox();
                livesImages[i].Size = new Size(30, 25);
                livesImages[i].Image = Properties.Resources.syringe;
                livesImages[i].Top = formHeight - 70;
                livesImages[i].Left = left;
                livesImages[i].BackColor = Color.Transparent;
                livesImages[i].SizeMode = PictureBoxSizeMode.StretchImage;
                left = left + 30;
            }
        }
        public Bullet makeBullet(string bulletTag)
        {
            Bullet bullet = new Bullet(7, 25, pictureBox.Top - 20, pictureBox.Left + this.pictureBox.Width / 2,
               Properties.Resources.bulletDrop2, bulletTag);
            return bullet;
        }
        public void GoLeft(bool left)
        {
            this.goLeft = left;
        }
        public void GoRight(bool right)
        {
            this.goRight = right;
        }
     
        public void movePlayer()
        {
            if (this.goLeft && this.pictureBox.Location.X > 0)
            {
                 this.pictureBox.Left -= Player.playerSpeed;
            }
            if (this.goRight && this.pictureBox.Location.X < formWidth - this.pictureBox.Width)
            {
                 this.pictureBox.Left += Player.playerSpeed;
            }
        }
        public void draw(Graphics g)
        {
            g.DrawImage(this.pictureBox.Image, this.pictureBox.Location.X, this.pictureBox.Location.Y, this.pictureBox.Width, this.pictureBox.Height);
            for(int i=0;i<lives;i++ )
            {
                g.DrawImage(livesImages[i].Image, livesImages[i].Location.X, livesImages[i].Location.Y, livesImages[i].Width, livesImages[i].Height);
            }
        }
    }
}
