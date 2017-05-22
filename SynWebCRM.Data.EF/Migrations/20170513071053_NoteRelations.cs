using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SynWebCRM.Data.EF.Migrations
{
    public partial class NoteRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Customer_NoteId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Deal_NoteId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DealId",
                table: "Note",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_CustomerId",
                table: "Note",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_DealId",
                table: "Note",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Customer_CustomerId",
                table: "Note",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Deal_DealId",
                table: "Note",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "DealId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Customer_CustomerId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Deal_DealId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_CustomerId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_DealId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Customer_NoteId",
                table: "Note",
                column: "NoteId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Deal_NoteId",
                table: "Note",
                column: "NoteId",
                principalTable: "Deal",
                principalColumn: "DealId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
