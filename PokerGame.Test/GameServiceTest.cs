using PokerGame.Core.Services;
using PokerGame.Core.Models;

namespace PokerGame.Test
{
    public class GameServiceTest
    {
        [Fact]
        public void StartGame_ShouldCreatePlayers()
        {
            // Arrange
            var gameParameters = new GameParameters();
            gameParameters.numberOfPlayers = 4;
            gameParameters.numberOfRounds = 5;
            var deckService = new DeckService();
            var playerService = new PlayerService();
            var handService = new HandService();
            var scorerService = new ScorerService(handService);
            var gameService = new GameService(deckService, playerService, handService, scorerService);

            // Act
            var players = gameService.StartGame(gameParameters);

            // Assert
            Assert.Equal(4, players.players.Count);
        }

        [Fact]
        public void StartRound_ShouldDealsCardsToPlayers()
        {
            // Arrange
            var gameParameters = new GameParameters();
            gameParameters.numberOfPlayers = 2;
            gameParameters.numberOfRounds = 5;
            var deckService = new DeckService();
            var playerService = new PlayerService();
            var handService = new HandService();
            var scorerService = new ScorerService(handService);
            var gameService = new GameService(deckService, playerService, handService, scorerService);

            // Act
            var players = gameService.StartGame(gameParameters);
            gameService.StartRound();

            // Assert
            foreach (var player in players.players)
            {
                Assert.Equal(2, player.Hand.Count);
            }
        }

        [Fact]
        public void EndGame_WhenAllRoundsCompleted_DeterminesOverallWinner()
        {
            // Arrange
            var gameParameters = new GameParameters();
            gameParameters.numberOfPlayers = 4;
            gameParameters.numberOfRounds = 5;
            var deckService = new DeckService();
            var playerService = new PlayerService();
            var handService = new HandService();
            var scorerService = new ScorerService(handService);
            var gameService = new GameService(deckService, playerService, handService, scorerService);
            gameService.StartGame(gameParameters);

            // Act
            for (int i = 0; i < gameParameters.numberOfRounds; i++)
            {
                gameService.StartRound();
                gameService.EndRound();
            }

            // Assert
            Assert.NotNull(gameService.DetermineOverallWinner().players);
        }
    }
}