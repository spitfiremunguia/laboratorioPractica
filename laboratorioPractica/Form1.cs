using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Utilities.CreateMainBin(Utilities.mainFilePath);            
            treeView1 = Utilities.UpdateTreeview(treeView1,Utilities.GetAllPlaylistbins(Utilities.mainFilePath));
            myTabControl1.TabPages.RemoveAt(0);
            axWindowsMediaPlayer2.Ctlenabled = true;
            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)axWindowsMediaPlayer2.Ctlcontrols;


        }
       
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
           
            frmPlaylistManager playlistManager = new frmPlaylistManager(treeView1);
            playlistManager.ShowDialog();
            treeView1.Nodes.Clear();
            treeView1= Utilities.UpdateTreeview( treeView1,Utilities.GetAllPlaylistbins(Utilities.mainFilePath));

          
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
            Utilities.SortPlaylistByName(true,treeView1);
        }

        private void downwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.SortPlaylistByName(false, treeView1);
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
                d.Name = treeView1.SelectedNode.Text;
                d.Height = newPage.Height * 2;
                d.Width = newPage.Width *2;
                d.Dock = DockStyle.Fill;
                d.EditMode = DataGridViewEditMode.EditProgrammatically;
                d.AllowUserToResizeColumns = false;
                d.RowHeadersVisible = false;
                d.AllowUserToResizeRows = false;
                d.AllowUserToResizeColumns = false;
                d.AllowUserToOrderColumns = false;
                d.CellMouseDoubleClick += CellMouseDoubleClick;
                newPage.ToolTipText = "Right click to see more options";
                myTabControl1.ShowToolTips=true;
                newPage.Controls.Add(d);
                myTabControl1.TabPages.Add(newPage);
                d.Show();
                Utilities.CreatePlaylistDisplay(d);
                string playListPath = Utilities.mainFilePath + treeView1.SelectedNode.Text + "\\" + treeView1.SelectedNode.Text + ".lil";
                Utilities.GetAllSongsFromDictionary(playListPath, d);
            }
           
        }
        
        public void CellMouseDoubleClick(object sender, EventArgs e)
        {
            DataGridView d = sender as DataGridView;
            DataGridViewCell c = d.SelectedCells[0];
            if(c.ColumnIndex==2&&c.Value!=null)
            {
                string songPath = Utilities.SearchSongPath(d.Name, c.Value.ToString());
                if(songPath!=string.Empty)
                {
                    axWindowsMediaPlayer2.URL = (songPath);
                    TagLib.File f = TagLib.File.Create(songPath, TagLib.ReadStyle.Average);
                    if (f.Tag.Pictures.Length == 0)
                    {
                        f.Dispose();
                        pictureBox7.Image = Properties.Resources.icons8_No_hay_entrada_Filled_50;
                        return;
                    }
                    MemoryStream ms = new MemoryStream(f.Tag.Pictures[0].Data.Data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    pictureBox7.Image = image;
                    f.Dispose();
                }
              
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Utilities.SearchPlayList(textBox1.Text,treeView1.Nodes,treeView1);
        }

       
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Utilities.SortPlayList(true, myTabControl1.SelectedTab.Controls[0] as DataGridView, myTabControl1.SelectedTab.Text, true);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Utilities.SortPlayList(false, myTabControl1.SelectedTab.Controls[0] as DataGridView, myTabControl1.SelectedTab.Text, true);
        }

        private void upwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utilities.SortPlayList(true, myTabControl1.SelectedTab.Controls[0] as DataGridView, myTabControl1.SelectedTab.Text,false);
        }

        private void downwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utilities.SortPlayList(false, myTabControl1.SelectedTab.Controls[0] as DataGridView, myTabControl1.SelectedTab.Text, false);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Utilities.SearchSongByName(textBox2.Text,(DataGridView)myTabControl1.SelectedTab.Controls[0]);
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            treeView1.HideSelection = true;
            
        }

        private void axWindowsMediaPlayer2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            MessageBox.Show("");
        }
    }
}
