namespace PokerKata
{
    public static class Enums
    {
        
        public enum Kartenwert
        {
            Zwei,
            Drei,
            Vier,
            Fünf,
            Sechs,
            Sieben,
            Acht,
            Neun,
            Zehn,
            Bube,
            Dame,
            König,
            Ass
        }

        

        public enum Handwertung
        {
            HöchsteKarte = 0,
            EinPaar = 100,
            ZweiPaar = 1000,
            Drilling = 2000,
            Strasse = 3000,
            Flush = 4000,
            FullHouse = 5000,
            Vierling = 6000,
            StraightFlush = 7000,
            RoyalFlush = 8000
        }
    }
}