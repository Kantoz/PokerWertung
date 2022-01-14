using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public static class KartenExtension
    {
        private static bool IstFlush(this IReadOnlyCollection<Karte> karten)
        {
            return karten.All(card => card.Farbe == karten.First().Farbe);
        }

        public static int CheckRoyalFlush(this IReadOnlyCollection<Karte> karten)
        {
            bool royalflush = IstStrasse(karten, out var höchsteCard) && karten.IstFlush() &&
                              höchsteCard.Wertung == Enums.Kartenwert.Ass;
            if (!royalflush)
                return 0;
            return (int)Enums.Handwertung.RoyalFlush;
        }

        public static int CheckStraightFlush(this IReadOnlyCollection<Karte> karten)
        {
            bool straightFlush = IstStrasse(karten, out var höchsteCard) && karten.IstFlush();
            if (!straightFlush)
                return 0;
            return (int)Enums.Handwertung.StraightFlush + (int)höchsteCard.Wertung;
        }


        public static int CheckVierling(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.IstVierling())
                return 0;
            var vierlingVonHand = karten.GetVierlingVonHand();
            var hoechsterKartenwert = vierlingVonHand.Select(card => card.First()).First().Wertung;
            return (int)Enums.Handwertung.Vierling + (int)hoechsterKartenwert;
        }

        public static int CheckDrilling(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.IstDrilling())
                return 0;
            var drillingVonHand = karten.GetDrillingVonHand();
            var hoechsterKartenwert = drillingVonHand.Select(card => card.First()).First().Wertung;

            var geordneteKartenOhneDrilling = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();

            geordneteKartenOhneDrilling.RemoveAll(x => x == hoechsterKartenwert);

            int kartenwertung = 0;
            for (int i = 0; i < geordneteKartenOhneDrilling.Count(); i++)
                kartenwertung += (int)geordneteKartenOhneDrilling[i] * (i + 1);

            return (int)Enums.Handwertung.Drilling + kartenwertung;
        }

        public static int CheckZweiPaare(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.IstZweiPaare())
                return 0;

            var paare = karten.GetPaareVonHand();

            var paarWertungen = paare.SelectMany(paar => paar.Select(karte => karte.Wertung)).Distinct().ToArray();

            int paarWertung = (int)paarWertungen.Max() * 3 + (int)paarWertungen.Min() * 2;

            var geordneteKarten = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();

            var kicker = geordneteKarten.Except(paarWertungen).FirstOrDefault();

            return (int)Enums.Handwertung.ZweiPaar + paarWertung + (int)kicker;
        }

        public static int CheckEinPaare(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.IstEinPaar())
                return 0;

            var paare = karten.GetPaareVonHand();

            var paarWertungen = paare.SelectMany(paar => paar.Select(karte => karte.Wertung)).Distinct().ToArray();

            int paarWertung = (int)paarWertungen.Max() * 3;

            var geordneteKarten = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();

            var kicker = geordneteKarten.Except(paarWertungen).ToArray();

            int kartenwertung = 0;
            for (int i = 0; i < kicker.Count(); i++)
                kartenwertung += (int)kicker[i] * (i + 1);

            return (int)Enums.Handwertung.EinPaar + paarWertung + kartenwertung;
        }

        public static int CheckHoechsteKarte(this IReadOnlyCollection<Karte> karten)
        {
            var geordneteKarten = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();

            int kartenwertung = 0;
            for (int i = 0; i < geordneteKarten.Count(); i++)
                kartenwertung += (int)geordneteKarten[i] * (i + 1);

            return (int)Enums.Handwertung.HöchsteKarte + kartenwertung;
        }

        public static int CheckFullHouse(this IReadOnlyCollection<Karte> karten)
        {
            var drilling = karten.GetDrillingVonHand().ToArray();
            var paare = karten.GetPaareVonHand().ToArray();

            if (!drilling.Any() || !paare.Any())
                return 0;

            var hoechsterKartenwertDrilling = drilling.Select(card => card.First()).First().Wertung;
            var hoechsterKartenwertPaar = paare.Select(card => card.First()).First().Wertung;
            return (int)Enums.Handwertung.FullHouse + (int)hoechsterKartenwertDrilling * 3 +
                   (int)hoechsterKartenwertPaar * 2;
        }

        public static int CheckFlush(this IReadOnlyCollection<Karte> karten)
        {
            bool istFlush = karten.IstFlush();
            if (!istFlush)
                return 0;

            var geordneteKarten = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();

            int kartenwertung = 0;
            for (int i = 0; i < karten.Count(); i++) kartenwertung += (int)geordneteKarten[i] * (i + 1);

            return (int)Enums.Handwertung.Flush + kartenwertung;
        }

        public static int CheckStraight(this IReadOnlyCollection<Karte> karten)
        {
            bool istStrasse = IstStrasse(karten, out var höchsteCard);
            if (!istStrasse)
                return 0;
            return (int)Enums.Handwertung.Strasse + (int)höchsteCard.Wertung;
        }

        private static bool IstZweiPaare(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GetPaareVonHand().Count() == 2;
        }

        private static bool IstEinPaar(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GetPaareVonHand().Count() == 1;
        }

        private static bool IstStrasse(this IReadOnlyCollection<Karte> karten, out Karte höchsteCard)
        {
            var geordneteKarten = karten.Select(card => card.Wertung).OrderBy(y => y).ToList();
            höchsteCard = karten.Last();
            for (int i = 0; i < geordneteKarten.Count - 1; i++)
                if (geordneteKarten[i + 1] - geordneteKarten[i] != 1)
                    return false;

            return true;
        }

        private static IEnumerable<List<Karte>> GetPaareVonHand(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GroupBy(card => card.Wertung)
                .Where(grp => grp.Count() == 2)
                .Select(grp => grp.ToList());
        }

        private static IEnumerable<List<Karte>> GetDrillingVonHand(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GroupBy(card => card.Wertung)
                .Where(grp => grp.Count() == 3)
                .Select(grp => grp.ToList());
        }

        private static bool IstVierling(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GetVierlingVonHand().Any();
        }

        private static bool IstDrilling(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GetDrillingVonHand().Any();
        }

        private static IEnumerable<List<Karte>> GetVierlingVonHand(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GroupBy(card => card.Wertung)
                .Where(grp => grp.Count() == 4)
                .Select(grp => grp.ToList());
        }
    }
}