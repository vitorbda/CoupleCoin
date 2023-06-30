using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoupleCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class Att : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "User",
                newName: "AlterDate");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseType",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "ExpenseType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AlterDate",
                table: "ExpenseType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoupleId",
                table: "ExpenseType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCouple",
                table: "ExpenseType",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseType_CoupleId",
                table: "ExpenseType",
                column: "CoupleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseType_Couple_CoupleId",
                table: "ExpenseType",
                column: "CoupleId",
                principalTable: "Couple",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseType_Couple_CoupleId",
                table: "ExpenseType");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseType_CoupleId",
                table: "ExpenseType");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "ExpenseType");

            migrationBuilder.DropColumn(
                name: "AlterDate",
                table: "ExpenseType");

            migrationBuilder.DropColumn(
                name: "CoupleId",
                table: "ExpenseType");

            migrationBuilder.DropColumn(
                name: "IsCouple",
                table: "ExpenseType");

            migrationBuilder.RenameColumn(
                name: "AlterDate",
                table: "User",
                newName: "CreateDate");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
