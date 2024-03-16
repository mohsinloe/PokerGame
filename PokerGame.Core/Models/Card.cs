using static PokerGame.Common.Utility;

namespace PokerGame.Core.Models
{
    public class Card
    {
        public int Value { get; }
        public Suit Suit { get; }

        public Card(int value, Suit suit)
        {
            Value = value;
            Suit = suit;
        }
    }
}
