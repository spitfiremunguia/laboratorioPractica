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
    public partial class frmPlaylistManager : Form
    {
        TreeView t1 = new TreeView();
        public frmPlaylistManager(TreeView t)
        {
            InitializeComponent();
            t1 = t;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string playlistName = this.textBox1.Text;
            string playlistDescription = this.textBox2.Text;
           
               
                Utilities.CreatePlaylistDirectory(playlistName);
                Utilities.createlilplyfiles(Utilities.mainFilePath, playlistName, playlistDescription);
                this.Close();
                
            
            
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_De_acuerdo_50;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = laboratorioPractica.Properties.Resources.icons8_De_acuerdo_50__1_;
        }
    }
}
