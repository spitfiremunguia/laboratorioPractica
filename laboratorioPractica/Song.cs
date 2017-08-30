using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorioPractica
{
    public class Song
    {
        private string songName { get; set; }
        private string duration { get; set; }
        private string fullPath { get; set; }
        public Song()
        {

        }

        public Song(string songName, string duration)
        {
            this.songName = songName;
            this.duration = duration;
        }
        public string getSongName() { return this.songName; }
        public string getSongDuration() { return this.duration; }
        public void setSongName(string songName) { this.songName = songName; }
        public void setSongDuration(string songDuration) { this.duration = songDuration; }

       
    }
}
