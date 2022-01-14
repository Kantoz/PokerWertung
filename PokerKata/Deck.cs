using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public class Deck
    {
        public Deck()
        {
            foreach (var farbe in Enum.GetValues<Enums.Farben>())
            foreach (var wertung in Enum.GetValues<Enums.Kartenwert>())
                Karten.Add(new Karte
                {
                    Farbe = farbe,
                    Wertung = wertung
                });
        }

        public List<Karte> Karten { get; set; } = new();

        public Hand GibKartenFuerSpieler()
        {
            IEnumerable<Karte> spielerKarten = Karten.GetRange(0, 5);
            Karten = Karten.Skip(5).ToList();

            return new Hand { Karten = spielerKarten };
        }

        public void Mischen()
        {
            var rng = new Random();
            if (Karten.Any())
            {
                int n = Karten.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    var value = Karten[k];
                    Karten[k] = Karten[n];
                    Karten[n] = value;
                }
            }
        }
    }
}