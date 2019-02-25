using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Data.Migrations
{
    public partial class CreatedUserRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Rooms",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Rooms",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Messages",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Messages",
                newName: "CreatedById");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatedById",
                table: "Rooms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LastModifiedById",
                table: "Rooms",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatedById",
                table: "Rooms",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_LastModifiedById",
                table: "Rooms",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_CreatedById",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_LastModifiedById",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CreatedById",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_LastModifiedById",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Rooms",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Rooms",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Messages",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Messages",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
