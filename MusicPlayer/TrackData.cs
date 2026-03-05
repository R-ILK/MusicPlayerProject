using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class TracksData : DbContext
    {
        public TracksData() : base("TracksData"){}
        public DbSet<Track> Tracks { get; set; }
    }
}
