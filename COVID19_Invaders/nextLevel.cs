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
    public partial class nextLevel : Form
    {
        public Form1 form { get; set; }
        public nextLevel(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            load();
        }
        private void load()
        {
            label1.BackColor = Color.Transparent;
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            form.Close();
            this.Close();
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            form.levelUp();
            this.Close();
        }

        private void nextLevel_KeyPress(object sender, KeyPressEventArgs e)
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
