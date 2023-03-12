using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToeApi.Core.Interface;
using TicTacToeApi.Request;
using TicTacToeApi.Response;

namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        
        /// <summary>
        /// Return all games
        /// </summary>
        /// <returns>All games in db</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var response = await _gameService.GetAllGames();
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        
        /// <summary>
        /// Get game from Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var response = await _gameService.GetGameAsync(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// create game
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Player1 or Player2 is "x" or "o".
        /// Sample request:
        ///     POST /game
        ///     {
        ///         "player1": "x",
        ///         "player1": "o"
        ///     }
        /// </remarks>
        /// <returns>Creating game</returns>
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            var response = await _gameService.CreateGame(request.Player1, request.Player2);
            return Ok(new GameCreateResponse()
            {
                GameId = response.Id,
                Message = "Game create"
            });
        }

        
        /// <summary>
        /// Delete game from Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                _gameService.DeleteGame(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Make move in game with ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Next player, symbol player and board this game</returns>
        /// /// <remarks>
        /// Row and col greater than or equal to 2 
        /// 
        /// Sample request:
        ///     PUT /1/move
        ///     {
        ///         "row": "1",
        ///         "col": "1",
        ///         "playerSymbol": "x"
        ///     }
        /// </remarks>
        /// <response code="200">succes</response>
        /// <response code="400"></response>
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MakeMove(int id, [FromBody] MoveRequest request)
        {
            try
            {
                var response = await _gameService.MakeMove(id, request.Row, request.Col, request.PlayerSymbol);
                return Ok(new MakeMoveResponse()
                {
                    NextPlayer = response.CurrentPlayer,
                    Board = response.Board,
                    Winner = response.Winner
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
