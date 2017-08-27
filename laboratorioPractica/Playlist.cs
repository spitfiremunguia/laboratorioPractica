using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorioPractica
{
    public class Playlist
    {
        private string binPath;
        private string name;
        private string description;
        public Playlist(string name, string description)
        {
            this.SetName(name);
            this.SetDescription(description);
            this.SetBinPath(@"C:\lilPlay\" + name);
        }
        public  string GetBinPath()
        {
            return binPath;
        }

        public void SetBinPath(string value)
        {
            binPath = value;
        }
        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            name = value;
        }
        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(string value)
        {
            description = value;
        }
    }
}
