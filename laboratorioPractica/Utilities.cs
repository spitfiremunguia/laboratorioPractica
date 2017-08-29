using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Id3Lib;
using ICSharpCode;
using Mp3Lib;
using TagLib;
using TagLib.Mpeg;
using System.Globalization;

namespace laboratorioPractica
{
    public class Utilities
    {
        public static TreeView generalTree = new TreeView();
        public static string mainFilePath = @"C:\lilPlay\";
        public static void createMainBin(string mainFilePath)
        {
            if (!Directory.Exists(mainFilePath))
            {
                MessageBox.Show("A new directory will be created on your C disk", "Hello", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Directory.CreateDirectory(mainFilePath);
                
            }
        }
     
        public static TreeView UpdateTreeview(TreeView aTreeView, List<string> allPlaylistNames)
        {
            foreach (string playlistName in allPlaylistNames)
            {
                TreeNode n = new TreeNode(playlistName, 0, 0);
                aTreeView.Nodes.Add(n);
            }
            return aTreeView;
        }
        public static List<string> getAllPlaylistbins(string mainFilePath)
        {
            List<string> allPlaylistNames = new List<string>();

            foreach (string directory in Directory.GetDirectories(mainFilePath))
            {
                allPlaylistNames.Add(new DirectoryInfo(directory).Name);
            }
            return allPlaylistNames;

        }
        public static void CreatePlaylistDirectory(string name)
        {
            if (!Directory.Exists(Utilities.mainFilePath + name))
            {
                Directory.CreateDirectory(Utilities.mainFilePath + name);
            }
            else
            {
                MessageBox.Show("Dude, there is already a playlist with that name", "lol", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void createlilplyfiles(string mainFilePath, string plyName, string description)
        {
            List<string> allFolders = Directory.GetDirectories(mainFilePath).ToList();
            foreach (string s in allFolders)
            {
                var a = new DirectoryInfo(s);
                
                if (s == a.FullName)
                {
                    System.IO.File.Create(mainFilePath + plyName + "\\" +plyName + ".lil").Dispose();
                    break;
                }
            } 
        }
        public static void sortPlaylistByName(bool upWard, TreeView t)
        {

            List<string> l = new List<string>();
            foreach (TreeNode n in t.Nodes)
            {
                l.Add(n.Text);
            }
            if (upWard)
            {
                l.Sort();
            }
            else
            {
                l.Reverse();
            }
            t.Nodes.Clear();
            Utilities.UpdateTreeview(t, l);
        }
        public static void searchPlayList(string search, TreeNodeCollection allNodesNames, TreeView t)
        {
            List<string> names = new List<string>();
            foreach (TreeNode n in allNodesNames)
            {
                names.Add(n.Text);
            }
            int index = names.BinarySearch(search);
            if (index >= 0)
            {
                t.SelectedNode = t.Nodes[index];
                t.Nodes[index].BackColor = System.Drawing.Color.Beige;

            }
        }
        public static void createPlaylistDisplay(DataGridView d)
        {
            d.BackgroundColor = Color.Gainsboro;
            d.GridColor = Color.Gainsboro;
            d.Columns.Add("Number", "");// this cell will display the index of each son
            d.Columns.Add("Duration", "Duration");//this cell will display the duration of each song
            d.Columns.Add("Name", "Name");
            d.Columns.Add("MoreOptions", "");
            for (int j = 0; j < d.Columns.Count; j++)
            {
                string columnName = d.Columns[j].Name;
                switch (columnName)
                {
                    case "Number":
                        d.Columns[j].Width = 40;
                        break;
                    case "Duration":
                        d.Columns[j].Width = 100;
                        break;
                    case "Name":
                        d.Columns[j].Width = 450;
                        break;
                    case "MoreOptions":
                        d.Columns[j].Width = 100;
                        break;

                }
            }

        }
        public static void addSongs(string playListName, string[] FilePaths, DataGridView d)
        {
            string[]lineParts = new string[FilePaths.Length];
            List<string> allSongsNames = new List<string>();
           
            for (int i=0;i<FilePaths.Length;i++)
            {
                var a = new DirectoryInfo(FilePaths[i]);
                lineParts[i] =a.Name+'|'+FilePaths[i] ;
                allSongsNames.Add(a.Name);
            }
            System.IO.File.AppendAllLines(mainFilePath + playListName + "\\" + playListName + ".lil", lineParts);
            refreshPlaylistDisplay(d, FilePaths, allSongsNames);
        }
        public static void GetAllSongsFromDictionary(string playlistPath,DataGridView d)
        {
        
            List<string> allLines = new List<string>();
            string[] s = System.IO.File.ReadAllLines(playlistPath);
            List<string> names = new List<string>();
            List<string> filePaths = new List<string>();
            foreach (string line in s)
            {
                string[] sub = line.Split('|');
                names.Add(sub[0]);
                filePaths.Add(sub[1]);
            } 
            refreshPlaylistDisplay(d,filePaths.ToArray(),names);
        }
        public static void refreshPlaylistDisplay(DataGridView d,string[]filePaths,List<string>allsongsName)
        {
            int i = d.RowCount-1;
            int j = 0;
            foreach(string paths in filePaths)
            {
                var inf = new DirectoryInfo(filePaths[j]);
                if(inf.Extension!=".mp3")
                {
                    continue;
                }
                try
                {
                    TagLib.File f = TagLib.File.Create(paths, TagLib.ReadStyle.Average);
                    var duration = ((int)f.Properties.Duration.TotalMinutes);
                    
                    f.Dispose();
                    d.Rows.Add();
                    d.Rows[i].Cells[0].Value = i + 1;
                    d.Rows[i].Cells[1].Value = duration + ":00";
                    d.Rows[i].Cells[2].Value = allsongsName[j];
                    j++;
                    i++;
                }
                catch
                {
                    continue;
                }
               
              
            }
            
        }
       
    }
}
