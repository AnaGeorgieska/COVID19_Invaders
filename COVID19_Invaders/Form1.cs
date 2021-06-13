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
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public int numPlayers { get; set; }
        public bool pause { get; set; }
        public bool music { get; set; }
        public Scene scene { get; set; }
        public Form1(int numPlayers)
        {
            InitializeComponent();
            wplayer.URL = "Epic Adventure Music.mp3";
            wplayer.controls.play();
            pbMusic.BackColor = Color.Transparent;
            pbMusic.SizeMode = PictureBoxSizeMode.StretchImage;
            this.music = true;
            DoubleBuffered = true;
            this.numPlayers = numPlayers;
            scene = new Scene(this.numPlayers, this);
            gameTimer.Start();
            timer1.Start();
            this.pause = false;
            txtScore.ForeColor = Color.Transparent;
            pbPause.BackColor = Color.Transparent;
            pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
            gameSetUp();
        }
        public void gameSetUp()
        {
            scene = new Scene(this.numPlayers, this);
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
            scene.gameTimerTick();
            txtScore.Text = "Score: " + scene.score.score;
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
                wplayer.controls.pause();
                pbPause.Image = Properties.Resources.start;
                pbPause.BackColor = Color.Transparent;
                pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
                pause = true;
            }
            else
            {
                timer1.Start();
                gameTimer.Start();
                wplayer.controls.play();
                pbPause.Image = Properties.Resources.pause;
                pbPause.BackColor = Color.Transparent;
                pbPause.SizeMode = PictureBoxSizeMode.StretchImage;
                pause = false;
            }
        }

        private void pbMusic_Click(object sender, EventArgs e)
        {
            if(music)
            {
                wplayer.controls.pause();
                music = false;
            }
            else
            {
                wplayer.controls.play();
                music = true;
            }
        }
    }
}
