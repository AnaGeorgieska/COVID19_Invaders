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
        public static int score { get; set; }
        public static int level { get; set; }
        public Form1 form { get; set; }
        public level Level { get; set; }
        public int numPlayers { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public Scene(int numPlayers, Form1 form)
        {
            this.numPlayers = numPlayers;
            this.form = form;
            player1 = new Player( 1, form.Width,form.Height);
            player2 = null;
            if (numPlayers == 2)
                player2 = new Player(2, form.Width, form.Height);
            level = 1;
            score = 0;
            Level = new LittleInvadersLevel(numPlayers, form, player1, player2);
            Level.makeInvaders();
        }
        
        public void levelUp()
        {
            if (Level.player1 != null)
                Level.player1.fullPower = false;
            if (Level.player2 != null)
                Level.player2.fullPower = false;
            player1 = Level.player1;
            player2 = Level.player2;
            level += 1;
            Level.removeAll();
            if(level==2 || level==3)
            {
                 Level = new LittleInvadersLevel(numPlayers, form, player1, player2);
            }
            else Level = new BigInvadersLevel(numPlayers, form, player1, player2);
            Level.makeInvaders();
        }
        
        public void keyDown( KeyEventArgs e)
        {
            Level.keyDown(e);
        }
        public void keyUp( KeyEventArgs e)
        {
            Level.keyUp(e);
        }
        public void gameTimerTick()
        {
            Level.gameTimerTick();

        }
        
        public void dontMove()
        {
            Level.dontMove();
        }
        public void paint( PaintEventArgs e)
        {
            Level.paint(e);
        }
              
        public void timer1Tick()
        {
            Level.timer1Tick();
        }
        
   }
}
