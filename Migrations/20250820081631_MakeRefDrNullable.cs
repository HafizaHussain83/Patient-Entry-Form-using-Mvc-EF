using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCStore.Migrations
{
    /// <inheritdoc />
    public partial class MakeRefDrNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "entry_date",
                table: "products",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "RefDr",
                table: "products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefDr",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "entry_date",
                table: "products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100);
        }
    }
}
