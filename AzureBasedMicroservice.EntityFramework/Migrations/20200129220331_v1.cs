using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureBasedMicroservice.EntityFramework.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alterings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Operation = table.Column<int>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    IsIncrease = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alterings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alterings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlteringId = table.Column<int>(nullable: false),
                    TrackingCode = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Alterings_AlteringId",
                        column: x => x.AlteringId,
                        principalTable: "Alterings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FullName" },
                values: new object[] { 1, "User 1" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FullName" },
                values: new object[] { 2, "User 2" });

            migrationBuilder.CreateIndex(
                name: "IX_Alterings_CustomerId",
                table: "Alterings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AlteringId",
                table: "Payments",
                column: "AlteringId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Alterings");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
