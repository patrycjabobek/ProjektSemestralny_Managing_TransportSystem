using System;
using System.Collections.Generic;
using System.Text;

namespace RJKMLibrary
{
    public class Relacja
    {
        public ushort idRelacji { get; set; }
        public ushort numerLinii { get; set; }
        public ushort idPierwszegoPrzystanku { get; set; }
        public ushort idOstatniegoPrzystanku { get; set; }
        
    }
}
