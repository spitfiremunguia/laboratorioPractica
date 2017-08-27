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
using System.IO;

namespace laboratorioPractica
{
    public partial class Form1 : Form
    {
        List<Song> mainList = new List<Song>();
        string mainFilePath = @"C:\lilPlay";
        public Form1()
        {
            InitializeComponent();
            Utilities.createMainBin(mainFilePath);
            
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
        /// <summary>
        /// add new playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

            frmPlaylistManager playlistManager = new frmPlaylistManager(treeView1);
            playlistManager.ShowDialog();
            var a= Utilities.generalTree;
            treeView1 = Utilities.generalTree;
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Más_50;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Más_64;
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
