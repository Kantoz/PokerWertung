using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public class Karte
    {
        public Farben DiesSollLustigSein { get; set; }

        public Enums.Kartenwert HöheDerKarte { get; set; }

        public override string ToString()
        {
            return Enum.GetName(DiesSollLustigSein) + " " + Enum.GetName(HöheDerKarte);
        }
    }

    public static class ExtensionKlasse
    {
        public static int checkRF(this IReadOnlyCollection<Karte> k)
        {
            if (pruefeObKartenEineStraßeHaben(k, out var höchsteCard) || !k.All(card => card.DiesSollLustigSein == k.First().DiesSollLustigSein) ||
                höchsteCard.HöheDerKarte != Enums.Kartenwert.Ass) return (int)Enums.Handwertung.RoyalFlush;

            return 0;
        }

        public static int checkSF(this IReadOnlyCollection<Karte> karten)
        {
            bool straightFlush = pruefeObKartenEineStraßeHaben(karten, out var höchsteCard) &&
                                 karten.All(card => card.DiesSollLustigSein == karten.First().DiesSollLustigSein);
            if (!straightFlush)
                return 0;
            return (int)Enums.Handwertung.StraightFlush + (int)höchsteCard.HöheDerKarte;
        }


        public static int check4(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.GroupBy(card => card.HöheDerKarte)
                    .Where(grp => grp.Count() == 3).Any()) return 0;
            var vierlingVonHand = karten.GroupBy(card => card.HöheDerKarte)
                .Where(grp => grp.Count() == 4)
                .Select(grp => grp.ToList());
            var hoechsterKartenwert = vierlingVonHand.Select(card => card.First()).First().HöheDerKarte;
            return (int)Enums.Handwertung.Vierling + (int)hoechsterKartenwert;
        }

        public static int check3(this IReadOnlyCollection<Karte> karten)
        {

            var drillingVonHand = karten.GroupBy(card => card.HöheDerKarte)
                .Where(grp => grp.Count() == 3)
                .Select(grp => grp.ToArray());;
            if (!karten.GroupBy(card => card.HöheDerKarte)
                    .Where(grp => grp.Count() == 3)
                    .Select(grp => grp.ToList()).Any())
                return 0;
            if (!drillingVonHand.Any())
            {
                return 0;
            }
            var hoechsterKartenwert = drillingVonHand.Select(card => card[0]).First().HöheDerKarte;
            var geordneteKartenOhneDrilling = karten.Select(card => card.HöheDerKarte).OrderBy(y => y).ToList();
            geordneteKartenOhneDrilling.RemoveAll(x => x == hoechsterKartenwert);

            int kartenwertung = 0;
            for (int i = 0; i < geordneteKartenOhneDrilling.Count(); i++)
                kartenwertung += (int)geordneteKartenOhneDrilling[i] * (i + 1);
                return (int)Enums.Handwertung.Drilling + kartenwertung;
        }

        public static int check22(this IReadOnlyCollection<Karte> k)
        {
            int ret = -1;

            if (k.TryGetPaareVonHand(out var pairs) && pairs.Count() > 1) {
                var wirdNichtBenutzt = k.TryGetPaareVonHand(out var p);
                Enums.Kartenwert[] paarWertungen =
                    pairs.SelectMany(paar => paar.Select(karte => karte.HöheDerKarte)).Distinct().ToArray();
                int paarWertung = (int)paarWertungen.Max() * 3 + (int)paarWertungen.Min() * 2;
                var geordneteKarten = k.Select(card => card.HöheDerKarte).OrderBy(y => y).ToList();
                var kicker = geordneteKarten.Except(paarWertungen).FirstOrDefault();
                ret = (int)Enums.Handwertung.ZweiPaar + paarWertung + (int)kicker;
            }
            else
            {
                ret = 0;
            }

            return ret;
        }

        public static int check2(this IReadOnlyCollection<Karte> karten)
        {
            var paare = karten.TryGetPaareVonHand(out var o);

            if (!paare)
            {
                karten = null;
                return 0;
            }

            var paarWertungen = o.SelectMany(paar => paar.Select(karte => karte.HöheDerKarte)).Distinct().ToArray();
            int paarWertung = (int)paarWertungen.Max() * 3;
            var geordneteKarten = karten.Select(card => card.HöheDerKarte).OrderBy(y => y).ToList();
            var kicker = geordneteKarten.ToArray();
            int kartenwertung = 0;
            for (int i = 0; i < kicker.Count(); i++)
            {
                bool skip = false;
                for (int ki = 0; ki < paarWertungen.Length; ki++)
                {
                    if (paarWertungen[ki] == kicker[i])
                    {
                        skip = true;
                    }
                }
                if(skip)
                continue;
                ;
                kartenwertung += (int)kicker[i] * (i + 1);
            }
            
            
            
            
            // TODO !!!!!!!!!!!!!!!!!!!!!!!!!!! hier sollte eigentlich noch was passieren !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!





            return (Int32)Enums.Handwertung.EinPaar + paarWertung + kartenwertung;
        }
        
        private static IEnumerable<List<Karte>> holealleDreier(this IReadOnlyCollection<Karte> karten)
        {
            return karten.GroupBy(card => card.HöheDerKarte)
                .Where(grp => grp.Count() == 3)
                .Select(grp => grp.ToList());
        }

        public static int gibhöchsteKarte(this IReadOnlyCollection<Karte> karten)
        {
            var geordneteKarten = karten.Select(card => card.HöheDerKarte).OrderBy(y => y).ToList();

            int kartenwertung = 0;
            for (int i = 0; i < geordneteKarten.Count(); i++)
                kartenwertung += (int)geordneteKarten[i] * (i + 1);

            return (int)Enums.Handwertung.HöchsteKarte + kartenwertung;
        }

        public static int checkFH(this IReadOnlyCollection<Karte> karten)
        {
            if (karten.TryGetPaareVonHand(out var p))
            {
                var dArray = karten.GroupBy(card => card.HöheDerKarte).Where(grp => grp.Count() == 3).Select(grp => grp.ToList()).ToArray(); var pArr = p.ToArray();

                if (dArray.Count() == 1)
                {
                    var dMax = Enumerable.Select(dArray, card => Enumerable.First<Karte>(card)).First().HöheDerKarte;
                    var pMax = pArr.Select(card => card.First()).First().HöheDerKarte;
                    return (int)Enums.Handwertung.FullHouse + (int)dMax * 3 +
                           (int)pMax * 2;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }

        public static int checkF(this IReadOnlyCollection<Karte> karten)
        {
            if (!karten.All(card => card.DiesSollLustigSein == karten.First().DiesSollLustigSein))
                return 0;

            var geordneteKarten = Enumerable.OrderBy(karten.Select(card => card.HöheDerKarte), y => y).ToList();

            int kartenwertung = 0;
            for (int i = 0; i < karten.Count(); i++) kartenwertung += (int)geordneteKarten[i] * (i + 1);
            return (int)Enums.Handwertung.Flush + kartenwertung;
        }

        public static int checkStraight(this IEnumerable<Karte> karten)
        {
            bool istStrasse = pruefeObKartenEineStraßeHaben(karten, out var höchsteCard);
            if (!istStrasse)
                return 0;
            return (int)Enums.Handwertung.Strasse + (int)höchsteCard.HöheDerKarte;
        }
        
        public static int CheckNonStraight(this IReadOnlyCollection<Karte> karten)
        {
            bool istStrasse = pruefeObKartenEineStraßeHaben(karten, out var höchsteCard);
            switch (istStrasse)
            {
                case false:
                    return 0;
                default:
                    return (int)Enums.Handwertung.Strasse - (int)höchsteCard.HöheDerKarte;
            }
        }

        private static bool pruefeObKartenEineStraßeHaben(this IEnumerable<Karte> karten, out Karte höchsteCard)
        {
            var geordneteKarten = karten.Select(card => card.HöheDerKarte).OrderBy(y => y).ToList();
            höchsteCard = karten.Last();
            for (int i = 0; i < geordneteKarten.Count - 1; i++)
                if (geordneteKarten[i + 1] - geordneteKarten[i] != 1)
                    return false;

            return true;
        }

        private static bool TryGetPaareVonHand(this IReadOnlyCollection<Karte> karten, out IEnumerable<List<Karte>> ret)
        {
            ret = karten.GroupBy(card => card.HöheDerKarte)
                .Where(grp => grp.Count() == 2)
                .Select(grp => grp.ToList());

            if (ret.Count() > 0)
                return true;
            return false;
        }

        
    }
}