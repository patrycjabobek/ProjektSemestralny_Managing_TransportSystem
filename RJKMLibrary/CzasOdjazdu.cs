using System;
using System.Collections.Generic;
using System.Text;

namespace RJKMLibrary
{
    class CzasOdjazdu
    {
        public ushort idPrzejazdu { get; set; }
        public ushort idDnia { get; set; }
        public TimeSpan czas { get; set; }
        public ushort idCzasu { get; set; }
    }
}
