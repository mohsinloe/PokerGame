using PokerGame.Core.Models;

namespace PokerGame.Core.Interfaces
{
    public interface IScorerService
    {
        public void AssignScores(List<Player> players);
    }
}
