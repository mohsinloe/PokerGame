using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;

namespace PokerGame.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private List<Player> _players;

        public PlayerService()
        {
            _players = new List<Player>();
        }
        public List<Player> GetPlayers()
        {
            return _players;
        }

        public List<Player> AddPlayers(int numPlayers)
        {
            _players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                _players.Add(new Player(i, 0));
            }

            return _players;
        }
    }
}
