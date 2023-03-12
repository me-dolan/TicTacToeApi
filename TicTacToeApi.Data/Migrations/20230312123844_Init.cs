using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPlayer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
