using PokerGame.Core.Models;
using static PokerGame.Common.Utility;

namespace PokerGame.Core.Interfaces
{
    public interface IHandService
    {
        public HandRank RankHand(List<Card> cards);
    }
}
