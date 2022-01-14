using System;

namespace PokerKata
{
    public class Karte
    {
        public Enums.Farben Farbe { get; set; }

        public Enums.Kartenwert Wertung { get; set; }

        public override string ToString()
        {
            return Enum.GetName(Farbe) + " " + Enum.GetName(Wertung);
        }
    }
}