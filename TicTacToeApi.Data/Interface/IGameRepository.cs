using TicTacToeApi.Data.Model;

namespace TicTacToeApi.Data.Interface
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game> FindAsync(int id);
        Game Find(int id);
    }
}
