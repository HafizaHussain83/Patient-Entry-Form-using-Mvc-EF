using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCStore.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    sex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    adrress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    entry_date = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    referencing_doctor_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    diagnosis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    department_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
