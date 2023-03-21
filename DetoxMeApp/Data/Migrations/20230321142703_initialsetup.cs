using Microsoft.EntityFrameworkCore.Migrations;

namespace DetoxMeApp.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetoxQuestion = table.Column<string>(nullable: true),
                    DetoxAnswer = table.Column<string>( nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detox", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detox");
        }
    }
}
