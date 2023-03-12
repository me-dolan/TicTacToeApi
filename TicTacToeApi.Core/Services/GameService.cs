using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using TicTacToeApi.Core.Interface;
using TicTacToeApi.Data.Interface;
using TicTacToeApi.Data.Model;

namespace TicTacToeApi.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        
        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<Game> CreateGame(string player1, string player2)
        {
            var game = new Game()
            {
                Player1 = player1,
                Player2 = player2,
                CurrentPlayer = player1,
                Board = "         ",
                GameState = GameState.InProgress
            };

            _repository.Add(game);
            await _repository.SaveAsync();

            return game;
        }
        
        public async Task<Game> GetGameAsync(int id)
        {
            var result = await _repository.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var result = await _repository.Get();
            return result;
        }
        
        public async Task<Game> MakeMove(int id, int row, int col, string player)
        {
            if(row >= 3 && col >= 3)
            {
                throw new ArgumentException($"Going out of bounds");
            }
            
            var game = await GetGameAsync(id);

            if(game == null)
            {
                throw new ArgumentException($"Game {id} not found");
            }

            if(game.CurrentPlayer != player)
            {
                throw new ArgumentException($"Not your turn");
            }

            if(game.Winner != null)
            {
                throw new ArgumentException($"Game is over");
            }

            var board = GetBoardState(game);

            if (board[row, col] != " ")
            {
                throw new ArgumentException($"Position ({row}, {col}) is already taken");
            }

            board[row, col] = player;

            if (CheckForWinner(board, game.CurrentPlayer))
            {
                game.Winner = game.CurrentPlayer;
            }
            else if (CheckForDraw(board))
            {
                game.Winner = "Draw";
            }
            else
            {
                game.CurrentPlayer = game.CurrentPlayer == game.Player1 ? game.Player2 : game.Player1;
            }

            game.Board = BoardToString(board);

            _repository.Update(game);
            await _repository.SaveAsync();

            return game;
        }

        public void DeleteGame(int id)
        {
            var entity = _repository.Find(id);
            _repository.Remove(entity);
            _repository.Save();
        }

        private bool CheckForWinner(string[,] board, string playerSymbol)
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == playerSymbol && board[i, 1] == playerSymbol && board[i, 2] == playerSymbol)
                {
                    return true;
                }
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == playerSymbol && board[1, j] == playerSymbol && board[2, j] == playerSymbol)
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0, 0] == playerSymbol && board[1, 1] == playerSymbol && board[2, 2] == playerSymbol)
            {
                return true;
            }

            if (board[0, 2] == playerSymbol && board[1, 1] == playerSymbol && board[2, 0] == playerSymbol)
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw(string[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == " ")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string[,] GetBoardState(Game game)
        {
            var board = new string[3, 3];
            var state = game.Board;
            for (int i = 0; i < 9; i++)
            {
                var row = i / 3;
                var col = i % 3;
                board[row, col] = state[i].ToString();
            }
            return board;
        }

        private string BoardToString(string[,] board)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.Append(board[i, j]);
                }
            }
            return result.ToString();
        }
    }
}
