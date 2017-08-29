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
using laboratorioPractica.Properties;

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

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = laboratorioPractica.Properties.Resources.icons8_Eliminar_imagen_48__2_;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_Más_50;
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = laboratorioPractica.Properties.Resources.icons8_Búsqueda_48__2_;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = laboratorioPractica.Properties.Resources.icons8_Búsqueda_48;
        }
        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox6.Image = laboratorioPractica.Properties.Resources.icons8_Búsqueda_48__2_;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = laboratorioPractica.Properties.Resources.icons8_Búsqueda_48;
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
                Directory.Delete(Utilities.mainFilePath + treeView1.SelectedNode.Text,true);
                
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
            
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            foreach(TreeNode n in treeView1.Nodes)
            {
                n.BackColor = Color.Gainsboro;
                treeView1.Focus();
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
                d.BackgroundColor = Color.Gainsboro;
                newPage.ToolTipText = "Right click to close";
                myTabControl1.ShowToolTips=true;
                newPage.Controls.Add(d);
                myTabControl1.TabPages.Add(newPage);
                d.Show();
                Utilities.createPlaylistDisplay(d);
                string playListPath = Utilities.mainFilePath + treeView1.SelectedNode.Text + "\\" + treeView1.SelectedNode.Text + ".lil";
                Utilities.GetAllSongsFromDictionary(playListPath, d);
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
                n.BackColor = Color.Gainsboro;
                treeView1.Focus();
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
