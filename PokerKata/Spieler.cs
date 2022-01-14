using System.Collections.Generic;
using System.Linq;

namespace PokerKata
{
    public class Spieler
    {
        private string spieler_name = "test";

        public string Name{ get { return spieler_name.ToString();}
            set { spieler_name = value; } }


        public class SpielerComparer : IComparer<Spieler>
        {
            public int Compare(Spieler x, Spieler y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
            
                var xk = x.Karten.ToArray();
                int xr = xk.checkRF() + xk.checkSF() - xk.check3() + xk.check4() + (xk.checkF() + xk.checkFH() + xk.check3() + xk.checkStraight() + xk.check22() + xk.check2() + xk.gibhöchsteKarte());
            
                var yk = y.Karten.ToArray();
                var yr = (yk.checkRF()+ (yk.check2()
                                             + (yk.checkSF()
                                             + yk.check4()))
                                             + yk.checkF() + yk.checkFH() + yk.checkStraight()) + yk.check3()
                            + yk.check22()
                                + yk.gibhöchsteKarte();


                if (xr > yr)
                    return 1;
                else if (yr > xr)
                    return -1;
                else
                {
                    return 0;
                }
            }
        }

        public Spieler(string name)
        {
            Name = name;
        }


        public IEnumerable<Karte> Karten { get; set; }
        
        
    }
}