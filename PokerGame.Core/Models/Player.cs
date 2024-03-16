
namespace PokerGame.Core.Models
{
    public class Player
    {
        public int Id { get; }
        public List<Card> Hand { get; }
        public int Score { get; set; }

        public Player(int id, int score)
        {
            Id = id;
            Hand = new List<Card>();
            Score = score;
        }
    }
}
