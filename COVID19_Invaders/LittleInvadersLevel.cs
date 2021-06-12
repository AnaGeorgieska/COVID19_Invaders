using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19_Invaders
{
    public class LittleInvadersLevel : level
    {
        public LittleInvadersLevel(int numPlayers, Form1 form, Player player1, Player player2) 
            : base(numPlayers, form, player1, player2)
        { }
        public override void makeInvaders()
        {
            sadInvaders = new List<Invader>();
            int left = 0;
            int top = 5;
                for (int i = 0; i < 50; i++)
                {
                    Image img = null;
                    if (Scene.level == 1) img = Properties.Resources.sadFace;
                    else if (Scene.level == 2) img = Properties.Resources.sadFace21;
                    else img = Properties.Resources.sadFace31;
                    if (Scene.level == 3)
                    {
                        left = random.Next(form.Width * 2) - form.Width;
                        top = -random.Next(form.Height);
                    }
                    Invader temp = new Invader(60, 50, top, left, img);
                    sadInvaders.Add(temp);
                    left = left - 80;
                }            
        }
        public override void gameTimerTick()
        {
            if (player1 != null)
                player1.movePlayer();
            if (player2 != null)
                player2.movePlayer();
            if (sadInvaders.Count < 1)
            {
                form.nextLevelForm();
            }

            moveEnemy();
            moveBullet();
            addCovidBullet();
            moveEnemyBullet();
            moveLives();
            movePowers();
            if (Scene.score == 10 || Scene.score == 60 || Scene.score == 110)
            {
                Invader.enemySpeed = 8 + 2 * Scene.level;
            }
            
            if (player1 == null && player2 == null)
                form.gameOver("You've been infected by COVID.");

        }

        public override void moveEnemy()
        {
            for (int i = sadInvaders.Count - 1; i >= 0; i--)
            {
                    sadInvaders[i].moveInvader(form.Width);
                    if (sadInvaders[i].pictureBox.Top > form.Height)
                    {
                        sadInvaders.Remove(sadInvaders[i]);
                        break;
                    }
                    else
                    {
                        if (player1 != null)
                        {
                            if (sadInvaders[i].pictureBox.Bounds.IntersectsWith(player1.pictureBox.Bounds))
                            {
                                player1 = null;
                            }
                        }
                        if (player2 != null)
                        {
                            if (sadInvaders[i].pictureBox.Bounds.IntersectsWith(player2.pictureBox.Bounds))
                            {
                                player2 = null;
                            }
                        }
                    }
                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    if (bullets[j].pictureBox.Bounds.IntersectsWith(sadInvaders[i].pictureBox.Bounds))
                    {
                        bullets.Remove(bullets[j]);
                        sadInvaders.Remove(sadInvaders[i]);
                        Scene.score += 1;
                        break;
                    }
                }
            }
            
        }

    }
}
