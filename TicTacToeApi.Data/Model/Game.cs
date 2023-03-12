using System.Text.Json;
using System.Xml;

namespace TicTacToeApi.Data.Model
{
    public class Game
    {
        public int Id { get; set; }
        //public string[,] Board { get; set; } = new string[3, 3];
        public string Board { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string CurrentPlayer { get; set; }
        public string? Winner { get; set; }
        public GameState GameState { get; set; }

        
        
    }

    public enum GameState
    {
        InProgress,
        Draw,
        EndGame
    }
}
