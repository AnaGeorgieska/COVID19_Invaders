using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public abstract class level
    {
        public List<Invader> sadInvaders { get; set; } //листа со ковид напаѓачи
        public List<InvaderBullet> sadInvadersBullets { get; set; } //листа со ковид оружје
        public List<PictureBox> livesList { get; set; } //листа со бонус животи
        public List<Bullet> bullets { get; set; } //листа со капки вакцина
        public List<PictureBox> powerList { get; set; } //листа со бонус моќ
        public Form1 form { get; set; } //форма за приказ
        public static Random random = new Random(); //променлива за генерирање рандом број
        public int numPlayers { get; set; } //број на играчи
        public Player player1 { get; set; } //прв играч
        public Player player2 { get; set; } //втор играч
        public level(int numPlayers, Form1 form, Player player1, Player player2)
        {
            this.form = form;
            bullets = new List<Bullet>();
            sadInvadersBullets = new List<InvaderBullet>();
            livesList = new List<PictureBox>();
            powerList = new List<PictureBox>();
            this.numPlayers = numPlayers;
            this.player1 = player1;
            this.player2 = player2;
        }
        
        public abstract void makeInvaders();
        public abstract void gameTimerTick();
        public abstract void moveEnemy();
        public void keyDown(KeyEventArgs e)
        {
            if (player1 != null)
            {
                if (e.KeyCode == Keys.Left)
                {
                    player1.GoLeft(true);
                }
                if (e.KeyCode == Keys.Right)
                {
                    player1.GoRight(true);
                }
            }
            if (player2 != null)
            {
                if (e.KeyCode == Keys.A)
                {
                    player2.GoLeft(true);
                }
                if (e.KeyCode == Keys.D)
                {
                    player2.GoRight(true);
                }
            }
        }
        public void keyUp(KeyEventArgs e)
        {
            if (player1 != null)
            {
                if (e.KeyCode == Keys.Left)
                {
                    player1.GoLeft(false);
                }
                if (e.KeyCode == Keys.Right)
                {
                    player1.GoRight(false);
                }
                if (e.KeyCode == Keys.Space)
                {
                    addBullet(player1);
                };
            }
            if (player2 != null)
            {
                if (e.KeyCode == Keys.A)
                {
                    player2.GoLeft(false);
                }
                if (e.KeyCode == Keys.D)
                {
                    player2.GoRight(false);
                }
                if (e.KeyCode == Keys.Q)
                {
                    addBullet(player2);
                }
            }
        }
        private void addBullet(Player player)
        {
            if (!player.fullPower)
              bullets.Add(  player.makeBullet("bullet"));
            else
            {
               bullets.Add( player.makeBullet("bullet1"));
               bullets.Add(player.makeBullet("bullet2"));
            }
        }
        public void movePowers()
        {
            for (int j = 0; j < powerList.Count; j++)
            {
                powerList[j].Top += 7;
                if (powerList[j].Top > form.Height)
                {
                    powerList.Remove(powerList[j]);
                    --j;
                }
                else if (player1 != null && powerList[j].Bounds.IntersectsWith(player1.pictureBox.Bounds) && !player1.fullPower)
                {
                    player1.fullPower = true;
                    powerList.Remove(powerList[j]);
                    --j;
                }
                else if (player2 != null && powerList[j].Bounds.IntersectsWith(player2.pictureBox.Bounds) && !player2.fullPower)
                {
                    player2.fullPower = true;
                    powerList.Remove(powerList[j]);
                    --j;
                }
            }
        }
        public void moveLives()
        {
            for (int j = 0; j < livesList.Count; j++)
            {
                livesList[j].Top += 7;
                if (livesList[j].Top > form.Height)
                {
                    livesList.Remove(livesList[j]);
                    --j;
                }
                else if (player1 != null && livesList[j].Bounds.IntersectsWith(player1.pictureBox.Bounds) && player1.lives < 5)
                {
                    player1.lives++;
                    livesList.Remove(livesList[j]);
                    --j;
                }
                else if (player2 != null && livesList[j].Bounds.IntersectsWith(player2.pictureBox.Bounds) && player2.lives < 5)
                {
                    player2.lives++;
                    livesList.Remove(livesList[j]);
                    --j;
                }
            }
        }
        public void moveEnemyBullet()
        {
            for (int j = 0; j < sadInvadersBullets.Count; j++)
            {
                sadInvadersBullets[j].move();
                if (sadInvadersBullets[j].pictureBox.Top > form.Height)
                {
                    sadInvadersBullets.Remove(sadInvadersBullets[j]);
                }
                else if (player1 != null && sadInvadersBullets[j].pictureBox.Bounds.IntersectsWith(player1.pictureBox.Bounds))
                {
                    sadInvadersBullets.Remove(sadInvadersBullets[j]);
                    --j;
                    --player1.lives;
                    if (player1.lives == 0)
                        player1 = null;
                }
                else if (player2 != null && sadInvadersBullets[j].pictureBox.Bounds.IntersectsWith(player2.pictureBox.Bounds))
                {
                    sadInvadersBullets.Remove(sadInvadersBullets[j]);
                    --j;
                    --player2.lives;
                    if (player2.lives == 0)
                        player2 = null;
                }
            }
        }
        public void addCovidBullet()
        {
            int temp = random.Next(sadInvaders.Count * (8 - Scene.level));
            if (temp < sadInvaders.Count && sadInvaders[temp].pictureBox.Location.X > 0)
            {
                InvaderBullet invaderBullet = new InvaderBullet(30, 30, sadInvaders[temp].pictureBox.Location.Y + sadInvaders[temp].pictureBox.Height,
                    sadInvaders[temp].pictureBox.Location.X + random.Next(sadInvaders[temp].pictureBox.Width), Properties.Resources.covidBullet);
                sadInvadersBullets.Add(invaderBullet);
            }
        }
        public void moveBullet()
        {
            for (int j = 0; j < bullets.Count; j++)
            {
                bullets[j].move();
                if (bullets[j].pictureBox.Top < 15)
                {
                    bullets.Remove(bullets[j]);
                    --j;
                }
            }
        }
        public void dontMove()
        {
            if (player1 != null)
            {
                player1.goLeft = false;
                player1.goRight = false;
            }
            if (player2 != null)
            {
                player2.goLeft = false;
                player2.goRight = false;
            }
        }
        public void paint(PaintEventArgs e)
        {
            foreach (Invader invader in sadInvaders)
            {
                invader.draw(e.Graphics);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.draw(e.Graphics);
            }
            foreach (InvaderBullet bullet in sadInvadersBullets)
            {
                bullet.draw(e.Graphics);
            }
            draw(livesList, e.Graphics);
            draw(powerList, e.Graphics);
            if (player1 != null)
                player1.draw(e.Graphics);
            if (player2 != null)
                player2.draw(e.Graphics);
        }
        public void draw(List<PictureBox> list, Graphics g)
        {
            foreach (PictureBox p in list)
            {
                g.DrawImage(p.Image, p.Location.X, p.Location.Y, p.Width, p.Height);
            }
        }
        public void removeAll()
        {
            if (sadInvaders != null)
                remove(sadInvaders);
            remove(bullets);
            remove(sadInvadersBullets);
            remove(livesList);
            remove(powerList);
        }
        public void remove<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list.Remove(list[i]);
                --i;
            }
        }
        public void timer1Tick()
        {
            int temp = random.Next(form.Width);
            if (temp % 2 == 0)
            {
                addLife(temp);
            }
            else if (temp % 2 == 1)
            {
                addPower(temp);
            }
        }
        public void addPower(int X)
        {
            addDrop(20, 25, X, Properties.Resources.powerDrop, powerList);
        }
        public void addLife(int X)
        {
            addDrop(40, 25, X, Properties.Resources.lifeDrop, livesList);
        }
        public void addDrop(int width, int height, int left, Image img, List<PictureBox> list)
        {
            PictureBox drop = new PictureBox();
            drop.Size = new Size(width, height);
            drop.Image = img;
            drop.Top = 0;
            drop.Left = left;
            drop.BackColor = Color.Transparent;
            drop.SizeMode = PictureBoxSizeMode.StretchImage;
            list.Add(drop);
        }
    }
}
