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
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
        }

        private void btn1Player_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(1);
            form.Show();
          // this.Close();
        }

        private void btn2Player_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(2);
            form.Show();
          //  this.Close();
        }
    }
}
