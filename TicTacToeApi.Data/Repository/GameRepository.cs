using TicTacToeApi.Data.Interface;
using TicTacToeApi.Data.Model;

namespace TicTacToeApi.Data.Repository
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext _context;
        public GameRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Game>();
        }

        public async Task<Game> FindAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }

        public Game Find(int id)
        {
            var result = _dbSet.Find(id);
            return result;
        }
    }
}
