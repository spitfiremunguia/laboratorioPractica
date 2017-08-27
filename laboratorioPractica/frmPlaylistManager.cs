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
            try
            {
                if (playlistName==string.Empty)
                {
                    throw new FormatException();
                }
                else
                {
                    Playlist ply = new Playlist(playlistName, playlistDescription);
                    Utilities.generalTree= Utilities.UpdateTreeview(t1,ply);
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("lol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
