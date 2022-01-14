using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public class Hand
    {
        public IEnumerable<Karte> Karten { get; set; }

        public int GetWertung()
        {
            var readOnlyKarten = Karten.ToArray();
            return readOnlyKarten.CheckRoyalFlush()
                   + readOnlyKarten.CheckStraightFlush()
                   + readOnlyKarten.CheckVierling()
                   + readOnlyKarten.CheckFlush()
                   + readOnlyKarten.CheckFullHouse()
                   + readOnlyKarten.CheckStraight()
                   + readOnlyKarten.CheckDrilling()
                   + readOnlyKarten.CheckZweiPaare()
                   + readOnlyKarten.CheckEinPaare()
                   + readOnlyKarten.CheckHoechsteKarte();
        }
    }
}