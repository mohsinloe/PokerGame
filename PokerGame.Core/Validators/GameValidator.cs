using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using PokerGame.Core.Models;

namespace PokerGame.Core.Validators
{
    public class GameInputValidator : AbstractValidator<GameParameters>
    {
        public GameInputValidator()
        {
            RuleFor(p => p.numberOfRounds).GreaterThanOrEqualTo(2).WithMessage("Minimum 2 rounds can be played in the game.");
            RuleFor(p => p.numberOfRounds).LessThanOrEqualTo(5).WithMessage("Maximum 5 rounds can be played in the game.");
            RuleFor(p => p.numberOfPlayers).GreaterThanOrEqualTo(2).WithMessage("Minimum 2 players can play the game.");
            RuleFor(p => p.numberOfPlayers).LessThanOrEqualTo(6).WithMessage("Maximum 6 players can play the game."); ;
        }
        public GameInputValidator(int currentRound)
        {
            RuleFor(p => p.numberOfRounds).GreaterThan(currentRound);
        }
    }

    public class GamePlayersValidator : AbstractValidator<List<Player>>
    {
        public GamePlayersValidator()
        {
            RuleFor(p => p).NotNull();
            RuleFor(p => p).NotEmpty();
        }
    }

    public class GameRoundValidator : AbstractValidator<BigInteger>
    {
        public GameRoundValidator(int currentRound)
        {
            RuleFor(p => p).GreaterThan(currentRound).WithMessage("All rounds completed and winner already determined.");
        }
    }
}
