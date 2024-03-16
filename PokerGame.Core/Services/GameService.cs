using PokerGame.Common;
using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;
using PokerGame.Core.Validators;
using System.Collections.Immutable;
using static PokerGame.Common.Utility;

namespace PokerGame.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IDeckService _deckService;
        private readonly IPlayerService _playerService;
        private readonly IHandService _handService;
        private readonly IScorerService _scorerService;
        private List<Player> _players;
        private List<Card> _deck;
        private int _currentRound;
        private int _numRounds;

        public GameService(IDeckService deckService, IPlayerService playerService,
                           IHandService handService, IScorerService scorerService)
        {
            _deckService = deckService;
            _playerService = playerService;
            _handService = handService;
            _scorerService = scorerService;
        }

        public Response StartGame(GameParameters gameParameters)
        {
            var gameInputValidator = new GameInputValidator();
            var validationResult = gameInputValidator.Validate(gameParameters);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    ValidationError = validationResult.Errors.Select(p => new FluentValidation.Results.ValidationFailure
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    }).ToList()
                };
            }

            _players = _playerService.AddPlayers(gameParameters.numberOfPlayers);
            _numRounds = gameParameters.numberOfRounds;
            _currentRound = 0;

            return new Response
            {
                players = _players,
                Round = gameParameters.numberOfRounds,
                Description = $"Game started. Total Players: {_players.Count} {Environment.NewLine}Total Round: {gameParameters.numberOfRounds}",
            };
        }

        public Response StartRound()
        {
            if (!IsGameStarted(_players))
            {
                return new Response
                {
                    players = null,
                    Error = new Error() { Code = 400, Message = "Game has not started yet.", Detail = "Game not started yet so the round cannot start." }
                };
            }

            var gamePlayerValidator = new GamePlayersValidator();
            var validationResult = gamePlayerValidator.Validate(_players);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    ValidationError = validationResult.Errors.Select(p => new FluentValidation.Results.ValidationFailure
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    }).ToList()
                };
            }

            var gameRoundValidator = new GameRoundValidator(_currentRound);
            validationResult = gameRoundValidator.Validate(_numRounds);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    ValidationError = validationResult.Errors.Select(p => new FluentValidation.Results.ValidationFailure
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage 
                    }).ToList()
                };
            }

            _deckService.Shuffle();
            _deck = _deckService.DealCards(_players.Count);

            _players.ForEach(player => player.Hand.AddRange(_deck.Skip(player.Id * 2).Take(2)));

            var (winners, highestRank) = GetWinnersAndRank();

            _scorerService.AssignScores(winners);
            _currentRound++;

            return new Response
            {
                Round = _currentRound,
                players = winners,
                Description = $"Round {_currentRound} started.",
                Error = null
            };
        }

        public Response EndRound()
        {
            if (!IsGameStarted(_players))
            {
                return new Response
                {
                    players = null,
                    Error = new Error() { Code = 400, Message = "Game has not started yet.", Detail = "Game not started yet so the winner cannot be determined." }            //    };
                };
            }

            var gamePlayerValidator = new GamePlayersValidator();
            var validationResult = gamePlayerValidator.Validate(_players);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    ValidationError = validationResult.Errors.Select(p => new FluentValidation.Results.ValidationFailure
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    }).ToList()
                };
            }

            var player = DetermineOverallWinner();
            var response = new Response();
            response.Round = _currentRound;
            response.players = _players;
            response.Error = null;
            response.Description = $"Winner of round {_currentRound} is Player: {player.players.FirstOrDefault().Id + 1}";

            if (_currentRound > _numRounds)
            {
                response.Description = $"{response.Description} {Environment.NewLine} Game winner is {player.players.FirstOrDefault().Id + 1}";
            }

            return response;
        }

        public Response DetermineOverallWinner()
        {
            if (!IsGameStarted(_players))
            {
                return new Response
                {
                    players = null,
                    Error = new Error() { Code = 400, Message = "Game has not started yet.", Detail = "Game not started yet so the winner cannot be determined." }            //    };
                };
            }

            var gamePlayerValidator = new GamePlayersValidator();
            var validationResult = gamePlayerValidator.Validate(_players);

            if (!validationResult.IsValid)
            {
                return new Response
                {
                    ValidationError = validationResult.Errors.Select(p => new FluentValidation.Results.ValidationFailure
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    }).ToList()
                };
            }

            List<Player> winnerList = new List<Player>();
            Player overallWinner = _players.OrderByDescending(p => p.Score).FirstOrDefault();
            if (overallWinner != null)
            {
                winnerList.Add(overallWinner);
            }

            return new Response
            {
                Description = $"Overall winner is Player {overallWinner.Id + 1}",
                players = winnerList
            };
        }

        private (List<Player> winners, HandRank highestRank) GetWinnersAndRank()
        {
            var winners = new List<Player>();
            HandRank highestRank = HandRank.HighCard;
            foreach (var player in _players)
            {
                HandRank rank = _handService.RankHand(player.Hand);
                if (rank > highestRank)
                {
                    highestRank = rank;
                    winners.Clear();
                    winners.Add(player);
                }
                else if (rank == highestRank)
                {
                    winners.Add(player);
                }
            }
            return (winners, highestRank);
        }

        private bool IsGameStarted(List<Player> playerList)
        {
            if (playerList is null)
            {
                return false;
            }

            return true;
        }
    }
}
