using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame.Common
{
    public class Utility
    {
        public enum Suit
        {
            Spades,
            Clubs,
            Hearts,
            Diamonds
        }
        public enum HandRank
        {
            HighCard,
            OnePair,
            Straight,
            Flush,
            StraightFlush
        }
    }
}
