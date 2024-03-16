using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;
using PokerGame.Core.Services;

var gameParameters = new GameParameters(); 

Console.Write("Enter players count (number only): ");
gameParameters.numberOfPlayers = Convert.ToInt32(Console.ReadLine());

Console.Write("Enter number of rounds (number only): ");
gameParameters.numberOfRounds = Convert.ToInt32(Console.ReadLine());

var deckService = new DeckService();
var playerService = new PlayerService();
var handService = new HandService();
var scorerService = new ScorerService(handService);
var gameService = new GameService(deckService, playerService, handService, scorerService);

gameService.StartGame(gameParameters);

for (int i = 0; i < gameParameters.numberOfRounds; i++)
{
    gameService.StartRound();
    Console.WriteLine($"Round {i + 1} ended. Winner is {gameService.EndRound().players.OrderByDescending(p => p.Score).ToList().FirstOrDefault().Id + 1}");
}

var p = gameService.DetermineOverallWinner().players.FirstOrDefault();

Console.WriteLine($"Winner is : Player {p.Id + 1}");
