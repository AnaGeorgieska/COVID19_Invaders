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
            label2.Text = description;
            label2.BackColor = Color.Transparent;
            txtscore.Text = string.Format("Your score: {0}", Scene.score);
            txtscore.BackColor = Color.Transparent;
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
    }
}
