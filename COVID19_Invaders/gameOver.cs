using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID19_Invaders
{
    public partial class gameOver : Form
    {
        public string description { get; set; }
        public Form1 form { get; set; }
        public gameOver(string desc, Form1 form)
        {
            InitializeComponent();
            this.description = desc;
            this.form = form;
            load();
        }
        private void load()
        {
            Score bestScore = BinaryDeserializeScore();
            if(bestScore.score<form.scene.score.score)
                bestScore = form.scene.score;
            label2.Text = description;
            label2.BackColor = Color.Transparent;
            txtscore.Text = string.Format("Best score: {0}\nYour score: {1}",bestScore.score, form.scene.score.score);
            txtscore.BackColor = Color.Transparent;
            BinarySerializeScore(bestScore);
        }
        
        private  void BinarySerializeScore(Score BestScore)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            using (FileStream str = File.Create(path + "\\BestScore.bs"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, BestScore);
            }
        }
        
        private  Score BinaryDeserializeScore()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Score BestScore = null;
            try
            {
                using (FileStream str = File.OpenRead(path + "\\BestScore.bs"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    BestScore= (Score)bf.Deserialize(str);
                }
                File.Delete(path + "\\BestScore.bs");
                return BestScore;
            }
            catch (FileNotFoundException)
            {
                return new Score();
            }
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            form.gameSetUp();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            form.Close();
            this.Close();
        }

        private void gameOver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ActiveControl is Button && e.KeyChar == (char)Keys.Space)
            {
                var button = this.ActiveControl;
                button.Enabled = false;
                Application.DoEvents();
                button.Enabled = true;
                button.Focus();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
