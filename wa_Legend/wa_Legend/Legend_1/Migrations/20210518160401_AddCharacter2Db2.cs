using Microsoft.EntityFrameworkCore.Migrations;

namespace Legend_1.Migrations
{
    public partial class AddCharacter2Db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Orders_CharacterId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Characters",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_CharacterId",
                table: "Characters",
                newName: "IX_Characters_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Orders_OrderId",
                table: "Characters",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Orders_OrderId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Characters",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_OrderId",
                table: "Characters",
                newName: "IX_Characters_CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Orders_CharacterId",
                table: "Characters",
                column: "CharacterId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
