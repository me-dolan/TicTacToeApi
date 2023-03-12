namespace TicTacToeApi.Request
{
    public class MoveRequest
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string PlayerSymbol { get; set; }
    }
}
