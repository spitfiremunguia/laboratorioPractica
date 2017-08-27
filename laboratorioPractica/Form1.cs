using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Id3Lib;

namespace laboratorioPractica
{
    public partial class Form1 : Form
    {
        List<Song> mainList = new List<Song>();
        public Form1()
        {
            InitializeComponent();
            //tabControl1.Dock = DockStyle.Fill;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnSearchFiles = new OpenFileDialog();
            opnSearchFiles.ShowDialog();
            //opnSearchFiles.Filter=
            //frmsongManager newsongManager = new frmsongManager();
            //newsongManager.ShowDialog();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            TabPage t = new TabPage();
            
        }
        //private void pictureBox1_MouseHover(object sender, EventArgs e)
        //{
        //    pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Añadir_64;
        //}
        //private void pictureBox1_MouseLeave(object sender, EventArgs e)
        //{
        //    pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Añadir_Filled_50;
        //}
    }
}
