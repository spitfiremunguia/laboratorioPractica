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
        public static void CreateMainBin(string mainFilePath)
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
                n.Name = playlistName;
                aTreeView.Nodes.Add(n);
            }
            return aTreeView;
        }
        public static List<string> GetAllPlaylistbins(string mainFilePath)
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
        public static void Createlilplyfiles(string mainFilePath, string plyName, string description)
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
        public static void SortPlaylistByName(bool upWard, TreeView t)
        {

            List<string> l = new List<string>();
            foreach (TreeNode n in t.Nodes)
            {
                l.Add(n.Text);
            }
            if (upWard)
            {
                l = l.OrderBy(x => x.ToString()).ToList();

            }
            else
            {
                l = l.OrderByDescending(x => x.ToString()).ToList();
            }
            t.Nodes.Clear();
            Utilities.UpdateTreeview(t, l);
        }
        public static void SearchPlayList(string search, TreeNodeCollection allNodesNames, TreeView t)
        {
            List<string> names = new List<string>();
            foreach (TreeNode n in allNodesNames)
            {
                names.Add(n.Text);
            }
            int index = names.BinarySearch(search);
            if (index >= 0)
            {
                var a= t.Nodes.Find(search,false)[0];
                t.SelectedNode = a;
                t.HideSelection = false;
               

            }
        }
        public static void SearchSongByName(string name,DataGridView d)
        {

            for(int i=0;i<d.Rows.Count;i++)
            {
               if(d.Rows[i].Cells[2].Value.ToString()==name)
                {

                    d.CurrentCell = d.Rows[i].Cells[2];
                    d.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }

        }
        public static void CreatePlaylistDisplay(DataGridView d)
        {
            d.BackgroundColor = Color.Gainsboro;
            d.GridColor = Color.Gainsboro;
            d.Columns.Add("Number", "");// this cell will display the index of each son
            d.Columns.Add("Duration", "Duration");//this cell will display the duration of each song
            d.Columns.Add("Name", "Name");
            d.Columns.Add("MoreOptions", "");
            for (int j = 0; j < d.Columns.Count; j++)
            {
                d.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                string columnName = d.Columns[j].Name;
                switch (columnName)
                {
                    case "Number":
                        d.Columns[j].Width = 63;
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
        public static void AddSongs(string playListName, string[] FilePaths, DataGridView d)
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
            RefreshPlaylistDisplay(d, FilePaths, allSongsNames);
        }
        public static void ReadLilFiles(string playlistPath,ref List<string>names,ref List<string>filePaths,bool getPathsAlso)
        {
            List<string> allLines = new List<string>();
            string[] s = System.IO.File.ReadAllLines(playlistPath);
            foreach (string line in s)
            {
                string[] sub = line.Split('|');
                names.Add(sub[0]);
                if(getPathsAlso)
                {
                    filePaths.Add(sub[1]);
                }
                
            }
        }
        public static void GetAllSongsFromDictionary(string playlistPath,DataGridView d)
        {
            List<string> names = new List<string>();
            List<string> filePaths = new List<string>();
            ReadLilFiles(playlistPath,ref names,ref filePaths,true);
            RefreshPlaylistDisplay(d,filePaths.ToArray(),names);
        }
        public static void SortPlayList(bool upWard, DataGridView d,string playListName,bool byName)
        {
            if(d.Rows.Count>1)
            {
                List<Song> allPlaylistSongs = new List<Song>();
                List<string> names = new List<string>();
                List<string> paths = new List<string>();
                ReadLilFiles(mainFilePath + playListName + "\\" + playListName + ".lil", ref names,ref paths,true);
                for(int i=0;i<d.Rows.Count-1;i++)
                {
                    var duration = GetSongDuration(paths[i]);
                    var name = names[i];
                    var s = new Song(name,duration.ToString()+":00");
                    allPlaylistSongs.Add(s);
                }
                if(byName)
                {
                    if(upWard)
                    {
                       allPlaylistSongs= allPlaylistSongs.OrderBy(x => x.getSongName()).ToList();
                    }
                    else
                    {
                       allPlaylistSongs= allPlaylistSongs.OrderByDescending(x => x.getSongName()).ToList();
                    }
                }
                else
                {
                    if (upWard)
                    {
                       allPlaylistSongs= allPlaylistSongs.OrderBy(x => x.getSongDuration()).ToList();
                    }
                    else
                    {
                       allPlaylistSongs= allPlaylistSongs.OrderByDescending(x => x.getSongDuration()).ToList();
                    }                    
                }
                for(int i=0;i<d.Rows.Count-1;i++)
                {
                    for(int j=0;j<d.Columns.Count-1;j++)
                    {
                        string columnName = d.Columns[j].Name;
                        switch (columnName)
                        {
                            case "Number":
                                d.Rows[i].Cells[j].Value = i+1;
                                break;
                            case "Duration":
                                d.Rows[i].Cells[j].Value = allPlaylistSongs[i].getSongDuration();
                                break;
                            case "Name":
                                d.Rows[i].Cells[j].Value = allPlaylistSongs[i].getSongName();
                                break;

                        }
                        
                    }
                }
            }
        }
        public static int GetSongDuration(string path)
        {
            try
            {
                TagLib.File f = TagLib.File.Create(path, TagLib.ReadStyle.Average);
                return ((int)f.Properties.Duration.TotalMinutes);
            }
            catch
            {
                return 0;
            }
        }
        
        public static void RefreshPlaylistDisplay(DataGridView d,string[]filePaths,List<string>allsongsName)
        {
            int i = d.RowCount-1;
            int j = 0;
           
            foreach (string paths in filePaths)
            {
                var inf = new DirectoryInfo(filePaths[j]);
                try
                {
                    var duration = GetSongDuration(paths);
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
                for (int h = 0; h < d.Rows.Count; h++)
                {
                    for (int k = 0; k < d.Columns.Count; k++)
                    {
                        d.Rows[h].Cells[k].Style.BackColor = Color.LightGray;
                    }
                }


            }
            
        }
        public static string SearchSongPath(string path,string name)
        {
            if(System.IO.File.Exists(mainFilePath + path + "\\" + path + ".lil"))
            {
                string[] allLines = System.IO.File.ReadAllLines(mainFilePath + path + "\\" + path + ".lil");
                Dictionary<string, string> searchFile = new Dictionary<string, string>();
                string[][] getKeys = new string[allLines.Length][];
                for (int i = 0; i < allLines.Length; i++)
                {
                    getKeys[i] = allLines[i].Split('|');
                }
                for (int i = 0; i < getKeys.Length; i++)
                {
                    if (!searchFile.ContainsKey(getKeys[i][0]))
                    {
                        searchFile.Add(getKeys[i][0], getKeys[i][1]);
                    }

                }
                return searchFile[name];
            }
            return string.Empty;
           
            
        }
        

    }
}
