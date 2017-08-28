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
using ICSharpCode;
using Mp3Lib;
using System.IO;

namespace laboratorioPractica
{
    public partial class Form1 : Form
    {
        List<Song> mainList = new List<Song>();
        public Form1()
        {
            InitializeComponent();
            Utilities.createMainBin(Utilities.mainFilePath);
          
            treeView1 = Utilities.UpdateTreeview(treeView1,Utilities.getAllPlaylistbins(Utilities.mainFilePath));
           
        }
       
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            //OpenFileDialog opnSearchFiles = new OpenFileDialog();
            //opnSearchFiles.Filter = "Music(.mp3) | *.mp3 | ALL Files(*.*) | *.* ";
            //opnSearchFiles.ShowDialog();

            frmPlaylistManager playlistManager = new frmPlaylistManager(treeView1);
            playlistManager.ShowDialog();
            treeView1.Nodes.Clear();
            treeView1= Utilities.UpdateTreeview( treeView1,Utilities.getAllPlaylistbins(Utilities.mainFilePath));
          
        }
#region //buttons events
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Más_64;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = laboratorioPractica.Properties.Resources.icons8_Eliminar_imagen_48;
        }

       
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox4.Image = laboratorioPractica.Properties.Resources.icons8_Clasificar_50;
        }

        private void pictureBox4_MouseHover(object sender, MouseEventArgs e)
        {
            //pictureBox4.Image = laboratorioPractica.Properties.Resources.icons8_Clasificar_50__2_;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = laboratorioPractica.Properties.Resources.icons8_Eliminar_imagen_48__2_;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Más_50;
        }

       
        #endregion

        private void upwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.sortPlaylistByName(true,treeView1);
        }

        private void downwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.sortPlaylistByName(false, treeView1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode!=null)
            {
                Directory.Delete(Utilities.mainFilePath + treeView1.SelectedNode.Text);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
            
            
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            foreach(TreeNode n in treeView1.Nodes)
            {
                n.BackColor = Color.Black;
            }
            if(treeView1.SelectedNode!=null)
            {
                
                TabPage newPage = new TabPage();
                newPage.Text = treeView1.SelectedNode.Text;
                newPage.BackColor = Color.Black;
                DataGridView d = new DataGridView();
                d.Height = newPage.Height * 2;
                d.Width = newPage.Width *2;
                d.Dock = DockStyle.Fill;
                d.BackgroundColor = Color.Black;

                newPage.Controls.Add(d);
                myTabControl1.TabPages.Add(newPage);
                d.Show();
            }
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
            Utilities.searchPlayList(textBox1.Text,treeView1.Nodes,treeView1);
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in treeView1.Nodes)
            {
                n.BackColor = Color.Black;
            }
            
            
        }
    }
}
