
using PokerGame.Core.Models;

namespace PokerGame.Core.Interfaces
{
    public interface IGameService
    {
        public Response StartGame(GameParameters gameParameters);
        public Response StartRound();
        public Response EndRound();
        public Response DetermineOverallWinner();
    }
}
