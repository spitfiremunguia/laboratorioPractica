using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratorioPractica
{
    public partial class frmsongManager : Form
    {
        public frmsongManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSongName = this.textBox1.Text;
            string strSongDuration = this.textBox2.Text;
            double flag;
            try
            {
                if (!double.TryParse(strSongDuration, out flag))
                {
                    throw new FormatException();
                }
            }
            catch
            {
                MessageBox.Show("lol", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
         
            
        }
    }
}
