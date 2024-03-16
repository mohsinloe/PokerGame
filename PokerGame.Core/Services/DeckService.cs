using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;
using static PokerGame.Common.Utility;

namespace PokerGame.Core.Services
{
    public class DeckService : IDeckService
    {
        private List<Card> _deck;

        public DeckService()
        {
            _deck = GenerateDeck();
        }
        public void Shuffle()
        {
            for (int i = _deck.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                Card temp = _deck[i];
                _deck[i] = _deck[j];
                _deck[j] = temp;
            }
        }
        public List<Card> DealCards(int numPlayers)
        {
            return _deck.GetRange(0, numPlayers * 2);
        }
        private List<Card> GenerateDeck()
        {
            var deck = new List<Card>();
            for (int value = 2; value <= 14; value++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    deck.Add(new Card(value, suit));
                }
            }
            return deck;
        }
    }
}
