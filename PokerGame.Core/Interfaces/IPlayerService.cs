using PokerGame.Core.Models;

namespace PokerGame.Core.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetPlayers();
        public List<Player> AddPlayers(int numPlayers);
    }
}
