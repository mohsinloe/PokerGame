using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;
using static PokerGame.Common.Utility;

namespace PokerGame.Core.Services
{
    public class HandService : IHandService
    {
        public HandRank RankHand(List<Card> cards)
        {
            if (IsStraightFlush(cards))
            {
                return HandRank.StraightFlush;
            }
            else if (IsFlush(cards))
            {
                return HandRank.Flush;
            }
            else if (IsStraight(cards))
            {
                return HandRank.Straight;
            }
            else if (HasOnePair(cards))
            {
                return HandRank.OnePair;
            }
            else
            {
                return HandRank.HighCard;
            }
        }

        private bool IsStraightFlush(List<Card> cards)
        {
            return IsFlush(cards) && IsStraight(cards);
        }

        private bool IsFlush(List<Card> cards)
        {
            Suit suit = cards[0].Suit;
            foreach (var card in cards)
            {
                if (card.Suit != suit)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsStraight(List<Card> cards)
        {
            cards.Sort((x, y) => x.Value.CompareTo(y.Value));
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i + 1].Value - cards[i].Value != 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasOnePair(List<Card> cards)
        {
            cards.Sort((x, y) => x.Value.CompareTo(y.Value));
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Value == cards[i + 1].Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
