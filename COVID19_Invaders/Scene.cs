using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
   public class Scene
    {
        public int score { get; set; }
        public static int level { get; set; }

        public static Random random = new Random();
        public List<Invader> sadInvaders { get; set; }
        public List<InvaderBullet> sadInvadersBullets { get; set; }
        public List<PictureBox> livesList { get; set; }
        public List<Bullet> bullets { get; set; }
        public List<PictureBox> powerList { get; set; }
        public Form1 form { get; set; }
        public int numPlayers { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public Scene(int numPlayers, Form1 form)
        {
            this.numPlayers = numPlayers;
            this.form = form;
            bullets = new List<Bullet>();
            sadInvadersBullets = new List<InvaderBullet>();
            livesList = new List<PictureBox>();
            powerList = new List<PictureBox>();
            this.numPlayers = numPlayers;
            player1 = new Player(form, 1);
            player2 = null;
            if (numPlayers == 2)
                player2 = new Player(form, 2);
            gameSetUp();
        }
        public void gameSetUp()
        {
            player1 = new Player(form, 1);
            player2 = null;
            if (numPlayers == 2)
                player2 = new Player(form, 2);
            level = 1;
            this.score = 0;
            removeAll();
            makeInvaders();
        }
        public void levelUp()
        {
            if (player1 != null)
                player1.fullPower = false;
            if (player2 != null)
                player2.fullPower = false;
            score = 0;
            level += 1;
            removeAll();
            makeInvaders();
        }
        private void makeInvaders()
        {
            sadInvaders = new List<Invader>();
            int left = 0;
            for (int i = 0; i < 50; i++)
            {
                Image img = null;
                if (level == 1) img = Properties.Resources.sadFace;
                else if (level == 2) img = Properties.Resources.sadFace21;
                else img = Properties.Resources.sadFace31;
                Invader temp = new Invader(60, 50, 5, left, img);
                sadInvaders.Add(temp);
                left = left - 80;
            }
        }
        public void keyDown( KeyEventArgs e)
        {
            if (player1 != null)
            {
                player1.keyDown(e);
            }
            if (player2 != null)
            {
                player2.keyDown(e);
            }
        }
        public void keyUp( KeyEventArgs e)
        {
            if (player1 != null)
            {
                player1.keyUp(e);
            }
            if (player2 != null)
            {
                player2.keyUp(e);
            }
        }
        public void gameTimerTick()
        {
            if (player1 != null)
                player1.movePlayer();
            if (player2 != null)
                player2.movePlayer();
            moveEnemy();
            moveBullet();
            addCovidBullet();
            moveEnemyBullet();
            moveLives();
            movePowers();
            if (score == 10)
            {
                Invader.enemySpeed = 8 + 2 * level;
            }
            if (score == 50 && level == 3)
            {
                form.gameOver("Happiness Found, Keep it safe!");
            }
            else if (score == 50)
            {
                form.nextLevelForm();
            }
            if (player1 == null && player2 == null)
                form.gameOver("You've been infected by COVID.");

        }
        private void movePowers()
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
        private void moveLives()
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
        private void moveEnemyBullet()
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
            int temp = random.Next(sadInvaders.Count * (8 - level));
            if (temp < sadInvaders.Count && sadInvaders[temp].pictureBox.Location.X > 0)
            {
                InvaderBullet invaderBullet = new InvaderBullet(30, 30, sadInvaders[temp].pictureBox.Location.Y,
                    sadInvaders[temp].pictureBox.Location.X + sadInvaders[temp].pictureBox.Width, Properties.Resources.covidBullet);
                sadInvadersBullets.Add(invaderBullet);
            }
        }
        private void moveBullet()
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
        private void moveEnemy()
        {
            for (int i = 0; i < sadInvaders.Count; i++)
            {
                sadInvaders[i].moveInvader(form.Width);
                enemyIntersectsPlayer(sadInvaders[i].pictureBox, player1);
                enemyIntersectsPlayer(sadInvaders[i].pictureBox, player2);
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (bullets[j].pictureBox.Bounds.IntersectsWith(sadInvaders[i].pictureBox.Bounds))
                    {
                        sadInvaders.Remove(sadInvaders[i]);
                        bullets.Remove(bullets[j]);
                        --i;
                        score += 1;
                        break;
                    }
                }
            }
        }
        public void enemyIntersectsPlayer(PictureBox enemy, Player player)
        {
            if (player != null)
                if (enemy.Bounds.IntersectsWith(player.pictureBox.Bounds))
                {
                    player = null;
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
        public void paint( PaintEventArgs e)
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
        private void draw(List<PictureBox> list, Graphics g)
        {
            foreach (PictureBox p in list)
            {
                g.DrawImage(p.Image, p.Location.X, p.Location.Y, p.Width, p.Height);
            }
        }
        private void removeAll()
        {
            if (sadInvaders != null)
                remove(sadInvaders);
            remove(bullets);
            remove(sadInvadersBullets);
            remove(livesList);
            remove(powerList);
        }
        private void remove<T>(List<T> list)
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
        private void addPower(int X)
        {
            addDrop(20, 25, X, Properties.Resources.powerDrop, powerList);
        }
        private void addLife(int X)
        {
            addDrop(40, 25, X, Properties.Resources.lifeDrop, livesList);
        }
        private void addDrop(int width, int height, int left, Image img, List<PictureBox> list)
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
