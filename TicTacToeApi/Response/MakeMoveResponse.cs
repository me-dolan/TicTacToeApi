namespace TicTacToeApi.Response
{
    public class MakeMoveResponse
    {
        public string NextPlayer { get; set; }
        public string Board { get; set; }
        public string Winner { get; set; }
    }
}
