using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public class Player
    {
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public static int playerSpeed { get; set; }
        public int lives { get; set; }
        public bool fullPower { get; set; }
        public PictureBox pictureBox { get; set; }
        public PictureBox[] livesImages { get; set; }
        public Form1 form { get; set; }
        public int numPlayer { get; set; }
        public Player(Form1 form, int num)
        {
            this.numPlayer = num;
            this.form = form;
            this.lives = 3;
            this.fullPower = false;
            Player.playerSpeed = 12;
            livesImages = new PictureBox[5];
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(77, 64);
            pictureBox.Image = Properties.Resources.syringe;
            pictureBox.Top = 600;
            pictureBox.Left = 300*numPlayer;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.setLivesImages();
        }
        public void setLivesImages()
        {
            int left = 0;
            if (numPlayer == 2)
                left = form.Width - 180;
            for (int i = 0; i < livesImages.Length; i++)
            {
                livesImages[i] = new PictureBox();
                livesImages[i].Size = new Size(30, 25);
                livesImages[i].Image = Properties.Resources.syringe;
                livesImages[i].Top = form.Height - 70;
                livesImages[i].Left = left;
                livesImages[i].BackColor = Color.Transparent;
                livesImages[i].SizeMode = PictureBoxSizeMode.StretchImage;
                left = left + 30;
            }
        }
        public void makeBullet(string bulletTag)
        {
            Bullet bullet = new Bullet(7, 25, pictureBox.Top - 20, pictureBox.Left + this.pictureBox.Width / 2,
               Properties.Resources.bulletDrop2, bulletTag);
            form.scene.bullets.Add(bullet);
        }
        public void keyDown(KeyEventArgs e)
        {
            if(numPlayer==1)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.goLeft = true;
                }
                if (e.KeyCode == Keys.Right)
                {
                    this.goRight = true;
                }
            }
            else
            {
                if (e.KeyCode == Keys.A)
                {
                    this.goLeft = true;
                }
                if (e.KeyCode == Keys.D)
                {
                    this.goRight = true;
                }
            }
        }

        public void keyUp(KeyEventArgs e)
        {
            if (numPlayer == 1)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.goLeft = false;
                }
                if (e.KeyCode == Keys.Right)
                {
                    this.goRight = false;
                }
                if (e.KeyCode == Keys.Space)
                {
                    addBullet();
                }
            }
            else
            {
                if (e.KeyCode == Keys.A)
                {
                    this.goLeft = false;
                }
                if (e.KeyCode == Keys.D)
                {
                    this.goRight = false;
                }
                if (e.KeyCode == Keys.Q)
                {
                    addBullet();
                }
            }
        }
        private void addBullet()
        {
            if (!this.fullPower)
                this.makeBullet("bullet");
            else
            {
                this.makeBullet("bullet1");
                this.makeBullet("bullet2");
            }
        }
        public void movePlayer()
        {
            if (this.goLeft && this.pictureBox.Location.X > 0)
            {
                 this.pictureBox.Left -= Player.playerSpeed;
            }
            if (this.goRight && this.pictureBox.Location.X < form.Width - this.pictureBox.Width)
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
