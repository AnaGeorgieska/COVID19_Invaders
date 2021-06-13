using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19_Invaders
{
   public  class BigInvadersLevel : level
    {
        public BigInvadersLevel(int numPlayers, Form1 form, Player player1, Player player2)
            : base(numPlayers, form, player1, player2)
        { }
        public override void makeInvaders()
        {
            sadInvaders = new List<Invader>();   
                Invader temp = new Invader(200, 200, 0, 100, Properties.Resources.sadFaceBig);
                sadInvaders.Add(temp);
                if (Scene.level == 5)
                {
                    Invader temp2 = new Invader(200, 200, 0, 600, Properties.Resources.sadFaceBig2);
                    sadInvaders.Add(temp2);
                }
        }
        public override void gameTimerTick()
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
           
            if (sadInvaders.Count < 1 && Scene.level == 5)
            {
                form.gameOver("Happiness found, keep it safe!");
            }
            else if (sadInvaders.Count < 1)
            {
                form.nextLevelForm();
            }
            if (player1 == null && player2 == null)
                form.gameOver("You've been infected by COVID.");
        }

        public override void moveEnemy()
        {
            for (int i = sadInvaders.Count - 1; i >= 0; i--)
            {
                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    if (bullets[j].pictureBox.Bounds.IntersectsWith(sadInvaders[i].pictureBox.Bounds))
                    {
                        bullets.Remove(bullets[j]);
                        --sadInvaders[i].strength;
                            if (sadInvaders[i].strength == 0)
                                sadInvaders.Remove(sadInvaders[i]);

                        form.scene.score.increment();
                        break;
                    }
                }
            }
            if ( sadInvaders.Count > 0)
            {
                if (sadInvaders.Count > 1)
                {
                    sadInvaders[0].moveInvaderBig();
                    sadInvaders[1].moveInvaderBig();
                }
                else
                {
                    sadInvaders[0].moveInvaderBig();
                }
            }
        }

    }
}
