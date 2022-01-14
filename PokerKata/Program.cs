using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    internal class Program
    {
        static Karte[] Karten;
        
        private static void Main(string[] args)
        {
            Karten =
                new Karte[Enum.GetValues<Farben>().Length * Enum.GetValues<Enums.Kartenwert>().Length];
            int i = 0;
            foreach (var farbe in Enum.GetValues<Farben>())
            foreach (var wertung in Enum.GetValues<Enums.Kartenwert>())
            {
                Karten[i] = new Karte
                {
                    DiesSollLustigSein = farbe,
                    HöheDerKarte = wertung
                };
                i++;
            }
               
            /**********************************************************************************/
            
            for (int j = 0; j < 10; j++)
            {
                Karten =
                    new Karte[Enum.GetValues<Farben>().Length * Enum.GetValues<Enums.Kartenwert>().Length];
                int k = 0;
                foreach (var farbe in Enum.GetValues<Farben>())
                foreach (var wertung in Enum.GetValues<Enums.Kartenwert>())
                {
                    Karten[k] = new Karte
                    {
                        DiesSollLustigSein = farbe,
                        HöheDerKarte = wertung
                    };
                    k++;
                }
                
                #region Mischen
                // mische das Deck!
                // TODO ist das so richtig? !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                List<Karte> Decj_KARTEN = Karten.ToList();
                var rng = new Random();
                if (Decj_KARTEN.Count > 0)
                {
                    int n = Decj_KARTEN.Count;
                    while (n > 1)
                    {
                        n--;
                        int l = rng.Next(n + 1);
                        var Karte = Decj_KARTEN[l];
                        Decj_KARTEN[l] = Decj_KARTEN[n];
                        Decj_KARTEN[n] = Karte;
                    }
                }

                Karten = Decj_KARTEN.ToArray();

        #endregion

        var k1 = new Program().InitialisiereEinenSpieler();
        var k2 = new Program().InitialisiereEinenSpieler();
        
        
        
                var p1 = new Spieler("Fritz");
                var p2 = new Spieler("Hans");
                p1.Karten = k1;
                p2.Karten = k2;
                p2.Karten.ToList().CheckNonStraight();
                foreach (var pk1 in p1.Karten) Console.WriteLine(p1.Name + " " + pk1);
                foreach (var pk2 in p2.Karten) Console.WriteLine(p2.Name + " " + pk2);
                Console.WriteLine(Endwertung.ErmittleGewinner(new[] { p1, p2 }).Name);
                Console.WriteLine("****************************");
                Console.WriteLine();
            }

            Console.ReadLine();
        } // Main-Methode
        
        public IEnumerable<Karte> InitialisiereEinenSpieler()
        {
            IEnumerable<Karte> spielerKarten = Karten.ToList().GetRange(0, 5);
            Karten = Karten.ToList().Skip(5).ToArray();

            return new List<Karte>(spielerKarten);
        }
    }
    
    public enum Farben
    {
        Herz,
        Kreuz,
        Pik,
        Diamond
    }
}