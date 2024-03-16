using Microsoft.AspNetCore.Mvc;
using PokerGame.Core.Interfaces;
using PokerGame.Core.Models;

namespace PokerGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Start Poker Game
        /// </summary>
        /// <param name="gameParameters">GameParameters Object</param>
        /// <returns>Returns ActionResult of Response type</returns>
        [HttpPost("Start")]
        public ActionResult<Response> StartGame(GameParameters gameParameters)
        {
            try
            {
                return Ok(_gameService.StartGame(gameParameters));
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Error = new Error() 
                    { 
                        Code = 500, 
                        Message = "An error occured", 
                        Detail = ex.Message 
                    }
                };
            }
        }

        /// <summary>
        /// Start Poker Game Round
        /// </summary>
        /// <returns>Returns ActionResult of Response type</returns>
        [HttpPost("round/start")]
        public ActionResult<Response> StartRound()
        {
            try
            {
                return Ok(_gameService.StartRound());
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Error = new Error() 
                    { 
                        Code = 500, 
                        Message = "An error occured", 
                        Detail = ex.Message 
                    }
                };
            }
        }

        /// <summary>
        /// End Poker Game Round
        /// </summary>
        /// <returns>Returns ActionResult of Response type</returns>
        [HttpPost("round/end")]
        public ActionResult<Response> EndRound()
        {
            try
            {
                return Ok(_gameService.EndRound());
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Error = new Error() 
                    { 
                        Code = 500, 
                        Message = "An error occured", 
                        Detail = ex.Message 
                    }
                };
            }
        }

        /// <summary>
        /// Determine Overall Winner of Game
        /// </summary>
        /// <returns>Returns ActionResult of Response type</returns>
        [HttpGet("OverallWinner")]
        public ActionResult<Response> GetOverallWinner()
        {
            try
            {
                return Ok(_gameService.DetermineOverallWinner());
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Error = new Error()
                    {
                        Code = 500,
                        Message = "An error occured",
                        Detail = ex.Message
                    }
                };
            }
        }
    }
}
