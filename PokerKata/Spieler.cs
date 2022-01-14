namespace PokerKata
{
    public class Spieler
    {
        public Spieler(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Hand Hand { get; set; }
    }
}