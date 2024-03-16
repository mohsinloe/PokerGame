using PokerGame.Core.Models;

namespace PokerGame.Core.Interfaces
{
    public interface IDeckService
    {
        public void Shuffle();
        public List<Card> DealCards(int numPlayers);
    }
}
