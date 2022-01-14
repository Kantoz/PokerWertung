using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public static class Endwertung
    {   
        public static Spieler ErmittleGewinner(IEnumerable<Spieler> spieler)
        {
            var spielers = spieler.ToList();    
            spielers.Sort(new Spieler.SpielerComparer());
            return spielers.Last();
        }
        
        
    }
}