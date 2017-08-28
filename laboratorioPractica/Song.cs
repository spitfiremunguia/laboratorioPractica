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
        private double duration { get; set; }
        private sbyte fullPath { get; set; }
        public Song()
        {

        }

        public Song(string songName, double duration)
        {
            this.songName = songName;
            this.duration = duration;
        }
        public string getSongName() { return this.songName; }
        public double getSongDuration() { return this.duration; }
        public void setSongName(string songName) { this.songName = songName; }
        public void setSongDuration(double songDuration) { this.duration = songDuration; }
    }
}
