using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;
using static PokerGame.Common.Utility;

namespace PokerGame.Core.Services
{
    public class ScorerService : IScorerService
    {
        private readonly IHandService _handService;
        public ScorerService(IHandService handService) 
        {
            _handService = handService;
        }
        public void AssignScores(List<Player> players)
        {
            foreach (var player in players)
            {
                switch (_handService.RankHand(player.Hand))
                {
                    case HandRank.StraightFlush:
                        player.Score += 5;
                        break;
                    case HandRank.Flush:
                        player.Score += 4;
                        break;
                    case HandRank.Straight:
                        player.Score += 3;
                        break;
                    case HandRank.OnePair:
                        player.Score += 2;
                        break;
                    case HandRank.HighCard:
                        player.Score += 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
