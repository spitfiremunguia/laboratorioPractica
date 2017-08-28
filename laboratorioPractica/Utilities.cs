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
        public static string mainFilePath = @"C:\lilPlay\";
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
        public static TreeView UpdateTreeview(TreeView aTreeView,List<string>allPlaylistNames)
        {
            foreach(string playlistName in allPlaylistNames)
            {
                TreeNode n = new TreeNode(playlistName,0,0);
                aTreeView.Nodes.Add(n);
            }
            
            return aTreeView;
        }
        public static  List<string>  getAllPlaylistbins(string mainFilePath)
        {
            List<string> allPlaylistNames = new List<string>();
            
            foreach (string directory in Directory.GetDirectories(mainFilePath))
            {
                allPlaylistNames.Add( new DirectoryInfo(directory).Name);

            }
            return allPlaylistNames;
            
        }
        public static void CreatePlaylistDirectory(string name)
        {
            if (!Directory.Exists(Utilities.mainFilePath + name))
                Directory.CreateDirectory(Utilities.mainFilePath + name);
            else
                MessageBox.Show("Dude, there is already a playlist with that name","lol",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //also creates the files that store the properties of the playlist.

        }
        public static void createlilplyfiles(string mainFilePath,string plyName,string description)
        {
            List<string> allFolders = Directory.GetDirectories(mainFilePath + plyName).ToList();
            foreach(string s in allFolders)
            {
                var a = new DirectoryInfo(s);
                if (s == a.Name)
                {
                    File.Create(mainFilePath + plyName + "\\Description.lil");
                    File.Create(mainFilePath + plyName + "\\" + plyName + ".lil");
                    break;
                }
            }   
            
            StreamWriter r = new StreamWriter(mainFilePath + plyName + "\\Description.lil");
            r.Write(description);
            r.Close();
        }
        public static void sortPlaylistByName(bool upWard,TreeView t)
        {
          
            List<string> l = new List<string>();
            foreach(TreeNode n in t.Nodes)
            {
                l.Add(n.Text);
            }
            if(upWard)
            {
                l.Sort();
            }
            else
            {
                l.Reverse();
            }
            t.Nodes.Clear();
            Utilities.UpdateTreeview(t,l);
        }
        public static void searchPlayList(string search,TreeNodeCollection allNodesNames,TreeView t)
        {
            List<string> names = new List<string>();
            foreach(TreeNode n in allNodesNames)
            {
                names.Add(n.Text);
            }
            int index = names.BinarySearch(search);
            if(index>=0)
            {
                t.SelectedNode = t.Nodes[index];
                t.SelectedNode.BackColor = System.Drawing.Color.Blue;
                
            }

        }
       
    }
}
