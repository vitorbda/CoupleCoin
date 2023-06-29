using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoupleCoinApi.Migrations
{
    /// <inheritdoc />
    public partial class attuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couple_Users_User1Id",
                table: "Couple");

            migrationBuilder.DropForeignKey(
                name: "FK_Couple_Users_User2Id",
                table: "Couple");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseType_Users_OwnerId",
                table: "ExpenseType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasPair",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "PasswordConfirmed",
                table: "User",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Couple_User_User1Id",
                table: "Couple",
                column: "User1Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Couple_User_User2Id",
                table: "Couple",
                column: "User2Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseType_User_OwnerId",
                table: "ExpenseType",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couple_User_User1Id",
                table: "Couple");

            migrationBuilder.DropForeignKey(
                name: "FK_Couple_User_User2Id",
                table: "Couple");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseType_User_OwnerId",
                table: "ExpenseType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "PasswordConfirmed");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailConfirmed",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPair",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Couple_Users_User1Id",
                table: "Couple",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Couple_Users_User2Id",
                table: "Couple",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseType_Users_OwnerId",
                table: "ExpenseType",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
