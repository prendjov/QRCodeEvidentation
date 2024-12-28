using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_RoomName",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Lectures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: true),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_RoomName",
                table: "Lectures",
                column: "RoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name");
        }
    }
}
