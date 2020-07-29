using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tp1Module6.Models
{
    public class SamouraiCreateEditVM
    {
        public Samourai Samourai { get; set; }
        public List<Arme> Armes { get; set; } = new List<Arme>();
        public int IdSelectedArme { get; set; }
        public List<ArtMartial> ArtMartiaux { get; set; } = new List<ArtMartial>();
        public List<int> ArtMartiauxId { get; set; } = new List<int>();
    }
}