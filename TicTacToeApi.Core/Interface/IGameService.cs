using TicTacToeApi.Data.Model;

namespace TicTacToeApi.Core.Interface
{
    public interface IGameService
    {
        Task<Game> CreateGame(string player1, string player2);
        void DeleteGame(int id);
        Task<Game> MakeMove(int id, int row, int col, string player);
        Task<Game> GetGameAsync(int id);
        Task<IEnumerable<Game>> GetAllGames();
    }
}
