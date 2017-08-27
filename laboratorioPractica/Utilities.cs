using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratorioPractica
{
    public class Utilities
    {
       public static TreeView generalTree = new TreeView();
       public static void createMainBin(string mainFilePath)
        {
            if(!Directory.Exists(mainFilePath))
            {
                MessageBox.Show("A new directory will be created on your C disk","Hello",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Directory.CreateDirectory(mainFilePath);
            }
            
        }
        public static Playlist CreatePlaylist(string name, string description)
        {
            return new Playlist(name, description);
        }
        public static TreeView UpdateTreeview(TreeView aTreeView,Playlist aNewPlaylist)
        {
            TreeNode n = new TreeNode(aNewPlaylist.GetName());
            aTreeView.Nodes.Add(n);
            return aTreeView;
        }
       
    }
}
