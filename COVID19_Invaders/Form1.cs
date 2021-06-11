using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
    public partial class Form1 : Form
    {
        public int numPlayers { get; set; }
        public bool pause { get; set; }
        public Scene scene { get; set; }
        public Form1(int numPlayers)
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.numPlayers = numPlayers;
            scene = new Scene(this.numPlayers, this);
            gameTimer.Start();
            timer1.Start();
            this.pause = false;
            pbPause.BackColor = Color.Transparent;
            pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
            gameSetUp();
        }
        public void gameSetUp()
        {
            scene.gameSetUp();
            gameTimer.Start();
            timer1.Start();
            this.pause = false;
            pbPause.BackColor = Color.Transparent;
            pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public void levelUp()
        {
            scene.levelUp();
            gameTimer.Start();
            timer1.Start();
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            scene.keyDown(e);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            scene.keyUp(e);
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + scene.score;
            scene.gameTimerTick();
            Invalidate();
        }
       
        public void nextLevelForm()
        {
            scene.dontMove();
            gameTimer.Stop();
            timer1.Stop();
            nextLevel form = new nextLevel(this);
            form.Show();
        }
        public void gameOver(string v)
        {
            scene.dontMove();
            gameTimer.Stop();
            timer1.Stop();
            gameOver form = new gameOver(v, this);
            form.Show();
        }
       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            scene.paint(e);
        }
        
        private void Player_Click(object sender, EventArgs e)
        {

        }
               
        private void timer1_Tick(object sender, EventArgs e)
        {
            scene.timer1Tick();
        }     
        
        private void pbPause_Click(object sender, EventArgs e)
        {
            if (!pause)
            {
                timer1.Stop();
                gameTimer.Stop();
                pbPause.Image = Properties.Resources.start;
                pbPause.BackColor = Color.Transparent;
                pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
                pause = true;
            }
            else
            {
                timer1.Start();
                gameTimer.Start();
                pbPause.Image = Properties.Resources.pause;
                pbPause.BackColor = Color.Transparent;
                pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
                pause = false;
            }
        }
    }
}
