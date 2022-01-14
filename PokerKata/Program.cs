using System;

namespace PokerKata
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var deck = new Deck();
                deck.Mischen();
                var playerOne = new Spieler("Fritz");
                var playerTwo = new Spieler("Hans");
                playerOne.Hand = deck.GibKartenFuerSpieler();
                playerTwo.Hand = deck.GibKartenFuerSpieler();
                foreach (var karte in playerOne.Hand.Karten) Console.WriteLine(playerOne.Name + " " + karte);
                foreach (var karte in playerTwo.Hand.Karten) Console.WriteLine(playerTwo.Name + " " + karte);
                var gewinner = Endwertung.ErmittleGewinner(new[] { playerOne, playerTwo });
                Console.WriteLine(gewinner.Name);
                Console.WriteLine("****************************");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}